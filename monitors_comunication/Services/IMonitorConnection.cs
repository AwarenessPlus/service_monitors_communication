using Emgu.CV;
using Emgu.CV.Structure;
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

        public Dictionary<String,T> GetDataMonitor();

        public int DisconnectMonitor();
    }
}
