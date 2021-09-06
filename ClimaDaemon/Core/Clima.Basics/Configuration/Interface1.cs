using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clima.Basics.Configuration
{
    public interface IItemSave
    {
        event EventHandler Save;
    }
}
