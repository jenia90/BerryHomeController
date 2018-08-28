using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BerryHomeController.Common.Models;

namespace BerryHomeController.Common.Services
{
    /// <summary>
    /// Creates a new StateApiService.
    /// <remarks>Since the StateController only has two methods - Get and Set - the only operations that are supported are GetByIdAsync() and PostAsync().</remarks>
    /// </summary>
    internal class BerryApiStateService : BerryApiService<State>
    {
        public BerryApiStateService() : base("State/")
        {
        }
    }
}
