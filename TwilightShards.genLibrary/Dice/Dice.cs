using System;
using System.Text;
using NPack;

namespace TwilightShards.genLibrary
{    
    /// <summary>
    /// A object to abstract access to the Mersenne Twister as well as provide predefined functions
    /// </summary>
    public class Dice
    {
        /// <summary>
        /// The Mersenne Twister object
        /// </summary>
        protected MersenneTwister dice = new MersenneTwister((int)DateTime.Now.Ticks / 10); 
         
        /// <summary>
        /// Constructor
        /// </summary>
        public Dice()
        {
        }

        /// <summary>
        /// This returns a (uint) number from 1 to the passed number.
        /// </summary>
        /// <param name="x">The passed number</param>
        /// <returns>A number within [1, x].</returns>
        public uint RollFrom1ToX(uint x)
        {
            return (uint)(x * dice.NextDoublePositive() + 1);
        }

        /// <summary>
        /// This returns a (long) number from 1 to the passed number.
        /// </summary>
        /// <param name="x">The passed number</param>
        /// <returns>A number within [1, x].</returns>
        public long RollFrom1ToX(long x)
        {
            return (long)(x * dice.NextDoublePositive() + 1);
        }

        /// <summary>
        /// This returns a (ulong) number from 1 to the passed number.
        /// </summary>
        /// <param name="x">The passed number</param>
        /// <returns>A number within [1, x].</returns>
        public ulong RollFrom1ToX(ulong x)
        {
            return (ulong)(x * dice.NextDoublePositive() + 1);
        }

        /// <summary>
        /// Rolls a probablity and returns an integer from 1 to the specified range
        /// </summary>
        /// <param name="probSize">The range to roll up to (default: 100)</param>
        /// <returns>An integer describing the probablity</returns>
        public int intProb(int probSize = 100)
        {
            return (int)(probSize * dice.NextDoublePositive() + 1);
        }

        /// <summary>
        /// Returns a positive double from 0 to 1.
        /// </summary>
        /// <returns>A double of probability</returns>
        public double dblProb()
        {
            return dice.NextDoublePositive();
        }

        /// <summary>
        /// The default gurps roll
        /// </summary>
        /// <returns>A number between 3 and 18</returns>
        public int gurpsRoll()
        {
            return this.rng(3,6);
        }

        /// <summary>
        /// This function will roll a dice but with a capped mod.
        /// </summary>
        /// <param name="cap">The capped mod</param>
        /// <param name="val">Value being added</param>
        /// <returns>A dice roll</returns>
        public int gurpsRollWithCappedMod(int cap, double val)
        {
            int mod = (int)Math.Floor(val);
            return val > cap ? this.rng(3, 6) + cap : this.rng(3, 6) + mod;
        }

        /// <summary>
        /// This function will roll a dice but with a capped mod.
        /// </summary>
        /// <param name="cap">The capped mod</param>
        /// <param name="val">Value being added</param>
        /// <returns>A dice roll</returns>
        public int gurpsRollWithCappedMod(int cap, int val)
        {
            return val > cap ? this.rng(3, 6) + cap : this.rng(3, 6) + val;
        }

        /// <summary>
        /// The default gurps roll (with modifiers)
        /// </summary>
        /// <param name="mod">The modifier</param>
        /// <returns>A number between 3 and 18 adjusted by the modifier</returns>
        public int gurpsRoll(int mod)
        {
            return this.rng(3, 6, mod);
        }

        /// <summary>
        /// The base die - rolls from 1 to the size parameter
        /// </summary>
        /// <param name="size">The size (of the metaphorical die)</param>
        /// <returns>A number in that range</returns>
        public int rng(int size)
        {
            return (int)(size * dice.NextDoublePositive() + 1);
        }

        /// <summary>
        /// The base die - rolls from 1 to the size parameter
        /// </summary>
        /// <param name="size">The size (of the metaphorical die)</param>
        /// <returns>A number in that range</returns>
        public long rng(long size)
        {
            return (long)(size * dice.NextDoublePositive() + 1);
        }
        
        /// <summary>
        /// A number of die from 1 to num, from 1 to the size
        /// </summary>
        /// <param name="num">Number of die</param>
        /// <param name="size">The size of the die</param>
        /// <returns>A number in the range given by numDsize</returns>
        public int rng(int num, int size)
        {
            int total = 0;
            for (int i = 0; i < num; i++)
            {
                total = total + this.rng(size);
            }

            return total;
        }

