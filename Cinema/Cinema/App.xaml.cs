using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cinema
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "Shapes_Experimantal" }); //what does Device.SetFlags do? 
            MainPage = new NavigationPage (new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
