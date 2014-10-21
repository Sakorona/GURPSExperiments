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

namespace GenerateSystemOrbits
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

        private void btnGenerateOrbits_Click(object sender, RoutedEventArgs e)
        {
            double star1Mass, FZStar1Inner, FZStar2Inner, FZStar1Outer, FZStar2Outer, FZStar3Inner;
            double FZStar3Outer, FZStar4Inner, FZStar4Outer;

            if (!(Double.TryParse(txtStar1Mass.Text, out star1Mass)))
                return;
            if (!(Double.TryParse(txtFZ1Inner.Text, out FZStar1Inner)))
                return;
            if (!(Double.TryParse(txtFZ1Outer.Text, out FZStar1Outer)))
                return;
            if (!(Double.TryParse(txtFZ2Inner.Text, out FZStar2Inner)))
                return;
            if (!(Double.TryParse(txtFZ2Outer.Text, out FZStar2Outer)))
                return;
            if (!(Double.TryParse(txtFZ3Inner.Text, out FZStar3Inner)))
                return;
            if (!(Double.TryParse(txtFZ3Outer.Text, out FZStar3Outer)))
                return;
            if (!(Double.TryParse(txtFZ4Inner.Text, out FZStar4Inner)))
                return;
            if (!(Double.TryParse(txtFZ4Outer.Text, out FZStar4Outer)))
                return;

            double innerRadius = StarReference.getInnerRadius(star1Mass);
            double outerRadius = StarReference.getOuterRadius(star1Mass);

            //first, let's get what giant type we are using


        }
    }
}
