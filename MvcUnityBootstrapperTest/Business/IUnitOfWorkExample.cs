using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcUnityBootstrapperTest.Business
{
    public interface IUnitOfWorkExample : IDisposable
    {
        string HelloFromUnitOfWorkExample();
    }
}
