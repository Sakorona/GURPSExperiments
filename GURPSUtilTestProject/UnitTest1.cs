using System;
using TwilightShards.genLibrary;
using TwilightShards.GURPSUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GURPSUtilTestProject
{
    public class FakeDice : Dice
    {
        public int diceRoll;
        public int rngRoll;

        public override int gurpsRoll(int mod)
        {
            return diceRoll + mod;
        }

        public override int rng(int num, int size, int mod)
        {
            return rngRoll + mod;
        }
    }

    [TestClass]
    public class StarReferenceTesting
    {
        [TestMethod]
        public void VerifyStellarMass()
        {
            FakeDice ourDice = new FakeDice();
            ourDice.diceRoll = 10;

            int numDice = StarReference.getNumberOfStars(ourDice, 0);
            
            Assert.AreEqual(1, numDice);
        }

        [TestMethod]
        public void VerifyMaxNumberOfStars()
        {
            FakeDice ourDice = new FakeDice();
            ourDice.diceRoll = 18;
            
            int numDice = StarReference.getNumberOfStars(ourDice, 3);
            
            Assert.AreEqual(3, numDice);
        }

        [TestMethod]
        public void VerifyMinNumberOfStars()
        {
            FakeDice ourDice = new FakeDice();
            ourDice.diceRoll = 3;

            int numDice = StarReference.getNumberOfStars(ourDice, 0);
            
            Assert.AreEqual(1, numDice);
        }

        [TestMethod]
        public void VerifyStellarMassRoll()
        {
            int roll1 = 10;
            int roll2 = 14;

            double starMass = StarReference.rollStellarMass(roll1, roll2);

            Assert.AreEqual(.3, starMass);
        }

        [TestMethod]
        public void VerifyStellarMassOneBound()
        {
            int roll1 = 1;
            int roll2 = 14;

            double starMass = StarReference.rollStellarMass(roll1, roll2);

            Assert.AreEqual(0, starMass);
        }

        [TestMethod]
        public void VerifyStellarMassBothBounds()
        {
            int roll1 = 1;
            int roll2 = 19;

            double starMass = StarReference.rollStellarMass(roll1, roll2);

            Assert.AreEqual(0, starMass);
        }

        [TestMethod]
        public void VerifyStellarAge()
        {
            FakeDice ourDice = new FakeDice();
            ourDice.diceRoll = 9;
            ourDice.rngRoll = 6;

            double stellarAge = StarReference.genSystemAge(ourDice);

            Assert.AreEqual(5.5,stellarAge);            
        }

        [TestMethod]
        public void VerifyStellarAgeOutOfBounds()
        {
            FakeDice ourDice = new FakeDice();
            ourDice.diceRoll = 2;
            ourDice.rngRoll = 6;

            double stellarAge = StarReference.genSystemAge(ourDice);

            Assert.AreEqual(13.8, stellarAge);
        }

    }
}