using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ExampleGUI.Commands;
using log4net;

namespace ExampleGUI
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IConsoleLog consoleLog;
        public IAsyncCommand StartButtonCommand { get; set; }
        public ICommand StopButtonCommand { get; set; }
        private CancellationTokenSource ctSource { get; set; }

        public MainWindowViewModel(ILog log)
        {
            consoleLog = new ConsoleLog(this, log);
            StartButtonCommand = new AwaitableDelegateCommand(StartButtonExecute);
            StopButtonCommand = new DelegateCommand(StopButtonExecute, () => Running);
        }
        
        private async Task StartButtonExecute()
        {
            consoleLog.WriteLine("Starting");
            Running = true;
            ctSource = new CancellationTokenSource();
            await Task.Run(async () =>
            {
                var random = new Random();
                while (!ctSource.Token.IsCancellationRequested)
                {
                    var delayMs = random.Next(1000, 2000);
                    await Task.Delay(delayMs);
                    consoleLog.WriteLine($"Delayed {delayMs}ms");
                }
            });
            consoleLog.WriteLine("Stopped");
        }

        private void StopButtonExecute()
        {
            consoleLog.WriteLine("Stopping");
            ctSource?.Cancel();
            Running = false;
        }

        private bool running;
        public bool Running
        {
            get => running;
            set
            {
                if (value == running) return;
                running = value;
                OnPropertyChanged();
            }
        }

        private string consoleText;
        public string ConsoleText
        {
            get => consoleText;
            set
            {
                if (value == consoleText) return;
                consoleText = value;
                OnPropertyChanged();
            }
        }
    }
}
