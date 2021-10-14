using System.Collections.Generic;

namespace Clima.UI.Interface.Themes
{
    public interface IThemeService
    {
        IEnumerable<Theme> InstalledThemes { get; }
        void LoadTheme(Theme theme);
        Theme CurrentTheme { get; }
    }
}