using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BerryHomeController.Common.Models;
using BerryHomeController.Common.Services;
using Unity.Attributes;
using Xamarin.Forms;

namespace BerryHomeController.Common.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {

        }

        [Unity.Attributes.Dependency]
        protected IBerryApiService<Models.Device> DeviceService { get; set; }

        [Unity.Attributes.Dependency]
        protected IBerryApiService<Job> JobService { get; set; }

        [Unity.Attributes.Dependency]
        protected IBerryApiService<State> StateService { get; set; }

        /// <summary>
        /// Pushes a page to the Navigation stack.
        /// </summary>
        /// <param name="page">Page object to be pushed.</param>
        protected async Task NavigateTo(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(page));
        }

        /// <summary>
        /// Pops a page from the Navigation stack.
        /// </summary>
        protected async Task NavigateBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
