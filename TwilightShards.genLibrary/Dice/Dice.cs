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
        /// The base die - rolls from 1 to the size parameter
        /// </summary>
        /// <param name="size">The size (of the metaphorical die)</param>
        /// <returns>A number in that range</returns>
        public int rng(int size)
        {
            if (size <= 0)
                throw new Exception("The size of the dice must be positive.");
            
            return (int)(size * dice.NextDoublePositive() + 1);
        }

        /// <summary>
        /// The base die - rolls from 1 to the size parameter
        /// </summary>
        /// <param name="size">The size (of the metaphorical die)</param>
        /// <returns>A number in that range</returns>
        public long rng(long size)
        {
            if (size <= 0)
                throw new Exception("The size of the dice must be positive.");
            
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
            if (size <= 0)
                throw new Exception("The size of the dice must be positive.");
            if (num <= 0)
                throw new Exception("The number of the dice must be positive.");            

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
            if (size <= 0)
                throw new Exception("The size of the dice must be positive.");
            if (num <= 0)
                throw new Exception("The number of the dice must be positive.");

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
        public virtual int rng(int num, int size, int mod)
        {
            if (size <= 0)
                throw new Exception("The size of the dice must be positive.");
            if (num <= 0)
                throw new Exception("The number of the dice must be positive.");

            int total;
            total = this.rng(num, size) + mod;
            return total;
        }

        /// <summary>
        /// This function takes a dice roll, and then mulitplies it by a multiplier.
        /// </summary>
        /// <param name="num">Number of die</param>
        /// <param name="size">The size of the die</param>
        /// <param name="mod">The modifier to the roll</param>
        /// <param name="multiplier">The mulitplier of the roll</param>
        /// <returns>A number in the range, multiplied by the given number</returns>
        public virtual double MultiplyRNG(int num, int size, int mod, double multiplier)
        {
            if (size <= 0)
                throw new Exception("The size of the dice must be positive.");
            if (num <= 0)
                throw new Exception("The number of the dice must be positive.");

            double total;
            total = (this.rng(num, size) + mod) * multiplier;

            return total;
        }

        /// <summary>
        /// This function takes a dice roll, and then mulitplies it by a multiplier.
        /// </summary>
        /// <param name="num">Number of die</param>
        /// <param name="size">The size of the die</param>
        /// <param name="mod">The modifier to the roll</param>
        /// <param name="multiplier">The mulitplier of the roll</param>
        /// <returns>A number in the range, multiplied by the given number</returns>
        public virtual int MultiplyRNG(int num, int size, int mod, int multiplier)
        {
            if (size <= 0)
                throw new Exception("The size of the dice must be positive.");
            if (num <= 0)
                throw new Exception("The number of the dice must be positive.");

            int total;
            total = (this.rng(num, size) + mod) * multiplier;

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
            if (size <= 0)
                throw new Exception("The size of the dice must be positive.");
            if (num <= 0)
                throw new Exception("The number of the dice must be positive.");

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

            return (dice.NextDoublePositive() * (endVal - startVal) + startVal);
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

        //*******************************************************************************************
        // GURPS Specific Functions
        //*******************************************************************************************

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

        /// <summary>
        /// The default gurps roll (with modifiers). Alias function.
        /// </summary>
        /// <param name="mod">The modifier</param>
        /// <returns>A number between 3 and 18 adjusted by the modifier</returns>
        public virtual int gurpsRoll(int mod = 0)
        {
            return this.rng(3, 6, mod);
        }

        public virtual double gurpsRollMultiplied(double multiplier, int mod = 0)
        {
            return this.MultiplyRNG(3, 6, mod, multiplier);
        }

        public virtual int gurpsRollMultiplied(int multiplier, int mod = 0)
        {
            return this.MultiplyRNG(3, 6, mod, multiplier);
        }

        //*******************************************************************************************
        // General Methods
        //*******************************************************************************************

        /// <summary>
        /// This function will return a varied result from a base value. If exclusive is specified, it will not include the ends
        /// </summary>
        /// <param name="variance">The variance</param>
        /// <param name="baseValue">The base value</param>
        /// <param name="exclusive">Whether or not this excludes the end (default: false)</param>
        /// <returns>A varied value</returns>
        public double VaryResult(double variance, double baseValue, bool exclusive = false)
        {
            double val;

            if (!exclusive)
                return this.rollInRange(baseValue - variance, baseValue + variance);
            else{
                do{
                   val = this.rollInRange(baseValue - variance, baseValue + variance);
                } while (val == baseValue - variance || val == baseValue + variance);
                return val;
            }
        }

        /// <summary>
        /// This function will return a varied result from a base value. Will not be more than max. If exclusive is specified, it will not include the ends
        /// </summary>
        /// <param name="variance">The variance</param>
        /// <param name="baseValue">The base value</param>
        /// <param name="max">The maximum possible value</param>
        /// <param name="exclusive">Whether or not this excludes the end (default: false)</param>
        /// <returns>A varied value</returns>
        public double VaryResultMax(double variance, double baseValue, double max, bool exclusive = false)
        {
            return Math.Min(VaryResult(variance, baseValue, exclusive), max);
        }

        /// <summary>
        /// This function will return a varied result from a base value. Will not be less than min. If exclusive is specified, it will not include the ends.
        /// </summary>
        /// <param name="variance">The variance</param>
        /// <param name="baseValue">The base value</param>
        /// <param name="min">The minimum possible value</param>
        /// <param name="exclusive">Whether or not this excludes the end (default: false)</param>
        /// <returns>A varied value</returns>
        public double VaryResultMin(double variance, double baseValue, double min, bool exclusive = false)
        {
            double val = VaryResult(variance, baseValue, exclusive);
            if (val < min)
                return min;
            else
                return val;
        }
    }
}
