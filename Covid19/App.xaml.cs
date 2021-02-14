using Covid19.Services;
using System.Linq;
using System.Windows;

namespace Covid19
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //var data_service = new DataService();
            //var countries = data_service.GetData().ToArray();
        }

    }
}
