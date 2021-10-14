using System;
using Clima.UI.Interface.Themes;

namespace Clima.UI.WPF.ThemeDark
{
    public class DarkTheme:Theme
    {
        public override Uri GetResourceUri()
        {
            string uri = "Clima.UI.WPF.ThemeDark";

            return new Uri("/" + uri + ";component/DarkTheme.xaml", UriKind.Relative);
        }
    }
}