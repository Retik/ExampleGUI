using System;
using log4net;

namespace ExampleGUI
{
    public interface IConsoleLog
    {
        void WriteLine(string message);
    }

    public class ConsoleLog : IConsoleLog
    {
        private readonly MainWindowViewModel viewModel;
        private readonly ILog log;

        public ConsoleLog(MainWindowViewModel viewModel, ILog log)
        {
            this.log = log;
            this.viewModel = viewModel;
        }

        public void WriteLine(string message)
        {
            log.Info(message);
            viewModel.ConsoleText += message + Environment.NewLine;
        }
    }
}
