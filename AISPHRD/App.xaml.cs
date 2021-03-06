﻿using AISPHRD.Data;
using AISPHRD.Repositories;
using AISPHRD.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AISPHRD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConscriptRepository, ConscriptRepository>();
            services.AddTransient<IWorkerRepository, WorkerRepository>();
            services.AddTransient<IMilitaryIDRepository, MilitaryIDRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite("Data Source=database.db"));

            services.AddSingleton<LoginWindow>();
            services.AddScoped<TabsWindow>();
        }

        public void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetService<LoginWindow>();
            mainWindow.Show();
        }
    }
}
