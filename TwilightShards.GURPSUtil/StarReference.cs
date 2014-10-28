using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightShards.genLibrary;

namespace TwilightShards.GURPSUtil
{
    public static class StarReference
    {
        public static readonly double WHITE_DWARF_LUMIN = .001;
        public static readonly double GIANT_STAR_LUMINADJ = 10000; //10000, not 25. :(
        public static readonly  string WHITE_DWARF_SPECTRA = "D";

        private static DiceTreeNode starMassTable;
        private static double[][] starEvolutionTable;

        static StarReference()
        {
            #region starEvolutionTable creation
            starEvolutionTable = new double[34][]{
                new double[7]{.1, 3100.0, .0012, 0,0,0,0},
                new double[7]{.15,3200.0, .0036, 0,0,0,0},
                new double[7]{.2,3200.0, .0079, 0,0,0,0},
                new double[7]{.25,3300.0, .015, 0,0,0,0},
                new double[7]{.3,3300.0,.024,0,0,0,0},
                new double[7]{.35,3400.0,.037,0,0,0,0},
                new double[7]{.4,3500,.054,0,0,0,0},
                new double[7]{.45,3600.0,.07,.08,70,0,0},
                new double[7]{.5,3800,.09,.11,59,0,0},
                new double[7]{.55,4000,.11,.15,50,0,0},
                new double[7]{.6,4200,.3,.20,42,0,0},
                new double[7]{.65,4400,.15,.25,37,0,0},
                new double[7]{.7,4600,.19,.35,30,0,0},
                new double[7]{.75,4900,.23,.48,24,0,0},
                new double[7]{.8,5200,.28,.65,20,0,0},
                new double[7]{.85,5400,.36,.84,17,0,0},
                new double[7]{.9,5500, .45,1,14,0,0},
                new double[7]{.95,5700,.56,1.3,12,1.8,1.1},
                new double[7]{1,5800, .68, 1.6, 10,1.6,1},
                new double[7]{1.05,5900, .87, 1.9, 8.8, 1.4, .8},
                new double[7]{1.1, 6000, 1.1, 2.2, 7.7, 1.2, .7},
                new double[7]{1.15, 6100, 1.4, 2.6, 6.7, 1.0, .6},
                new double[7]{1.2, 6300, 1.7, 3, 5.9, .9, .6},
                new double[7]{1.25, 6400, 2.1, 3.5, 5.2, .8, .5},
                new double[7]{1.3, 6500, 2.5, 3.9, 4.6, .7, .4},
                new double[7]{1.35, 6600, 3.1, 4.5, 4.1, .6, .4},
                new double[7]{1.4, 6700, 3.7, 5.1, 3.7, .6, .4},
                new double[7]{1.45, 6900, 4.3, 5.7,3.3, .5, .3},
                new double[7]{1.5, 7000, 5.1, 6.5, 3, .5, .3},
                new double[7]{1.6, 7300, 6.7, 8.2, 2.5, .4, .2},
                new double[7]{1.7, 7500, 8.6, 10, 2.1, .3, .2},
                new double[7]{1.8, 7800, 11, 13, 1.8, .3, .2},
                new double[7]{1.9, 8000, 13, 16, 1.5, .2, .1},
                new double[7]{2, 8200, 16, 20, 1.3, .2, .1}
            };


            #endregion

            //init members.
            #region starMassTable Initialization
            starMassTable = new DiceTreeNode();
            starMassTable.SetRange(3, 3, DiceTreeBase.Init(3, 200),
                                DiceTreeBase.Init(11, 190));
            starMassTable.SetRange(4, 4, DiceTreeBase.Init(3, 180),
                                DiceTreeBase.Init(9, 170),
                                DiceTreeBase.Init(12, 160));
            starMassTable.SetRange(5, 5, DiceTreeBase.Init(3, 150),
                                DiceTreeBase.Init(8, 145));
            starMassTable.SetRange(6, 6, DiceTreeBase.Init(3, 130),
                                DiceTreeBase.Init(8, 125),
                                DiceTreeBase.Init(10, 120),
                                DiceTreeBase.Init(11, 115),
                                DiceTreeBase.Init(13, 110));
            starMassTable.SetRange(7, 7, DiceTreeBase.Init(3, 105),
                                DiceTreeBase.Init(8, 100),
                                DiceTreeBase.Init(10, 95),
                                DiceTreeBase.Init(11, 90),
                                DiceTreeBase.Init(13, 85));
            starMassTable.SetRange(8, 8, DiceTreeBase.Init(3, 80),
                                DiceTreeBase.Init(8, 75),
                                DiceTreeBase.Init(10, 70),
                                DiceTreeBase.Init(11, 65),
                                DiceTreeBase.Init(13, 60));
            starMassTable.SetRange(9, 9, DiceTreeBase.Init(3, 55),
                                DiceTreeBase.Init(9, 50),
                                DiceTreeBase.Init(12, 45));
            starMassTable.SetRange(10, 10, DiceTreeBase.Init(3, 40),
                                DiceTreeBase.Init(9, 35),
                                DiceTreeBase.Init(12, 30));
            starMassTable.SetRange(11, 11, DiceTreeBase.Init(3, 25));
            starMassTable.SetRange(12, 12, DiceTreeBase.Init(3, 20));
            starMassTable.SetRange(13, 13, DiceTreeBase.Init(3, 15));
            starMassTable.SetRange(14, 18, DiceTreeBase.Init(3, 10));
            #endregion
        }

