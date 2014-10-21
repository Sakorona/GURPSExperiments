using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TwilightShards.GURPSUtil;
using TwilightShards.genLibrary;

namespace GenerateGURPSOrbits
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dice ourDice;

        public MainWindow()
        {
            ourDice = new Dice();
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            double star1Mass, star2Mass, star3Mass;
            bool secondStar = false, thirdStar = false, isGardenWorld = false;

            double star2Dist, star2Eccent, star3Dist, star3Eccent;            
            string star2Sep, star3Sep;

            string desc = "";

            if (!(Double.TryParse(txtStar1Mass.Text, out star1Mass)))
                return;
            if (!(Double.TryParse(txtStar2Mass.Text, out star2Mass)))
                return;
            if (!(Double.TryParse(txtStar3Mass.Text, out star3Mass)))
                return;

            if (chkIsGarden.IsChecked == null || chkIsGarden.IsChecked == false)
                isGardenWorld = false;
            if (chkIsGarden.IsChecked == true)
                isGardenWorld = true;

            if (chkSecondStarExist.IsChecked == null || chkSecondStarExist.IsChecked == false)
                secondStar = false;
            if (chkSecondStarExist.IsChecked == true)
                secondStar = true;

            if (chkThirdStarExist.IsChecked == null || chkThirdStarExist.IsChecked == false)
                thirdStar = false;
            if (chkThirdStarExist.IsChecked == true)
                thirdStar = true;
            
            if (secondStar && !thirdStar)
            {
                if (isGardenWorld)
                    star2Sep = StarReference.getSeperationType(ourDice, 4);
                else
                    star2Sep = StarReference.getSeperationType(ourDice);

                star2Dist = ourDice.rng(2, 6) * StarReference.getRadiusMultiplier(star2Sep);
                star2Eccent = StarReference.getStellarEccentricity(ourDice, StarReference.getEccentModifier(star2Sep));


                desc = desc + "The second star orbits at " + star2Dist + " AU ( " + star2Sep + " ). Eccentricity is " +
                            star2Eccent + " with periastron " + StarReference.genMinEccentricity(star2Dist, star2Eccent) +
                            " AU and apastron " + StarReference.genMaxEccentricity(star2Dist, star2Eccent) + " AU " + Environment.NewLine;

                if (star2Sep == "Distant")
                {
                    int diceRoll = ourDice.gurpsRoll();
                    if (diceRoll >= 11)
                    {
                        desc = desc + Environment.NewLine + "Generate a companion star here." + Environment.NewLine;
                    }
                }
            }
            

            //generate stellar spacing first.
            if (thirdStar)
            {
                if (isGardenWorld)
                    star2Sep = StarReference.getSeperationType(ourDice, 4);
                else
                    star2Sep = StarReference.getSeperationType(ourDice);

                star2Dist = ourDice.rng(2, 6) * StarReference.getRadiusMultiplier(star2Sep);
                star2Eccent = StarReference.getStellarEccentricity(ourDice, StarReference.getEccentModifier(star2Sep));


                desc = desc + "The second star orbits at " + star2Dist + " AU ( " + star2Sep + " ). Eccentricity is " +
                            star2Eccent + " with periastron " + StarReference.genMinEccentricity(star2Dist, star2Eccent) +
                            " AU and apastron " + StarReference.genMaxEccentricity(star2Dist, star2Eccent) + " AU " + Environment.NewLine;
                
                if (isGardenWorld)
                    star3Sep = StarReference.getSeperationType(ourDice, 10);
                else
                    star3Sep = StarReference.getSeperationType(ourDice, 6);

                if (star2Dist >= 600)
                    star3Dist = star2Dist + ourDice.rollInIntRange(1,200);
                else
                {
                    do
                    {
                        star3Dist = ourDice.rng(2, 6) * StarReference.getRadiusMultiplier(star3Sep);
                    } while (star3Dist <= star2Dist);
                }

                star3Eccent = StarReference.getStellarEccentricity(ourDice, StarReference.getEccentModifier(star3Sep));

                desc = desc + "The third star orbits at " + star3Dist + " AU ( " + star3Sep + " ). Eccentricity is " +
                            star3Eccent + " with periastron " + StarReference.genMinEccentricity(star3Dist, star3Eccent)  +
                            " AU and apastron " + StarReference.genMaxEccentricity(star3Dist, star3Eccent) + " AU " +  Environment.NewLine;

                if (star3Sep == "Distant")
                {
                    int diceRoll = ourDice.gurpsRoll();
                    if (diceRoll >= 11)
                    {
                        desc = desc + Environment.NewLine + "Generate a companion star here." + Environment.NewLine;

                    }
                }
            }


            txtOutput.Text = desc;

        }
    }
}
