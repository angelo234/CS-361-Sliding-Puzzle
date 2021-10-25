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

namespace CS_361_Sliding_Puzzle
{
    /// <summary>
    /// Interaction logic for ImageSelectionView.xaml
    /// </summary>
    public partial class ImageSelectionView : UserControl, ISwitchable
    {
        public ImageSelectionView()
        {
            InitializeComponent();
        }

        public void OnViewSwitched(object state)
        {
            throw new NotImplementedException();
        }
    }
}