        public static double rollStellarMass(Dice ourDice)
        {
            int roll1 = ourDice.gurpsRoll() -3;
            int roll2 = ourDice.gurpsRoll() -3;

            int mass = starMassTable.Walk<int>(roll1, roll2);

            return (mass/100.0);
        }

        public static int getNumberOfStars(Dice ourDice, int mod = 0)
        {
            int roll = ourDice.gurpsRoll() + mod;
            int numStars = (int)(Math.Floor((roll - 1.0) / 5.0));
                
            //fix a few possible logic bugs.
            if (numStars < 1) numStars = 1;
            else if (numStars > 3) numStars = 3;

            return numStars;           
        }

        public static double genSystemAge(Dice ourDice)
        {
            //get first roll
            int roll;
            roll = ourDice.gurpsRoll();

            if (roll == 3)
                return 0.01;
            if (roll >= 4 && roll <= 6)
                return (.1 + (ourDice.rng(1, 6, -1) * .3) + (ourDice.rng(1, 6, -1) * .05));
            if (roll >= 7 && roll <= 10)
                return (2 + (ourDice.rng(1, 6, -1) * .6) + (ourDice.rng(1, 6, -1) * .1));
            if (roll >= 11 && roll <= 14)
                return (5.6 + (ourDice.rng(1, 6, -1) * .6) + (ourDice.rng(1, 6, -1) * .1));
            if (roll >= 15 && roll <= 17)
                return (8 + (ourDice.rng(1, 6, -1) * .6) + (ourDice.rng(1, 6, -1) * .1));
            if (roll == 18)
                return (10 + (ourDice.rng(1, 6, -1) * .6) + (ourDice.rng(1, 6, -1) * .1));

            return 13.8;
        }

        public static double getTemperature(Dice ourDice, double starMass, double starAge)
        {
            if (starMass < .1 || starMass > 2)
                throw new Exception("Star Mass is invalid. Please pass a value between .1 and 2 solar masses");

            if (starAge > 14.0)
                throw new Exception("The stellar age is too old.");

            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                {
                    double currAge = starAge;
                    if (currAge > starEvolutionTable[i][4] && starEvolutionTable[i][4] != 0) //M-Span check.
                    {
                        currAge = currAge - starEvolutionTable[i][4];
                        if (currAge > starEvolutionTable[i][5] && starEvolutionTable[i][5] != 0) //S-Span check.
                        {
                            currAge = currAge - starEvolutionTable[i][5];
                            if (currAge >= starEvolutionTable[i][6] && starEvolutionTable[i][6] != 0) //G-Span check.
                                return 9000;
                            else //Giant Star Phase
                                return (ourDice.rng(2,6,-2) * 200) + 3000;
                        }
                        else
                        {  //Subgiant Phase
                            return starEvolutionTable[i][1] - ((currAge / starEvolutionTable[i][5]) * (starEvolutionTable[i][1] - 4800));
                        }
                    }
                    else
                    { //Main Sequence Phase
                        return starEvolutionTable[i][1] + ourDice.rng(-100, 100);
                    }
                }
            }
            return 0;
        }

