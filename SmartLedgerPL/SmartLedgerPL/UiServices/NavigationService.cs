using Microsoft.UI.Xaml.Controls;
using SmartLedger.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedgerPL.UiServices
{
    public class NavigationService : INavigationService
    {
        private Frame _frame;

        public void Initialize(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo(Type pageType)
        {
            _frame?.Navigate(pageType);
        }

        public void NavigateTo(Type pageType, object data)
        {
            _frame?.Navigate(pageType, data);
        }
    }

}
