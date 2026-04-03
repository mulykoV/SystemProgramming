using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MovieApp
{
    public class TreeItem
    {
        public required string Header { get; set; }
        public ObservableCollection<TreeItem> Nodes { get; set; } = new();
    }   

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DisplayMovie_Click(object sender, RoutedEventArgs e)
        {
            var myMovie = new Movie("Inception", 2010, 8.8, new List<string> { "Sci-Fi", "Action", "Thriller" });
            var rootNodes = new ObservableCollection<TreeItem>();
            var movieRoot = new TreeItem { Header = $"Кінофільм: {myMovie.Title}" };

            AddPropertyToTree(movieRoot, "Title", "string", myMovie.Title);
            AddPropertyToTree(movieRoot, "Year", "int", myMovie.ReleaseYear.ToString());
            AddPropertyToTree(movieRoot, "Rating", "double", myMovie.Rating.ToString());
            
            var genreNode = new TreeItem { Header = $"Genres (List, Count: {myMovie.Genres.Count})" };
            foreach (var genre in myMovie.Genres)
            {
                genreNode.Nodes.Add(new TreeItem { Header = $"- {genre}" });
            }
            movieRoot.Nodes.Add(genreNode);

            rootNodes.Add(movieRoot);
            
            
            MyTreeView.ItemsSource = rootNodes;
        }

        private void AddPropertyToTree(TreeItem parent, string name, string type, string value)
        {
            parent.Nodes.Add(new TreeItem { Header = $"[{type}] {name}: {value}" });
        }
    }
}