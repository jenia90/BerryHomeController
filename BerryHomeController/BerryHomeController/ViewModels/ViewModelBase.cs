using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BerryHomeController.Common.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {

        }

        public INavigation Navigation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected async Task NavigateTo(Page page)
        {
            await Navigation.PushAsync(new NavigationPage(page));
        }

        protected async Task NavigateBack()
        {
            await Navigation.PopAsync();
        }
    }
}
