﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg; *.png)|*.jpg;*.png";

            bool? success = openFileDialog.ShowDialog();

            if (success == true)
            {
                System.Drawing.Image image = null;

                try
                {
                    image = System.Drawing.Image.FromFile(openFileDialog.FileName);    
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Error with creating image from filename.");
                }

                if (image != null)
                {
                    // If image was selected, switch to the Game view
                    // and pass in the image file
                    ViewSwitcher.Switch(new GameView(image));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Image is null");
                }

                
            }

            
        }

        private void RandomImageButton_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();

            // Get images from folder containing bunch of images in project directory

            string picsDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/pics";

            string[] files = Directory.GetFiles(picsDir);

            // Choose random image from directory

            string chosenFile = files[rand.Next(0, files.Length)];

            System.Drawing.Image image = null;

            try
            {
                image = System.Drawing.Image.FromFile(chosenFile);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error with creating image from filename.");
            }

            if (image != null)
            {
                // If image found then switch to the Game view
                // and pass in the image file
                ViewSwitcher.Switch(new GameView(image));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Image is null");
            }
        } 
    }
}
