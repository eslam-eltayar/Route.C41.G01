using System;
using Route.C41.G01.PL.ViewModels;
namespace Route.C41.G01.PL.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