        /// <summary>
        /// A number of die from 1 to num, from 1 to the size
        /// </summary>
        /// <param name="num">Number of die</param>
        /// <param name="size">The size of the die</param>
        /// <returns>A number in the range given by numDsize</returns>
        public long rng(int num, long size)
        {
            long total = 0;
            for (int i = 0; i < num; i++)
            {
                total = total + this.rng(size);
            }

            return total;
        }

        /// <summary>
        /// A number of die from 1 to num, from 1 to the size with a modifer
        /// </summary>
        /// <param name="num">Number of die</param>
        /// <param name="size">The size of the die</param>
        /// <param name="mod">The modifier to the roll</param>
        /// <returns>A number in the range given by numDsize + mod</returns>
        public int rng(int num, int size, int mod)
        {
            int total;
            total = this.rng(num, size) + mod;
            return total;
        }

        /// <summary>
        /// A number of die from 1 to num, from 1 to the size with a modifer. Will always return greater than or equal to zero.
        /// </summary>
        /// <param name="num">Number of die</param>
        /// <param name="size">The size of the die</param>
        /// <param name="mod">The modifier to the roll</param>
        /// <returns>A number in the range given by numDsize + mod , but always greater than or equal to zero.</returns>
        public int rngGTZero(int num, int size, int mod)
        {
            int total = this.rng(num, size) + mod;
            return total > 0 ? total : 0;
        }

        /// <summary>
        /// Rolls a number within [startVal + range
        /// </summary>
        /// <param name="startVal">The beginning point</param>
        /// <param name="range">The possible range of numbers </param>
        /// <returns>A double number</returns>
        public double rollRange(double startVal, double range){

            return (dice.NextDoublePositive()) * range + startVal;
        }

        /// <summary>
        /// A number within [startVal, endVal]. If both are the same, it'll return startVal without rolling.
        /// </summary>
        /// <param name="startVal">The begining Value</param>
        /// <param name="endVal">The ending Value</param>
        /// <returns>The number</returns>
        public int rollInIntRange(double startVal, double endVal)
        {
            if (endVal == startVal)
                return (int)Math.Floor(startVal);

            double range = endVal - startVal;
            return (int)Math.Floor((dice.NextDoublePositive() * range + startVal));
        }

        /// <summary>
        /// A number within [startVal, endVal]. If both are the same, it'll return startVal without rolling.
        /// </summary>
        /// <param name="startVal">The begining Value</param>
        /// <param name="endVal">The ending Value</param>
        /// <returns>The number</returns>
        public long rollInLongRange(double startVal, double endVal)
        {
            if (endVal == startVal)
                return (long)Math.Floor(startVal);

            double range = endVal - startVal;
            return (long)Math.Floor((dice.NextDoublePositive() * range + startVal));
        }

        /// <summary>
        /// A number within [startVal, endVal]. If both are the same, it'll return startVal without rolling.
        /// </summary>
        /// <param name="startVal">The begining Value</param>
        /// <param name="endVal">The ending Value</param>
        /// <returns>The number</returns>
        public double rollInRange(double startVal, double endVal)
        {
            if (endVal == startVal)
                return startVal;

            double range = endVal - startVal;
            return (dice.NextDoublePositive() * range + startVal);
        }

        /// <summary>
        /// Returns a random letter.
        /// </summary>
        /// <param name="upper">A bool for upper class letter (default: false)</param>
        /// <returns></returns>
        public char rndLetter(bool upper = false)
        {
            int UNICODE_LOWER_A = 65;
            char c = (char)this.rollRange(UNICODE_LOWER_A, 25);
            if (upper) return char.ToUpper(c);
            else return c;
        }

        /// <summary>
        /// A random sequence generation
        /// </summary>
        /// <param name="num">Number of characters in the sequence</param>
        /// <param name="upper">Whether or not to use capital letters in the sequence instead of lower case (default: false)</param>
        /// <returns></returns>
        public string rndCombination(int num, bool upper = false)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                s.Append(rndLetter(upper));
            }

            return s.ToString();
        }
        
        /// <summary>
        /// A simple coin flip function
        /// </summary>
        /// <returns>Heads (true) Tails (false)</returns>
        public bool coinFlip()
        {
            int num = intProb();
            if (num > 50) return true;
            else return false;
        }

        //GURPS Specific functions

        /// <summary>
        /// This function is designed for roll under in GURPS. It returns true if less or equal to the target
        /// </summary>
        /// <param name="target">Target number</param>
        /// <param name="mod">Mod for the roll. Defaults to 0</param>
        /// <returns>Whether or not it is less or more than the qual</returns>
        public bool rollUnderGurps(int target, int mod = 0){
            int roll = this.gurpsRoll(mod);
            if (roll <= target)
                return true;
            else
                return false;
        }
    }
}
