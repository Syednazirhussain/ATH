using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATH.ViewModel
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}