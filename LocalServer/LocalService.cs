using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServer
{
    class LocalService : ILocalService
    {
        public void ConnectLocal()
        {
            Console.WriteLine("Client i local server su povezani.");
        }
    }
}
