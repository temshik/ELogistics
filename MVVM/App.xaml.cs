using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVVM.ErrorHandler;
using MVVM.Readers;
using MVVM.Services;
using System.Windows;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("config.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            // The default directory for the initial download of files.
            string directory = configuration.GetSection("Directory").Value;

            services.AddSingleton<IErrorHandler>(provider => new DefaultErrorHandler());
            services.AddTransient<IFileReader>(provider => new JsonFileReader(new DefaultErrorHandler(), directory));
            services.AddTransient<IDialogService>(provider => new DefaultDialogService());
            services.AddTransient<IFileService>(provider => new JsonFileService(new JsonFileReader(new DefaultErrorHandler(), directory)));
            services.AddSingleton<MainWindow>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
