using Castle.MicroKernel.Facilities;
using Clima.UI.Interface.Views;

namespace Clima.Bootstrapper.MVVM
{
    public class ViewActivatorFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.ComponentModelCreated += Kernel_ComponentModelCreated;
        }

        void Kernel_ComponentModelCreated(Castle.Core.ComponentModel model)
        {
            if (typeof(IView).IsAssignableFrom(model.Implementation))
            {
                if (model.CustomComponentActivator == null)
                    model.CustomComponentActivator = typeof(ViewActivator);
            }
        }
    }
}