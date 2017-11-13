using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.Core.Module
{
    public interface IModuleFinder
    {
        IEnumerable<IModule> Find();
    }
}
