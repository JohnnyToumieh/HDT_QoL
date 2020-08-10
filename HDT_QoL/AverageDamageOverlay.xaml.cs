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

namespace HDT_QoL
{
    /// <summary>
    /// Interaction logic for DamageOverlay.xaml
    /// </summary>
    public partial class AverageDamageOverlay : UserControl
    {
        public AverageDamageOverlay()
        {
            InitializeComponent();
        }

        private string _averageDamageDisplay;
        public string AverageDamageDisplay
        {
            get => _averageDamageDisplay;
            set
            {
                _averageDamageDisplay = value;
                //OnPropertyChanged();
            }
        }
    }
}
