using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IServices
{
    public interface INavigationService
    {
        void NavigateTo(Type pageType);
        void NavigateTo(Type pageType, object data);
    }

}
