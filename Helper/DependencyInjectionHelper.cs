using System;
using Microsoft.Extensions.DependencyInjection;

namespace APITemplate.Helper
{
    public class DependencyInjectionHelper{
        private readonly IServiceProvider _serviceProvider;
        public DependencyInjectionHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public T CreateInstance<T>() {
            T instance = (T)ActivatorUtilities.CreateInstance(_serviceProvider,typeof(T));
            return instance;
        }
    }
}