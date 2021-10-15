using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.Context;
using Clima.UI.Interface;

namespace Clima.Bootstrapper.MVVM
{
    public class ViewActivator : DefaultComponentActivator
    {
        public ViewActivator(
            ComponentModel model,
            IKernelInternal kernel,
            ComponentInstanceDelegate onCreation,
            ComponentInstanceDelegate onDestruction)
            : base(model, kernel, onCreation, onDestruction)
        {

        }

        protected override object CreateInstance(CreationContext context, ConstructorCandidate constructor, object[] arguments)
        {
            var component = base.CreateInstance(context, constructor, arguments);
            var assignator = Kernel.Resolve<IViewModelAssignation>();
            assignator.AssignViewModel(component, arguments);
            
            return component;
        }
    }
}