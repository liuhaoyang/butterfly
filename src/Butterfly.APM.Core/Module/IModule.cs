using System;
using System.Collections.Generic;
using System.Text;
using AspectCore.Injector;

namespace Butterfly.APM.Core.Module
{
    public interface IModule
    {
        string Name { get; }

        void Configure(IServiceContainer services);

        void Initialize(IServiceResolver resolver);
    }
}