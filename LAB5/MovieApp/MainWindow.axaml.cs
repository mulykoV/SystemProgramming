using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

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
            var myMovie = new Movie("Inception", 2010, 8.8,
                new List<string> { "Sci-Fi", "Action", "Thriller" });

            var rootNodes = new ObservableCollection<TreeItem>();
            var movieRoot = new TreeItem { Header = $"Кінофільм: {myMovie.Title}" };

            BuildTreeFromObject(movieRoot, myMovie);

            rootNodes.Add(movieRoot);
            MyTreeView.ItemsSource = rootNodes;
        }

        private void BuildTreeFromObject(TreeItem parent, object obj)
        {
            if (obj == null) return;

            var type = obj.GetType(); 

            foreach (PropertyInfo prop in type.GetProperties())
            {
                var value = prop.GetValue(obj);
                var propType = prop.PropertyType;

                if (typeof(IEnumerable).IsAssignableFrom(propType) && propType != typeof(string))
                {
                    var collectionNode = new TreeItem
                    {
                        Header = $"{prop.Name} (Collection)"
                    };

                    if (value is IEnumerable enumerable)
                    {
                        foreach (var item in enumerable)
                        {
                            collectionNode.Nodes.Add(new TreeItem
                            {
                                Header = $"- {item}"
                            });
                        }
                    }

                    parent.Nodes.Add(collectionNode);
                }
                else
                {
                    parent.Nodes.Add(new TreeItem
                    {
                        Header = $"[{propType.Name}] {prop.Name}: {value}"
                    });
                }
            }
        }
    }
}