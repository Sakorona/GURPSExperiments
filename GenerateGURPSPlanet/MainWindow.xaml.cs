﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
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

namespace GenerateGURPSPlanet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> worldSize;
        public ObservableCollection<string> planetType;
        public ObservableCollection<string> parentType;
        protected Dice ourDice;

        public MainWindow()
        {
            InitializeComponent();
            worldSize = new ObservableCollection<string>();
            worldSize.Add("Tiny");
            worldSize.Add("Small");
            worldSize.Add("Standard");
            worldSize.Add("Large");

            parentType = new ObservableCollection<string>();
            parentType.Add("Star");
            parentType.Add("Terrestial Planet");
            parentType.Add("Gas Giant Planet");

            planetType = new ObservableCollection<string>();
            planetType.Add("Moon");
            planetType.Add("Terrestial");
            planetType.Add("Gas Giant");

            cmbParenType.ItemsSource = parentType;
            cmbWorldSize.ItemsSource = worldSize;
            cmbWorldType.ItemsSource = planetType;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            double star1Lumin, star2Lumin, star3Lumin, star4Lumin, star5Lumin, star1Distance, star2Distance, star3Distance, star4Distance, 
                star5Distance, parentMass, primaryMass, systemAge;

            if (!(Double.TryParse(txtStar1Luminosity.Text, out star1Lumin)))
                return;
            if (!(Double.TryParse(txtStar1OrbitalDistance.Text, out star1Distance)))
                return;
            if (!(Double.TryParse(txtStar2Luminosity.Text, out star2Lumin)))
                return;
            if (!(Double.TryParse(txtStar2OrbitalDistance.Text, out star2Distance)))
                return;
            if (!(Double.TryParse(txtStar3Luminosity.Text, out star3Lumin)))
                return;
            if (!(Double.TryParse(txtStar3OrbitalDistance.Text, out star3Distance)))
                return;
            if (!(Double.TryParse(txtStar4Luminosity.Text, out star4Lumin)))
                return;
            if (!(Double.TryParse(txtStar4OrbitalDistance.Text, out star4Distance)))
                return;
            if (!(Double.TryParse(txtStar5Luminosity.Text, out star5Lumin)))
                return;
            if (!(Double.TryParse(txtStar5OrbitalDistance.Text, out star5Distance)))
                return;
            if (!(Double.TryParse(txtParentMass.Text, out parentMass)))
                return;
            if (!(Double.TryParse(txtPrimaryMass.Text, out primaryMass)))
                return;
            if (!(Double.TryParse(txtSystemAge.Text, out systemAge)))
                return;

            //calc blackbody.
            double[] blackbody = new double[5];
            if (star1Distance != 0)
                blackbody[0] = StarReference.getBlackBodyTemp(star1Lumin, star1Distance);
            if (star2Distance != 0)
                blackbody[1] = StarReference.getBlackBodyTemp(star2Lumin, star2Distance);
            if (star3Distance != 0)
                blackbody[2] = StarReference.getBlackBodyTemp(star4Lumin, star3Distance);
            if (star4Distance != 0)
                blackbody[3] = StarReference.getBlackBodyTemp(star4Lumin, star4Distance);
            if (star5Distance != 0)
                blackbody[4] = StarReference.getBlackBodyTemp(star5Lumin, star5Distance);
            
            double currBlackBody = StarReference.getBlackbodyTempTotal(blackbody);

            //now that we have the blackbody, let's get the world type.
            string selectedWorldSize = cmbWorldSize.SelectedItem.ToString();
            string selectedWorldType = cmbWorldType.SelectedItem.ToString();
            string selectedParentType = cmbParenType.SelectedItem.ToString();

            WorldType ourType;
            if (selectedWorldType != "Gas Giant")
                ourType = StarReference.getWorldType(ourDice, currBlackBody, selectedWorldSize, selectedWorldType,
                                                               selectedParentType, primaryMass, systemAge);
            else
                ourType = WorldType.GasGiant;

            //WorldType determined. Now to the next step: Atmosphere.
            
        }
    }
}