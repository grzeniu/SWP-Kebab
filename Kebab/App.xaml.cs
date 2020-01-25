using System.Windows;
using Kebab.Database;

namespace Kebab
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DataFactory.CreateSampleOrders();
            DataFactory.CreateMetadata();
        }
    }
}
