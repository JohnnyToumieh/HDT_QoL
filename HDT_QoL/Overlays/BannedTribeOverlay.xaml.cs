using System.Windows.Controls;

namespace HDT_QoL
{
    /// <summary>
    /// Interaction logic for BannedTribeOverlay.xaml
    /// </summary>
    public partial class BannedTribeOverlay : UserControl
    {
        public BannedTribeOverlay()
        {
            InitializeComponent();
        }

        internal void UpdateTribe(string bannedTribe)
        {
            BannedTribeText.Text = string.Format("Banned: {0}", bannedTribe);
        }
    }
}