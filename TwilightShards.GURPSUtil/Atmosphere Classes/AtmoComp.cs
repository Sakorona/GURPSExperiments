using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightShards.GURPSUtil
{
    /// <summary>
    /// A class describing the atmosphere.
    /// </summary>
    public class AtmoComp
    {
        //*******************************************************************************************
        // Members
        //*******************************************************************************************
        /// <summary>
        /// The internal list of elements in the atmosphere
        /// </summary>
        private List<AtmoCompRecord> atmoElements;

        /// <summary>
        /// The internal list of special conditions in the atmosphere
        /// </summary>
        private List<string> atmoConditions;

        /// <summary>
        /// Contains the flags for the atmosphere
        /// </summary>
        private bool[] atmoFlags;

        /// <summary>
        /// The atmosphere is marginal.
        /// </summary>
        private bool isMarginal;

        /// <summary>
        /// This dictionary are 
        /// </summary>
        private Dictionary<string, string> flagDescription;

        /* FLAG NUMERALS
         * 0 - Corrosive 
         * 1 - Suffocating
         * 2 - Lethally Toxic
         * 3 - Mildly Toxic
         * 4 - Highly Toxic
         */

        //*******************************************************************************************
        // Constructor
        //*******************************************************************************************
        /// <summary>
        /// Constructor. Takes no parameters.
        /// </summary>
        public AtmoComp()
        {
            atmoElements = new List<AtmoCompRecord>();
            atmoConditions = new List<string>();
            atmoFlags = new bool[5];
            isMarginal = false;
        }

        //*******************************************************************************************
        // General Atmosphere Functions
        //*******************************************************************************************
        /// <summary>
        /// Checks if the atmosphere contains an element
        /// </summary>
        /// <param name="element">String describing the element</param>
        /// <returns>True if it contains the element, false if not.</returns>
        public bool ContainsElement(string element)
        {
           /* var item = from i in this.atmoElements
                       where i.compound == element
                       select i;

            if (item.Count() >= 1)
                return true;
            else
                return false; */

            var result = this.atmoElements.FirstOrDefault(item => item.compound == element);
            if (result != null)
                return true;
            else
                return false;
            
        }
      
        /// <summary>
        /// Adds an element to the atmosphere. Checks to make sure duplicate elements are not added.
        /// </summary>
        /// <param name="element">The element being added</param>
        /// <param name="amount">The amount of the element</param>
        public void AddAtmoElement(string element, ElemAmount amount)
        {
            //do not add an element this already contains.
            if (this.ContainsElement(element))
                return;
            else
                this.atmoElements.Add(new AtmoCompRecord(element, amount));
        }

        /// <summary>
        /// Alters the element amount in the atmosphere.
        /// </summary>
        /// <param name="element">The element in the atmosphere</param>
        /// <param name="amount">New amount in the atmosphere</param>
        public void AlterAtmoElement(string element, ElemAmount amount)
        {
            foreach (AtmoCompRecord acr in this.atmoElements)
            {
                if (acr.compound == element) //update the record
                    acr.amount = amount;
            }
        }
        
        /// <summary>
        /// This adds a special condition to the atmosphere conditions list.
        /// </summary>
        /// <param name="condition">The special condition</param>
        public void AddSpecialCondition(string condition)
        {
            this.atmoConditions.Add(condition);
        }

        /// <summary>
        /// This will either add or increase the element if it's more than provided.
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="amount">The amount of the element</param>
        public void AddOrIncreaseAtmoElement(string element, ElemAmount amount)
        {
            if (this.ContainsElement(element))
            {
                foreach (AtmoCompRecord acr in this.atmoElements)
                {
                    if (acr.compound == element) //update the record
                    {
                        if (amount < acr.amount)
                            acr.amount = amount;
                    }
                }
            }
            else
                AddAtmoElement(element, amount);
        }

        /// <summary>
        /// This function purges and resets.
        /// </summary>
        public void Purge()
        {
            this.isMarginal = false;
            this.atmoConditions.Clear();
            this.atmoElements.Clear();

            this.atmoFlags[0] = false;
            this.atmoFlags[1] = false;
            this.atmoFlags[2] = false;
            this.atmoFlags[3] = false;
            this.atmoFlags[4] = false;
        }

        /// <summary>
        /// Returns a string describing the atmosphere
        /// </summary>
        /// <returns>Description of the atmosphere</returns>
        public string DescribeAtmosphere()
        {
            string desc = "";

            flagDescription = new Dictionary<string, string>();
            flagDescription.Add("Corrosive", "The atmosphere is corrosive");
            flagDescription.Add("Suffocating", "The atmosphere is suffocating.");
            flagDescription.Add("MildlyToxic", "The atmosphere is mildly toxic");
            flagDescription.Add("LethallyToxic", "The atmosphere is lethally toxic");
            flagDescription.Add("HighlyToxic", "The atmosphere is highly toxic.");

            List<string> primaryElements = new List<string>();
            List<string> secondaryElements = new List<string>();
            List<string> traceElements = new List<string>();

            foreach (AtmoCompRecord acr in this.atmoElements)
            {
                //short circuit.
                if (acr.amount == ElemAmount.Only)
                    return "Only has " + acr.compound;

                if (acr.amount == ElemAmount.Primary)
                    primaryElements.Add(acr.compound);

                if (acr.amount == ElemAmount.Some)
                    secondaryElements.Add(acr.compound);

                if (acr.amount == ElemAmount.Trace)
                    traceElements.Add(acr.compound);
            }

            if (primaryElements.Count >= 1)
                desc = "Primarily ";
            //now each.
            foreach (string s in primaryElements)
            {
                desc += s + " , ";
            }

            if (secondaryElements.Count >= 1)
                desc += " with significant amounts of ";
            foreach (string s in secondaryElements)
            {
                desc += s + " , ";
            }

            if (traceElements.Count >= 1)
                desc += " and trace amounts of ";
            for (int i = 0; i < traceElements.Count; i++)
            {
                desc += traceElements[i];
                if ((i + 1) < (traceElements.Count - 1))
                    desc += " , ";
            }

            desc += Environment.NewLine;
            desc += "Special Conditions: ";

            if (this.IsCorrosive())
                desc += flagDescription["Corrosive"] + "; ";
            if (this.IsHighlyToxic())
                desc += flagDescription["HighlyToxic"] + "; ";
            if (this.IsLethallyToxic())
                desc += flagDescription["LethallyToxic"] + "; ";
            if (this.IsMildlyToxic())
                desc += flagDescription["MildlyToxic"] + "; ";
            if (this.IsSuffocating())
                desc += flagDescription["Suffocating"] + "; ";

            //add in atmosphere conditions
            for (int i = 0; i < this.atmoConditions.Count; i++)
            {
                desc += this.atmoConditions[i];
                if ((i + 1) < (traceElements.Count - 1))
                    desc += "; ";
            }

            return desc;
        }

        //*******************************************************************************************
        // Atmosphere Flag Functions
        //*******************************************************************************************
        /// <summary>
        /// This sets the atmosphere as corrosive
        /// </summary>
        /// <param name="val">Value it's being set to</param>
        public void SetCorrosive(bool val)
        {
            this.atmoFlags[0] = val;
        }

        /// <summary>
        /// This sets the atmosphere as suffocating
        /// </summary>
        /// <param name="val">Value it's being set to</param>
        public void SetSuffocating(bool val)
        {
            this.atmoFlags[1] = val;
        }

        /// <summary>
        /// This sets the atmosphere as lethally toxic.
        /// </summary>
        /// <param name="val">Value it's being set to</param>
        public void SetMildlyToxic(bool val)
        {
            this.atmoFlags[3] = val;
        }

        /// <summary>
        /// This sets the atmosphere as highly toxic.
        /// </summary>
        /// <param name="val">Value it's being set to</param>
        public void SetHighlyToxic(bool val)
        {
            this.atmoFlags[4] = val;
        }

        /// <summary>
        /// This sets the atmosphere as lethally toxic.
        /// </summary>
        /// <param name="val">Value it's being set to</param>
        public void SetLethallyToxic(bool val)
        {
            this.atmoFlags[2] = val;
        }

        /// <summary>
        /// This sets the marginal status.
        /// </summary>
        /// <param name="val">The value it's being set to</param>
        public void SetMarginal(bool val)
        {
            this.isMarginal = val;
        }

        /// <summary>
        /// This function sees if the atmosphere is corrosive
        /// </summary>
        /// <returns></returns>
        public bool IsCorrosive()
        {
            return this.atmoFlags[0];
        }
        
        /// <summary>
        /// This function sees if the atmosphere is suffocating
        /// </summary>
        /// <returns></returns>
        public bool IsSuffocating()
        {
            return this.atmoFlags[1];
        }

        /// <summary>
        /// This function sees if the atmosphere is toxic
        /// </summary>
        /// <returns></returns>
        public bool IsToxic()
        {
            if (this.atmoFlags[2]) return true;
            if (this.atmoFlags[3]) return true;
            if (this.atmoFlags[4]) return true;

            return false;
        }
        
        /// <summary>
        /// Is this atmosphere mildly toxic?
        /// </summary>
        /// <returns>true if mildly toxic, false if not.</returns>
        public bool IsMildlyToxic()
        {
            return atmoFlags[4];
        }

        /// <summary>
        /// Is this atmosphere highly toxic?
        /// </summary>
        /// <returns>true if highly toxic, false if not.</returns>
        public bool IsHighlyToxic()
        {
            return atmoFlags[3];
        }

        /// <summary>
        /// Is this atmosphere lethally toxic?
        /// </summary>
        /// <returns>true if lethally toxic, false if not.</returns>
        public bool IsLethallyToxic()
        {
            return atmoFlags[2];
        }

        /// <summary>
        /// This returns the marginal cost.
        /// </summary>
        /// <returns></returns>
        public bool IsMarginal()
        {
            return this.isMarginal;
        }

    }
}
