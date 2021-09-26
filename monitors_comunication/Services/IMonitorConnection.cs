using monitors_comunication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace monitors_comunication.Services
{
    interface IMonitorConnection<T>
    {
        public int ConnectMonitor();

        public IMonitor<T> GetDataMonitor();

        public int DisconnectMonitor();
    }
}
