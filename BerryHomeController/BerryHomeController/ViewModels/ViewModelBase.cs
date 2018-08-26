using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BerryHomeController.Common.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected async Task NavigateTo(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(page));
        }

        protected async Task NavigateBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
