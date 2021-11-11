﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

            WebsiteURLGrid.Visibility = Visibility.Hidden;
            InternetImageButton.Visibility = Visibility.Visible;
        }

        private void LocalImageButton_Click(object sender, RoutedEventArgs e)
        {
            //string picsDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\pics";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg; *.png)|*.jpg;*.png";
            //openFileDialog.InitialDirectory = picsDir;

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
                    ViewSwitcher.Switch("game_view", image);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Image is null");
                }
            }
        }

        private void InternetImageButton_Click(object sender, RoutedEventArgs e)
        {
            WebsiteURLGrid.Visibility = Visibility.Visible;
            InternetImageButton.Visibility = Visibility.Hidden;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ViewSwitcher.Switch("main_menu");
        }

       

        private void OKURLButton_Click(object sender, RoutedEventArgs e)
        {
            string url = URLTextBox.Text;

            string serviceFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Image-Web-Scrapper-main\\";
            string servicePipeFilePath = serviceFolderPath + "images.txt";
            
            File.AppendAllText(servicePipeFilePath, Environment.NewLine + url);

            long fileLength = new System.IO.FileInfo(servicePipeFilePath).Length;

            string fileName = null;

            bool gotResponse = false;

            for (int i = 0 ; i < 5; i++)
            {
                Thread.Sleep(1000);
                
                if (new System.IO.FileInfo(servicePipeFilePath).Length != fileLength)
                {
                    fileName = File.ReadLines(servicePipeFilePath).Last();

                    gotResponse = true;

                    break;
                }
            }

            if (!gotResponse)
            {
                MessageBox.Show("Didn't get a response from Web Scrapper service in time. Please make sure the Web Scrapper service is running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            System.Diagnostics.Debug.WriteLine(fileName);

            System.Drawing.Image image = null;

            try
            {
                image = System.Drawing.Image.FromFile(serviceFolderPath + "images\\" + fileName + ".jpg");
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error with creating image from filename.");

                MessageBox.Show("Unable to retrieve an image from the website. Please try a different website.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (image != null)
            {
                // If image was selected, switch to the Game view
                // and pass in the image file
                ViewSwitcher.Switch("game_view", image);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Image is null");
            }

            URLTextBox.Text = "";
        }

        /*
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
        */
    }
}
