using Awareness_Desktop.BackEnd.Conexion_Monitores;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using monitors_comunication.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace monitors_comunication.Services
{
    public class MonitorVitalsSignsConnectionByVideo<T> : IMonitorConnection<T>
    {
        static List<Thread> threads;
        private static MonitorVitalsSignsConnectionByVideo<T> _instance;

        public static IMonitorConnection<T> GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MonitorVitalsSignsConnectionByVideo<T>();
                threads = new();

            }
            return _instance;
        }


        public Boolean ConnectMonitor()
        {

            if (threads.Count > 0)
            {
                return false;
            }
            else
            {
                ListennerMonitor listenner = new();
                Thread hilo = new(new ThreadStart(listenner.ListenerMonitores));
                hilo.Start();
               
                threads.Add(hilo);
                return true;
            }

           
        }

        public bool DisconnectMonitor()
        {

            if (threads.Count > 0)
            {
                Thread thread = threads.First<Thread>();
                thread.Interrupt();
                threads.Clear();
                GC.Collect();
                return true;
            }
            return false;
        }

        public async Task<FileStream> GetDataMonitor(String vitalSign)
        {

            if (Configuration.Configuration.CARDIAC_FRECUENCY.Equals(vitalSign))
            {
                 FileStream image= await Task.Run(() => System.IO.File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola1.png"));
                return image;
            }
            else if (Configuration.Configuration.SATURATION.Equals(vitalSign))
            {
                FileStream image = await Task.Run(() => System.IO.File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola2.png"));
                return image;
            }
            else if (Configuration.Configuration.NON_INVASIVE_BLOOD_PRESURE.Equals(vitalSign))
            {
                FileStream image = await Task.Run(() => System.IO.File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola3.png"));
                return image;
            }
            else
            {
                return null;
            }
        }
    }
}
