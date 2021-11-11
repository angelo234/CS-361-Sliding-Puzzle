﻿using System;
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
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl, ISwitchable
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        public void OnViewSwitched(object state)
        {
            
        }

        // Switch to the Image Selection view

        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            ViewSwitcher.Switch("image_selection_view");
        }

        // Exit program
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