        public static double getRadius(Dice ourDice, double starMass, double starAge, double starTemp, double starLumin)
        {
            if (starMass < .1 || starMass > 2)
                throw new Exception("Star Mass is invalid. Please pass a value between .1 and 2 solar masses");

            if (starAge > 14.0)
                throw new Exception("The stellar age is too old.");

            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                {
                    double currAge = starAge;
                    if (currAge > starEvolutionTable[i][4] && starEvolutionTable[i][4] != 0) //M-Span check.
                    {
                        currAge = currAge - starEvolutionTable[i][4];
                        if (currAge > starEvolutionTable[i][5] && starEvolutionTable[i][5] != 0) //S-Span check.
                        {
                            currAge = currAge - starEvolutionTable[i][5];
                            if (currAge >= starEvolutionTable[i][6] && starEvolutionTable[i][6] != 0) //G-Span check.
                                return 8748.95685 * Math.Pow(starMass, -1 / 3); 
                        }
                    }

                    return (155000 * Math.Sqrt(starLumin)) / Math.Pow(starTemp,2);
                }
            }
            return 0;
        }

        public static double getLuminosity(double starMass, double starAge)
        {
            if (starMass < .1 || starMass > 2)
                throw new Exception("Star Mass is invalid. Please pass a value between .1 and 2 solar masses");

            if (starAge > 14.0)
                throw new Exception("The stellar age is too old.");

            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                {
                    if (starEvolutionTable[i][4] == 0)
                        return starEvolutionTable[i][2];

                    double currAge = starAge;
                    if (currAge > starEvolutionTable[i][4]) //M-Span check.
                    {
                        currAge = currAge - starEvolutionTable[i][4];
                        if (currAge > starEvolutionTable[i][5]) //S-Span check.
                        {
                            currAge = currAge - starEvolutionTable[i][5];
                            if (currAge >= starEvolutionTable[i][6]) //G-Span check.
                                return WHITE_DWARF_LUMIN;
                            else //Giant Star Phase
                                return starEvolutionTable[i][3] * GIANT_STAR_LUMINADJ;
                        }
                        else //Subgiant Phase
                            return starEvolutionTable[i][3];
                    }
                    else //Main Sequence Phase
                        return starEvolutionTable[i][2] + ((starAge / starEvolutionTable[i][4]) * (starEvolutionTable[i][3] - starEvolutionTable[i][2]));
                }
            }
            return 0;
        }

        public static string getSpectralType(double starMass, double starTemp, double starAge)
        {
            if (starMass < .1 || starMass > 2)
                throw new Exception("Star Mass is invalid. Please pass a value between .1 and 2 solar masses");

            if (starAge > 14.0)
                throw new Exception("The stellar age is too old.");

            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                {
                    double currAge = starAge;
                    //short circuit.
                    if (starEvolutionTable[i][4] == 0)
                        return getStellarTypeFromMass(starMass);

                    if (currAge > starEvolutionTable[i][4] && starEvolutionTable[i][4] != 0) //M-Span check.
                    {
                        currAge = currAge - starEvolutionTable[i][4];
                        if (currAge > starEvolutionTable[i][5] && starEvolutionTable[i][5] != 0) //S-Span check.
                        {
                            currAge = currAge - starEvolutionTable[i][5];
                            if (currAge >= starEvolutionTable[i][6] && starEvolutionTable[i][6] != 0) //G-Span check.
                                return WHITE_DWARF_SPECTRA;
                            else //Giant Star Phase
                                return getStellarTypeFromTemp(starTemp);
                        }
                        else //Subgiant Phase
                            return getStellarTypeFromTemp(starTemp);
                    }
                    else //Main Sequence Phase
                        return getStellarTypeFromMass(starMass);
                }
            }
            return "X0";
        }
        
        private static string getStellarTypeFromMass(double mass)
        {
            if (mass <= .125) return "M7";
            if (.125 < mass && mass <= .175) return "M6";
            if (.175 < mass && mass <= .225) return "M5";
            if (.225 < mass && mass <= .325) return "M4";
            if (.325 < mass && mass <= .375) return "M3";
            if (.375 < mass && mass <= .425) return "M2";
            if (.425 < mass && mass <= .475) return "M1";
            if (.475 < mass && mass <= .525) return "M0";
            if (.525 < mass && mass <= .575) return "K8";
            if (.575 < mass && mass <= .625) return "K6";
            if (.625 < mass && mass <= .675) return "K5";
            if (.675 < mass && mass <= .725) return "K4";
            if (.725 < mass && mass <= .775) return "K2";
            if (.775 < mass && mass <= .825) return "K0";
            if (.825 < mass && mass <= .875) return "G8";
            if (.875 < mass && mass <= .925) return "G6";
            if (.925 < mass && mass <= .975) return "G4";
            if (.975 < mass && mass <= 1.025) return "G2";
            if (1.025 < mass && mass <= 1.075) return "G1";
            if (1.075 < mass && mass <= 1.125) return "G0";
            if (1.175 < mass && mass <= 1.20) return "F9";
            if (1.20 < mass && mass <= 1.225) return "F8";
            if (1.225 < mass && mass <= 1.275) return "F7";
            if (1.275 < mass && mass <= 1.325) return "F6";
            if (1.325 < mass && mass <= 1.375) return "F5";
            if (1.375 < mass && mass <= 1.425) return "F4";
            if (1.425 < mass && mass <= 1.475) return "F3";
            if (1.475 < mass && mass <= 1.55) return "F2";
            if (1.55 < mass && mass <= 1.65) return "F0";
            if (1.65 < mass && mass <= 1.75) return "A9";
            if (1.75 < mass && mass <= 1.85) return "A7";
            if (1.85 < mass && mass <= 1.95) return "A6";
            if (1.95 < mass && mass <= 2.0) return "A5";

            return "X0";
        }

        private static string getStellarTypeFromTemp(double temp)
        {
            if (temp < 3150) return "M7";
            if (3150 <= temp && temp < 3175) return "M6";
            if (3175 <= temp && temp < 3250) return "M5";
            if (3250 <= temp && temp < 3350) return "M4";
            if (3350 <= temp && temp < 3450) return "M3";
            if (3450 <= temp && temp < 3550) return "M2";
            if (3550 <= temp && temp < 3700) return "M1";
            if (3700 <= temp && temp < 3900) return "M0";
            if (3900 <= temp && temp < 4100) return "K8";
            if (4100 <= temp && temp < 4300) return "K6";
            if (4300 <= temp && temp < 4500) return "K5";
            if (4500 <= temp && temp < 4750) return "K4";
            if (4750 <= temp && temp < 5050) return "K2";
            if (5050 <= temp && temp < 5300) return "K0";
            if (5300 <= temp && temp < 5450) return "G8";
            if (5450 <= temp && temp < 5600) return "G6";
            if (5600 <= temp && temp < 5750) return "G4";
            if (5750 <= temp && temp < 5850) return "G2";
            if (5850 <= temp && temp < 5950) return "G1";
            if (5950 <= temp && temp < 6050) return "G0";
            if (6050 <= temp && temp < 6150) return "F9";
            if (6150 <= temp && temp < 6350) return "F8";
            if (6350 <= temp && temp < 6450) return "F7";
            if (6450 <= temp && temp < 6550) return "F6";
            if (6550 <= temp && temp < 6650) return "F5";
            if (6650 <= temp && temp < 6750) return "F4";
            if (6750 <= temp && temp < 6950) return "F3";
            if (6950 <= temp && temp < 7150) return "F2";
            if (7150 <= temp && temp < 7400) return "F0";
            if (7400 <= temp && temp < 7650) return "A9";
            if (7650 <= temp && temp < 7900) return "A7";
            if (7900 <= temp && temp < 8100) return "A6";
            if (8100 <= temp && temp < 8300) return "A5";

            return "X0";
        }

        public static string getCurrentStage(double starMass, double starAge)
        {
            if (starMass < .1 || starMass > 2)
                throw new Exception("Star Mass is invalid. Please pass a value between .1 and 2 solar masses");

            if (starAge > 14.0)
                throw new Exception("The stellar age is too old.");

            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                {
                    double currAge = starAge;
                    if (starEvolutionTable[i][4] == 0)
                        return "Main Sequence Stage";

                    if (currAge > starEvolutionTable[i][4] && starEvolutionTable[i][4] != 0) //M-Span check.
                    {
                        currAge = currAge - starEvolutionTable[i][4];
                        if (currAge > starEvolutionTable[i][5] && starEvolutionTable[i][5] != 0) //S-Span check.
                        {
                            currAge = currAge - starEvolutionTable[i][5];
                            if (currAge >= starEvolutionTable[i][6] && starEvolutionTable[i][6] != 0) //G-Span check.
                                return "White Dwarf Stage";
                            else //Giant Star Phase
                                return "Giant Star Stage";
                        }
                        else //Subgiant Phase
                            return "Subgiant Star Stage";
                    }
                    else //Main Sequence Phase
                        return "Main Sequence Stage";
                }
            }
            return "FAILOUT STAGE";
        }

        public static string getNextStage(double starMass, double starAge)
        {
            if (starMass < .1 || starMass > 2)
                throw new Exception("Star Mass is invalid. Please pass a value between .1 and 2 solar masses");

            if (starAge > 14.0)
                throw new Exception("The stellar age is too old.");

            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                {
                    double currAge = starAge;
                    if (starEvolutionTable[i][4] == 0 || starEvolutionTable[i][5] == 0)
                        return "White Dwarf Stage";

                    if (currAge > starEvolutionTable[i][4] && starEvolutionTable[i][4] != 0) //M-Span check.
                    {
                        currAge = currAge - starEvolutionTable[i][4];
                        if (currAge > starEvolutionTable[i][5] && starEvolutionTable[i][5] != 0) //S-Span check.
                        {
                            currAge = currAge - starEvolutionTable[i][5];
                            if (currAge >= starEvolutionTable[i][6] && starEvolutionTable[i][6] != 0) //G-Span check.
                                return "";
                            else //Giant Star Phase
                                return "White Dwarf Stage";
                        }
                        else //Subgiant Phase
                            return "Giant Star Stage";
                    }
                    else //Main Sequence Phase
                        return "Subgiant Stage";
                }
            }
            return "FAILOUT STAGE";
        }

        public static double getTimeRemainingOnCurrentStage(double starMass, double starAge)
        {
            if (starMass < .1 || starMass > 2)
                throw new Exception("Star Mass is invalid. Please pass a value between .1 and 2 solar masses");

            if (starAge > 14.0)
                throw new Exception("The stellar age is too old.");

            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                {
                    double currAge = starAge;
                    if (starEvolutionTable[i][4] == 0)
                        return 1300 - currAge;

                    if (currAge > starEvolutionTable[i][4] && starEvolutionTable[i][4] != 0) //M-Span check.
                    {
                        currAge = currAge - starEvolutionTable[i][4];
                        if (currAge > starEvolutionTable[i][5] && starEvolutionTable[i][5] != 0) //S-Span check.
                        {
                            currAge = currAge - starEvolutionTable[i][5];
                            if (currAge >= starEvolutionTable[i][6] && starEvolutionTable[i][6] != 0) //G-Span check.
                                return 0;
                            else //Giant Star Phase
                                return starEvolutionTable[i][6] - currAge;
                        }
                        else //Subgiant Phase
                            return starEvolutionTable[i][5] - currAge;
                    }
                    else //Main Sequence Phase
                        return starEvolutionTable[i][4] - currAge;
                }
            }
            return -1.0;
        }

        public static double getInnerRadius(double starMass)
        {
            double initLumin = 0;
            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                    initLumin = starEvolutionTable[i][2];
            }

            double lumFactor = Math.Sqrt(initLumin);
            if (.1 * starMass > .01 * lumFactor)
                return .1 * starMass;
            else
                return Math.Round(.01 * lumFactor, 3);
        }

        public static double getOuterRadius(double starMass)
        {
            return Math.Round((40 * starMass), 3);
        }

        public static double getSnowLine(double starMass)
        {
            double initLumin = 0;
            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                    initLumin = starEvolutionTable[i][2];
            }

            return (4.85 * Math.Sqrt(initLumin));
        }

        public static double genMinEccentricity(double radius, double eccent)
        {
            return ((1 - eccent) * radius);
        }

        public static double genMaxEccentricity(double radius, double eccent)
        {
            return ((1 + eccent) * radius);
        }

        public static string getColorFromTemp(double starTemp)
        {
            if (starTemp <= 3500)
                return "Red";
            else if (starTemp > 3500 && starTemp <= 4900)
                return "Orange";
            else if (starTemp > 4900 && starTemp <= 6000)
                return "Yellow";
            else if (starTemp > 6000 && starTemp <= 7500)
                return "Yellow-White";
            else if (starTemp > 7500 && starTemp <= 10000)
                return "White";
            else if (starTemp > 10000 && starTemp <= 28000)
                return "Blue-White";
            else if (starTemp > 28000)
                return "Blue";

            return "?!!?!";
        }

        public static string getSeperationType(Dice ourDice, int modifier = 0)
        {
            int diceRoll = ourDice.gurpsRoll(modifier);
            if (diceRoll <= 6)
                return "Very Close";
            else if (diceRoll >= 7 && diceRoll <= 9)
                return "Close";
            else if (diceRoll == 10 || diceRoll == 11)
                return "Moderate";
            else if (diceRoll >= 12 && diceRoll <= 14)
                return "Wide";
            else if (diceRoll >= 15)
                return "Distant";

            return "????";
        }

        public static double getStellarEccentricity(Dice ourDice, int modifier = 0)
        {
            int diceRoll = ourDice.gurpsRoll(modifier);
            if (diceRoll <= 3)
                return 0;
            else if (diceRoll == 4)
                return .1;
            else if (diceRoll == 5)
                return .2;
            else if (diceRoll == 6)
                return .3;
            else if (diceRoll == 7)
                return .4;
            else if (diceRoll == 8)
                return .4;
            else if (diceRoll == 9)
                return .5;
            else if (diceRoll == 10)
                return .5;
            else if (diceRoll == 11)
                return .5;
            else if (diceRoll == 12)
                return .6;
            else if (diceRoll == 13)
                return .6;
            else if (diceRoll == 14)
                return .7;
            else if (diceRoll == 15)
                return .7;
            else if (diceRoll == 16)
                return .8;
            else if (diceRoll == 17)
                return .9;
            else if (diceRoll >= 18)
                return .95;

            return 1;
        }

        public static double getRadiusMultiplier(string seperationType){
            switch (seperationType){
                case "Very Close":
                    return .05;
                case "Close":
                    return .5;
                case "Moderate":
                    return 2;
                case "Wide":
                    return 10;
                case "Distant":
                    return 50;
                default:
                    return 0;
            }
        }

        public static int getEccentModifier(string seperationType)
        {
            if (seperationType == "Very Close")
                return -6;
            else if (seperationType == "Close")
                return -4;
            else if (seperationType == "Moderate")
                return -2;

            return 0;
        }

        public static GasGiantType getGasGiantType(Dice ourDice)
        {
            int diceRoll = ourDice.gurpsRoll();
            if (diceRoll < -10)
                return GasGiantType.NoGasGiant;
            else if (diceRoll == 11 || diceRoll == 12)
                return GasGiantType.ConventionalGasGiant;
            else if (diceRoll == 13 || diceRoll == 14)
                return GasGiantType.EccentricGasGiant;
            else if (diceRoll >= 15)
                return GasGiantType.EpistellarGasGiant;

            return GasGiantType.NoGasGiant;
        }

        public static double getTimeSinceLastStageChange(double starMass, double starAge)
        {
            if (starMass < .1 || starMass > 2)
                throw new Exception("Star Mass is invalid. Please pass a value between .1 and 2 solar masses");

            if (starAge > 14.0)
                throw new Exception("The stellar age is too old.");

            for (int i = 0; i < starEvolutionTable.Length; i++)
            {
                if (starMass == starEvolutionTable[i][0])
                {
                    double currAge = starAge;
                    if (starEvolutionTable[i][4] == 0)
                        return starAge;

                    if (currAge > starEvolutionTable[i][4] && starEvolutionTable[i][4] != 0) //M-Span check.
                    {
                        currAge = currAge - starEvolutionTable[i][4];
                        if (currAge > starEvolutionTable[i][5] && starEvolutionTable[i][5] != 0) //S-Span check.
                        {
                            currAge = currAge - starEvolutionTable[i][5];
                            if (currAge >= starEvolutionTable[i][6] && starEvolutionTable[i][6] != 0) //G-Span check.
                                return (currAge - starEvolutionTable[i][6]);
                            else //Giant Star Phase
                                return currAge;
                        }
                        else //Subgiant Phase
                            return currAge;
                    }
                    else //Main Sequence Phase
                        return currAge;
                }
            }
            return -1.0;
        }

        public static double getBlackBodyTemp(double lumin, double radius)
        {
            double blackbody = 0;
            
            if (radius == 0)
                throw new Exception("Orbital Radius cannot be 0.");

            blackbody = (278 * Math.Pow(lumin, .25)) / Math.Sqrt(radius);

            return blackbody;
        }

        public static double getBlackbodyTempTotal(double[] temps)
        {
            double blackTemp = 0;

            for (int i = 0; i < temps.Length; i++ )
            {
                blackTemp = Math.Pow(temps[i], 4);
            }

            return Math.Pow(blackTemp, .25);
        }

        public static WorldType getWorldType(Dice ourDice, double blackbody, WorldSize worldSize, string parentType, double primaryMass, double systemAge)
        {
            //apply the rule that the diff is split.
            //Now for tiny worlds.
            if (worldSize == WorldSize.Tiny)
            {
                if (blackbody >= 140.5)
                    return WorldType.Rock;
                else
                {
                    if (parentType == "Gas Giant Planet")
                        return WorldType.IceSulfur;
                    else
                        return WorldType.Ice;
                }
            }
            
            //Now for small worlds.
            if (worldSize == WorldSize.Small)
            {
                if (blackbody < 80.5)
                    return WorldType.Hadean;
                else if (blackbody >= 80.5 && blackbody < 140.5)
                    return WorldType.Ice;
                else if (blackbody > 140.5)
                    return WorldType.Rock;
            }

            //now for standard worlds
            if (worldSize == WorldSize.Standard)
            {
                if (blackbody < 80.5)
                    return WorldType.Hadean;

                else if (blackbody >= 80.5 && blackbody < 150.5)
                    return WorldType.Ice;

                else if (blackbody >= 150.5 && blackbody < 230.5)
                {
                    if (primaryMass <= .65)
                        return WorldType.Ammonia;
                    else
                        return WorldType.Ice;
                }

                else if (blackbody > -230.5 && blackbody < 240)
                    return WorldType.Ice;

                else if (blackbody >= 240 && blackbody < 321)
                {
                    int roll = ourDice.gurpsRollWithCappedMod(10, (systemAge / .5));
                    if (roll >= 18)
                        return WorldType.Garden;
                    else
                        return WorldType.Ocean;
                }

                else if (blackbody >= 321 && blackbody < 500.5)
                    return WorldType.Greenhouse;
                else if (blackbody > 500.5)
                    return WorldType.Chthonian;
            }

            if (worldSize == WorldSize.Large)
            {
                if (blackbody < 150.5)
                    return WorldType.Ice;

                else if (blackbody >= 150.5 && blackbody < 230.5)
                {
                    if (primaryMass <= .65)
                        return WorldType.Ammonia;
                    else
                        return WorldType.Ice;
                }

                else if (blackbody > -230.5 && blackbody < 240)
                    return WorldType.Ice;

                else if (blackbody >= 240 && blackbody < 321)
                {
                    int roll = ourDice.gurpsRollWithCappedMod(5, (systemAge / .5));
                    if (roll >= 18)
                        return WorldType.Garden;
                    else
                        return WorldType.Ocean;
                }

                else if (blackbody >= 321 && blackbody < 500.5)
                    return WorldType.Greenhouse;
                else if (blackbody > 500.5)
                    return WorldType.Chthonian;
            }

            return WorldType.None;
        }

        public static PlanetType convertPlanetType(string baseVal)
        {
            if (baseVal == "Asteroid Belt")
                return PlanetType.AsteroidBelt;
            else if (baseVal == "Terrestial Planet")
                return PlanetType.TerrestialPlanet;
            else if (baseVal == "Moon")
                return PlanetType.Moon;
            else if (baseVal == "Gas Giant Planet")
                return PlanetType.GasGiantPlanet;

            return PlanetType.None;
        }

        public static WorldSize convertWorldSize(string baseVal)
        {
            if (baseVal == "Tiny")
                return WorldSize.Tiny;
            else if (baseVal == "Small")
                return WorldSize.Small;
            else if (baseVal == "Standard")
                return WorldSize.Standard;
            else if (baseVal == "Medium")
                return WorldSize.Medium;
            else if (baseVal == "Large")
                return WorldSize.Large;

            return WorldSize.None;
        }

        public static int getTerrestialMajorMoons(Dice ourDice, WorldSize planetSize, double primaryDistance)
        {
            int mod = -4;

            if (primaryDistance <= 0.5)
                return 0;
            if (primaryDistance > .5 && primaryDistance <= .75)
                mod = mod -3;
            if (primaryDistance > .75 && primaryDistance <= 1.5)
                mod = mod - 1;

            if (planetSize == WorldSize.Tiny) mod = mod - 2;
            if (planetSize == WorldSize.Small) mod = mod - 1;
            if (planetSize == WorldSize.Large) mod = mod + 1;

            return ourDice.rngGTZero(1,6,mod);
         }

        public static int getTerrestialMoonlets(Dice ourDice, WorldSize planetSize, double primaryDistance)
        {
            int mod = -2;

            if (primaryDistance <= 0.5)
                return 0;
            if (primaryDistance > .5 && primaryDistance <= .75)
                mod = mod - 3;
            if (primaryDistance > .75 && primaryDistance <= 1.5)
                mod = mod - 1;

            if (planetSize == WorldSize.Tiny) mod = mod - 2;
            if (planetSize == WorldSize.Small) mod = mod - 1;
            if (planetSize == WorldSize.Large) mod = mod + 1;

            return ourDice.rngGTZero(1,6,mod);
        }

        public static int getGasGiantRingMoons(Dice ourDice, double primaryDistance)
        {
            int mod = 0;
            if (primaryDistance <= .1)
                mod = -10;
            else if (primaryDistance > .1 && primaryDistance <= .5)
                mod = -8;
            else if (primaryDistance > .5 && primaryDistance <= .75)
                mod = -6;
            else if (primaryDistance > .75 && primaryDistance <= 1.5)
                mod = -3;

            return ourDice.rngGTZero(2, 6, mod);
        }

        public static int getGasGiantCapturedMoons(Dice ourDice, double primaryDistance)
        {
            int mod = 0;

            if (primaryDistance <= .1)
                return 0;
            else if (primaryDistance > .1 && primaryDistance <= .5)
                mod = -5;
            else if (primaryDistance > .5 && primaryDistance <= .75)
                mod = -4;
            else if (primaryDistance > .75 && primaryDistance <= 1.5)
                mod = -1;

            return ourDice.rngGTZero(1, 6, mod);
        }

        public static int getGasGiantMajorMoons(Dice ourDice, double primaryDistance)
        {
            int mod = 0;

            if (primaryDistance <= .5)
                return 0;
            else if (primaryDistance > .5 && primaryDistance <= .75)
                mod = -5;
            else if (primaryDistance > .75 && primaryDistance <= 1.5)
                mod = -4;
            else if (primaryDistance > 1.5 && primaryDistance <= 3)
                mod = mod - 1;

            return ourDice.rngGTZero(1, 6, mod);
        }

        public static double rollAtmosphere(Dice ourDice)
        {
            double variance = .05;
            return (ourDice.gurpsRoll() / 10.0 + ourDice.rollInRange(-1 * variance, variance));
        }

       

        public static void addMarginalConditions(Planet curr, Dice ourDice)
        {
            int roll = ourDice.gurpsRoll();
            if (roll == 3 || roll == 4)
            {
                if (ourDice.dblProb() < .99)
                {
                    curr.AddAtmosphericCondition(AtmosphericConditions.Chlorine);
                    curr.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);
                    curr.AddAtmosphericCondition(AtmosphericConditions.LocallyLethallyToxic);
                }
                else
                {
                    curr.AddAtmosphericCondition(AtmosphericConditions.Flourine);
                    curr.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);
                    curr.AddAtmosphericCondition(AtmosphericConditions.LocallyLethallyToxic);
                }
            }

            else if (roll == 5 || roll == 6)
            {
                curr.AddAtmosphericCondition(AtmosphericConditions.SulfurCompounds);
                curr.AddAtmosphericCondition(AtmosphericConditions.MildlyToxic);
                curr.AddAtmosphericCondition(AtmosphericConditions.LocallyHighlyToxic);
            }

            else if (roll == 7)
            {
                curr.AddAtmosphericCondition(AtmosphericConditions.NitrogenCompounds);
                curr.AddAtmosphericCondition(AtmosphericConditions.MildlyToxic);
                curr.AddAtmosphericCondition(AtmosphericConditions.LocallyHighlyToxic);
            }

            else if (roll == 8 || roll == 9)
            {
                curr.AddAtmosphericCondition(AtmosphericConditions.OrganicToxins);
                if (ourDice.dblProb() < .85)
                    curr.AddAtmosphericCondition(AtmosphericConditions.MildlyToxic);
                else
                    curr.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);
            }

            else if (roll == 10 || roll == 11)
            {
                curr.AddAtmosphericCondition(AtmosphericConditions.LowOxygen);
                curr.AddAtmosphericCondition(AtmosphericConditions.EffectiveOnePressureClassDown);
            }

            else if (roll == 12 || roll == 13)
            {
                curr.AddAtmosphericCondition(AtmosphericConditions.Pollutants);
                if (ourDice.dblProb() > .95)
                    curr.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);
                else
                    curr.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);
            }

            else if (roll == 14)
            {
                curr.AddAtmosphericCondition(AtmosphericConditions.HighCarbonDioxide);
                if (ourDice.dblProb() > .81)
                    curr.AddAtmosphericCondition(AtmosphericConditions.MildlyToxic);
            }

            else if (roll == 15 || roll == 16)
            {
                curr.AddAtmosphericCondition(AtmosphericConditions.HighOxygen);
                curr.AddAtmosphericCondition(AtmosphericConditions.EffectiveOnePressureClassUp);
                if (ourDice.dblProb() > .81){
                    curr.AddAtmosphericCondition(AtmosphericConditions.FlammabilityOneClassUp);
                    curr.AddAtmosphericCondition(AtmosphericConditions.MildlyToxic);
                }
            }

            else if (roll == 17 || roll == 18)
                curr.AddAtmosphericCondition(AtmosphericConditions.InertGases);
        }
    }
}
