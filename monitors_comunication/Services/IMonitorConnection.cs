using Emgu.CV;
using Emgu.CV.Structure;
using monitors_comunication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace monitors_comunication.Services
{
    public interface IMonitorConnection<T>
    {
        private static IMonitorConnection<T> _instance;
        public static IMonitorConnection<T> GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MonitorVitalsSignsConnectionByVideo<T>();

            }
            return _instance;
        }
        public bool ConnectMonitor();

        public bool DisconnectMonitor();

        Task<FileStream> GetDataMonitor(String vitalSign);
    }
}
