﻿namespace Clima.Basics.Services
{
    public interface IServiceProvider
    {
        T Resolve<T>();

        void Register<TService, TImpl>(string name = "")
            where TService : class
            where TImpl : TService;

        void RegisterWithoutInterface<TImpl>(string name = "")
            where TImpl : class;

        void InitializeService(IServiceInitializer initializer);
    }
}