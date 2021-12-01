# CS-361-Sliding-Puzzle
This is a C# WPF Application based Sliding Puzzle game. 

This game is just a classic sliding puzzle game, but you can choose an image to be used for the game in two different ways. You can either pick an image from your computer locally or you can choose an image from a website by pasting in the website's URL into the game, which uses my teammate's Web Image Scrapper microservice to scrape the image from the desired website. 

This GitHub repository includes GitHub releases (executables of the game) so you can download and run the game right away without having to compile the source code. 

Before running my game you must do the following (allows for scraping an image from a inputted website URL):
  1. Download the my teammate's microservice from this GitHub repository as a zip file https://github.com/Manbir5/Image-Web-Scrapper
  2. Extract the zip file contents in a folder called `Image-Web-Scrapper-main` on your Desktop.
  3. Run the python script using `py image_downloader.py` and keep it running while my game is open (if you don't have Python installed, download it from here https://www.python.org/downloads/)
