using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Ninject;

using Week27_DependencyInjection.Interfaces;
using Week27_DependencyInjection.Services.MessagingServices;
using Week27_DependencyInjection.Services.PaymentProcessors;

namespace Week27_DependencyInjection.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IMessageService>().To<AcmeMessagingServiceAdaptor>();
            _kernel.Bind<IPaymentProcessor>().To<AcmePaymentProcessorAdaptor>();
        }



    }
}