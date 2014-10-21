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

using TwilightShards.genLibrary;
using TwilightShards.GURPSUtil;

namespace RollGURPSStar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected Dice ourDice;

        public MainWindow()
        {
            ourDice = new Dice();
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            double massUpper, massLower, maxStarAge;
            
            if (!(Double.TryParse(txtStarMassLL.Text, out massLower)))
               massLower = .1;

            if (!(Double.TryParse(txtStarMassUL.Text, out massUpper)))
                massUpper = 2.0;

            if (!(Double.TryParse(txtStarAge.Text, out maxStarAge)))
                maxStarAge = 0;

            if (massLower < .1)
                massLower = .1;

            if (massUpper > 2.0)
                massUpper = 2;

            double starMass, starAge;
            do
            {
                starMass = StarReference.rollStellarMass(ourDice);
            } while (starMass > massUpper || starMass < massLower);

            if (maxStarAge == 0)
                starAge = StarReference.genSystemAge(ourDice);
            else
                starAge = maxStarAge;


            //we have the age and mass. Now to get the luminosity.
            double starLumin = StarReference.getLuminosity(starMass, starAge);
            double starTemp = StarReference.getTemperature(ourDice, starMass, starAge);
            double starRadius = StarReference.getRadius(ourDice, starMass, starAge, starTemp, starLumin);

            string currStage = StarReference.getCurrentStage(starMass, starAge);

            string starDesc = "Star has a mass of " + starMass + " solar masses. The effective temperature is " + Math.Round(starTemp, 3) + "K." +
                                  Environment.NewLine + "The luminosity is " + Math.Round(starLumin, 3) + " solar luminosities. It has a radius of " +
                                  Math.Round(starRadius, 3) + " AU and spectral class of " + StarReference.getSpectralType(starMass, starTemp, starAge) + "." +
                                  Environment.NewLine + "The color of the star is " + StarReference.getColorFromTemp(starTemp) + "." +
                                  Environment.NewLine + "The current age of the star is " + starAge + " GYa. " + Environment.NewLine +
                                  "The star is in the stellar evolution stage of " + currStage + " for " + 
                                  StarReference.getTimeSinceLastStageChange(starMass, starAge) + " GYa";

            if (currStage != "White Dwarf Stage")
            {
                starDesc = starDesc + ", with " + StarReference.getTimeRemainingOnCurrentStage(starMass, starAge) + " GYa remaining until " +
                           StarReference.getNextStage(starMass, starAge);
            }

            //spacing. Now let's add system creation details.
            starDesc = starDesc + ".";
            starDesc = starDesc + Environment.NewLine + Environment.NewLine;

            starDesc = starDesc + "Creation zone is between " + StarReference.getInnerRadius(starMass) + " AU and " + StarReference.getOuterRadius(starMass)
                       + " AU. The snow line is " + Math.Round(StarReference.getSnowLine(starMass),3) + " AU.";

            txtStarDetails.Text = starDesc;
                                  
        }
    }
}
