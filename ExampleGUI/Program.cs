using System;
using log4net;
using SimpleInjector;

namespace ExampleGUI
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            var container = Bootstrap(isInDesignMode: false);

            RunApplication(container);
        }

        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
                var mainWindow = container.GetInstance<MainWindow>();

                app.Run(mainWindow);
            }
            catch (Exception ex)
            {
                container.GetInstance<ILog>().Error(ex);
                throw;
            }
        }

        public static Container Bootstrap(bool isInDesignMode)
        {
            var container = new Container();

            if (isInDesignMode)
            {
                //Design-time implementations
            }
            else
            {
                //Runtime implementations
            }

            container.Register<MainWindow>();
            container.Register<MainWindowViewModel>();
            container.RegisterConditional(typeof(ILog),
                c => typeof(Log4NetAdapter<>).MakeGenericType(c.Consumer.ImplementationType),
                Lifestyle.Singleton,
                c => true);

            container.Verify();

            return container;
        }
    }
}
