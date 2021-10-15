using System;
using Clima.UI.Interface.Themes;

namespace Clima.UI.WPF.Views.Themes
{
    public class GenericTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            string uri;
            uri = "Clima.UI.WPF.Views";

            return new Uri("/" + uri + ";component/Themes/generic.xaml", UriKind.Relative);
        }
    }
}