using BerryHomeController.Common.ViewModels;
using Xamarin.Forms;

namespace BerryHomeController.Common.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //((MainPageViewModel) BindingContext).Navigation = Navigation;
        }
    }
}
