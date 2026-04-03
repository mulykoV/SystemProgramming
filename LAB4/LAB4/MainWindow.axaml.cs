using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LAB4;

public partial class MainWindow : Window
{
    private CancellationTokenSource? _cts;

    public MainWindow()
    {
        InitializeComponent();
        
        // Підключаємо події до вже існуючих полів, які згенерувала Avalonia
        this.btnStart.Click += OnStartClick;
        this.btnCancel.Click += OnCancelClick;
    }

    public async void OnStartClick(object? sender, RoutedEventArgs e)
    {
        btnStart.IsEnabled = false;
        btnCancel.IsEnabled = true;
        txtResult.Text = "Обчислення...";
        progBar.Value = 0;

        _cts = new CancellationTokenSource();
       IProgress<int> progress = new Progress<int>(v => {
    progBar.Value = v;
    lblStatus.Text = $"Виконано: {v}%";
});
        try {
            long result = await Task.Run(() => {
                long sum = 0;
                for (int i = 1; i <= 100; i++) {
                    _cts.Token.ThrowIfCancellationRequested();
                    Thread.Sleep(50); // Імітація роботи
                    sum += i;
                    progress.Report(i);
                }
                return sum;
            }, _cts.Token);

            txtResult.Text = $"Результат: {result}";
            lblStatus.Text = "Статус: Успішно!";
        }
        catch (OperationCanceledException) {
            lblStatus.Text = "Операцію скасовано.";
            txtResult.Text = "Скасовано";
            progBar.Value = 0;
        }
        finally {
            btnStart.IsEnabled = true;
            btnCancel.IsEnabled = false;
            _cts?.Dispose();
            _cts = null;
        }
    }

    public void OnCancelClick(object? sender, RoutedEventArgs e) => _cts?.Cancel();
}