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

namespace WpfApp1
{
    using System.IO;
    using System.Reflection;

    using Microsoft.Win32;

    using Newtonsoft.Json;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Classification> _classifications;

        private Classification _currentClassification;

        public MainWindow()
        {
            InitializeComponent();
            _classifications = new List<Classification>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var pathToFiles = @"C:\Users\Alex\Desktop\images";
            var directories = Directory.EnumerateDirectories(pathToFiles).ToList();
            var classes = directories.Select(p => Path.GetFileName(p)).ToList();

            var files = new List<String>();
            foreach (var directory in directories)
            {
                files.AddRange(Directory.EnumerateFiles(directory));
            }

            files.Shuffle();
            files = files.Take(200).ToList();

            foreach (var file in files)
            {
                var fileNameWithSymbolFolder = file.Substring(pathToFiles.Length+1);
                var className = fileNameWithSymbolFolder.Substring(0, fileNameWithSymbolFolder.Length - new FileInfo(file).Name.Length - 1);
                var expectedType = classes.IndexOf(className) + 1;
                var classification = new Classification(file, expectedType);
                _classifications.Add(classification);
            }

            LoadNextImage();
        }

        private void LoadNextImage()
        {
            var i = 0;
            foreach (var classification in _classifications)
            {
                i++;
                if (classification.UserClassification == Category.Unclassified)
                {
                    image.Source = new BitmapImage(new Uri(classification.FilePath));
                    _currentClassification = classification;
                    labelProgress.Content = $"{i}/{_classifications.Count}";
                    return;
                }
            }

            image.Source = null;
            MessageBox.Show(this, "No more images to classify. \nThank you for Participation :-)");
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void ClassifyAs(int classNumber)
        {
            _currentClassification.UserClassification = classNumber;
            LoadNextImage();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var json = JsonConvert.SerializeObject(_classifications);
            var resultFile = $"results{DateTime.Now.ToString("hh-mm-ss")}.json";
            File.WriteAllText(resultFile, json);
            MessageBox.Show(this, $"File {resultFile} saved.");
                
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (dialog.ShowDialog(this).Value)
                {
                    var json = File.ReadAllText(dialog.FileName);
                    _classifications = JsonConvert.DeserializeObject<List<Classification>>(json);
                    LoadNextImage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while loading json");
            }
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Undo();
        }

        private void Undo()
        {
            for (int i = 0; i < _classifications.Count; i++)
            {
                if (_classifications[i].UserClassification == Category.Unclassified && i > 0)
                {
                    textBox.Text = _classifications[i - 1].UserClassification.ToString();
                    _classifications[i - 1].UserClassification = Category.Unclassified;
                    LoadNextImage();
                    return;
                }
            }
        }

        private void statisticsButton_Click(object sender, RoutedEventArgs e)
        {
            var totalClassifications = _classifications.Where(c => c.UserClassification != Category.Unclassified).Count();
            var wrongClassifications = _classifications.Where(c => c.UserClassification != Category.Unclassified && c.UserClassification != c.ExpectedClassification);
            var wrongClassificationsCount = wrongClassifications.Count();
            var correctClassifications = _classifications.Where(c => c.UserClassification != Category.Unclassified && c.UserClassification == c.ExpectedClassification).Count();

            string message = $"Total classifications: {totalClassifications}\n" + 
                $"Correct classifications: {correctClassifications}\n" + 
                $"Wrong classifications: {wrongClassificationsCount}\n" + 
                $"Accuracy: {correctClassifications * 100.0 / totalClassifications}%\n\n" + 
                $"Incorrect files: {string.Join(" ", wrongClassifications.Select(c => Path.GetFileName(c.FilePath)))}" 
                
                ;
            MessageBox.Show(this, message);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ClassifyImage();
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                Undo();
            }
            else if (e.Key == Key.Enter)
            {
                ClassifyImage();
                textBox.Clear();
            }
        }

        private void ClassifyImage()
        {
            try
            {
                var classNumber = int.Parse(textBox.Text);
                ClassifyAs(classNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ungültige Klassen-nummber: " + ex.Message);
            }
        }
    }

    internal class Category
    {
        public const int Unclassified = -1;

        public int CurrentClass { get; set; } = Unclassified;
    }

    internal class Classification
    {
        public string FilePath { get; set;  }

        public int ExpectedClassification { get; set; }

        public int UserClassification { get; set; }

        public Classification(string file, int expectedClassification)
        {
            this.FilePath = file;
            this.ExpectedClassification = expectedClassification;
            this.UserClassification = Category.Unclassified;
        }
    }

    public static class ShuffleExtension
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
