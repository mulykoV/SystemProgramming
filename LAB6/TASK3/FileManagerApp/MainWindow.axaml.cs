using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FileManagerApp; 

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        string defaultPath = "/home/volodymyr/diploma_project";
        LoadDirectory(Directory.Exists(defaultPath) ? defaultPath : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
    }

    private void LoadDirectory(string? path)
    {
        if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) return;
        
        PathBox.Text = path;
        var items = new List<string>();
        try
        {
            foreach (var dir in Directory.GetDirectories(path))
                items.Add("📁 " + Path.GetFileName(dir));
            foreach (var file in Directory.GetFiles(path))
                items.Add("📄 " + Path.GetFileName(file));
            
            FileListView.ItemsSource = items;
        }
        catch (Exception ex) { FileContentView.Text = "Помилка: " + ex.Message; }
    }

    private void FileListView_DoubleTapped(object sender, TappedEventArgs e)
    {
        if (FileListView.SelectedItem is string selected && !string.IsNullOrEmpty(PathBox.Text))
        {
            string name = selected.Substring(3); 
            string newPath = Path.Combine(PathBox.Text, name);

            if (Directory.Exists(newPath)) 
                LoadDirectory(newPath);
            else if (File.Exists(newPath))
            {
                try {
                    FileContentView.Text = File.ReadAllText(newPath);
                } catch {
                    FileContentView.Text = "Неможливо прочитати файл.";
                }
            }
        }
    }

    private void GoUp_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(PathBox.Text))
        {
            var parent = Directory.GetParent(PathBox.Text);
            if (parent != null) LoadDirectory(parent.FullName);
        }
    }

    private void Refresh_Click(object sender, RoutedEventArgs e) => LoadDirectory(PathBox.Text);
}