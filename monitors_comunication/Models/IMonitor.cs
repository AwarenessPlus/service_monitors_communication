using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace monitors_comunication.Models
{
    public interface IMonitor<T>
    {
        public Dictionary<string,T> ObtainDataMonitor();
        public int SaveDataMonitor(Dictionary<String,T> dataMonitor);
    }
}
