using Awareness_Desktop.BackEnd.Conexion_Monitores;
using Emgu.CV;
using Emgu.CV.Structure;
using monitors_comunication.Models;
using System;
using System.Collections.Generic;

namespace monitors_comunication.Services
{
    public class MonitorVitalsSignsConnectionByVideo<T> : IMonitorConnection<T>
    {
        private string path = "C:\\Users\\aulasingenieria\\Documents\\Awareness+\\web services\\monitors_comunication\\service_monitors_comunication\\monitors_comunication\\bin\\Debug\\net5.0\\video\\videoPrueba.mp4";
        private VideoCapture capture;

        public string Path { get => path; set => path = value; }
        public VideoCapture Capture { get => capture; set => capture = value; }

        public int ConnectMonitor()
        {
            capture = new VideoCapture(path);
            if (!capture.IsOpened)
            {
                throw new Exception("Capture intialization failed");
            }
            return 1;
        }

        public int DisconnectMonitor()
        {
            throw new NotImplementedException();
        }

        public IMonitor<T> GetDataMonitor()
        {
            Mat imageAux = new();
            capture.Read(imageAux);
            if (imageAux.IsEmpty)
            {
                return null;
            }
            CvInvoke.Imwrite("C:\\Users\\aulasingenieria\\Documents\\Awareness+\\web services\\monitors_comunication\\service_monitors_comunication\\monitors_comunication\\bin\\Debug\\net5.0\\video\\hola.png", imageAux);
            string[] colors = { "Green", "Yellow", "Red", "Yellow", "White", "Red", "Yellow" };
            CoordinatesFinder coordinates = new(colors);
            ImageSeparator imageSeparator = new(coordinates.FindxCoordinates(imageAux), coordinates.FindyCoordinates(imageAux));
            imageSeparator.SplitImage(imageAux.ToImage<Bgr, Byte>());
            Image<Bgr, byte>[] arrayOfImages = imageSeparator.GetImagesCreated().ToArray();
            Dictionary<String, Image<Bgr, byte>> vitalSignImages = new();
            vitalSignImages.Add(Configuration.Configuration.CARDIAC_FRECUENCY, arrayOfImages[6]);
            vitalSignImages.Add(Configuration.Configuration.SATURATION, arrayOfImages[10]);
            vitalSignImages.Add(Configuration.Configuration.NON_INVASIVE_BLOOD_PRESURE, arrayOfImages[24]);
            VitalSigns<Image<Bgr, byte>> vitalSigns = new();
            vitalSigns.SaveDataMonitor(vitalSignImages);
            Console.WriteLine("imagen añadida");
            CvInvoke.Imwrite("C:\\Users\\aulasingenieria\\Documents\\Awareness+\\web services\\monitors_comunication\\service_monitors_comunication\\monitors_comunication\\bin\\Debug\\net5.0\\video\\hola.png", arrayOfImages[6]);
            return (IMonitor<T>)vitalSignImages;
        }
    }
}
