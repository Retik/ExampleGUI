using SimpleInjector;

namespace ExampleGUI
{
    public class ViewModelLocator
    {
        private Container container;

        public ViewModelLocator()
        {
            this.container = Program.Bootstrap(isInDesignMode: true);
        }

        public MainWindowViewModel MainWindowViewModel
        {
            get { return this.container.GetInstance<MainWindowViewModel>(); }
        }
    }
}
