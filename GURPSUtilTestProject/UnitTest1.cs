using System;
using TwilightShards.genLibrary;
using TwilightShards.GURPSUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GURPSUtilTestProject
{
    public class FakeDice : Dice
    {
        public int diceRoll;

        public override int gurpsRoll(int mod)
        {
            return diceRoll + mod;
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
    }
}