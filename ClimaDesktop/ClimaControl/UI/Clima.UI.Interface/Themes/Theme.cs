using System;

namespace Clima.UI.Interface.Themes
{
    public abstract class Theme
    {
        public Theme()
        {
        }
        public abstract Uri GetResourceUri();
    }
}