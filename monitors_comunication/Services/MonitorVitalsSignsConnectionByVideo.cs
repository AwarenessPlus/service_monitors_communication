using Awareness_Desktop.BackEnd.Conexion_Monitores;
using Emgu.CV;
using Emgu.CV.Structure;
using monitors_comunication.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace monitors_comunication.Services
{
    public class MonitorVitalsSignsConnectionByVideo
    {
        private string path = "C:\\Users\\jmarinflorez\\Documents\\Uni\\service_communication_monitors\\service_monitors_communication\\monitors_comunication\\bin\\Debug\\net5.0\\video\\videoPrueba.mp4";
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

        public VitalSigns<Bitmap> GetDataMonitor()
        {
            Mat imageAux = new();
            capture.Read(imageAux);
            if (imageAux.IsEmpty)
            {
                return null;
            }
            CvInvoke.Imwrite("C:\\Users\\jmarinflorez\\Documents\\Uni\\service_communication_monitors\\service_monitors_communication\\monitors_comunication\\bin\\Debug\\net5.0\\video\\original.png", imageAux);
            string[] colors = { "Green", "Yellow", "Red", "Yellow", "White", "Red", "Yellow" };
            CoordinatesFinder coordinates = new(colors);
            ImageSeparator imageSeparator = new(coordinates.FindxCoordinates(imageAux), coordinates.FindyCoordinates(imageAux));
            imageSeparator.SplitImage(imageAux.ToImage<Bgr, Byte>());
            Image<Bgr, byte>[] arrayOfImages = imageSeparator.GetImagesCreated().ToArray();
            Dictionary<String, Bitmap> vitalSignImages = new();
            vitalSignImages.Add(Configuration.Configuration.CARDIAC_FRECUENCY,  arrayOfImages[6].Mat.ToBitmap());
            vitalSignImages.Add(Configuration.Configuration.SATURATION, arrayOfImages[10].Mat.ToBitmap());
            vitalSignImages.Add(Configuration.Configuration.NON_INVASIVE_BLOOD_PRESURE, arrayOfImages[24].Mat.ToBitmap());
            VitalSigns<Bitmap> vitalSigns = new();
            vitalSigns.SaveDataMonitor(vitalSignImages);
            Console.WriteLine("imagen añadida");
            CvInvoke.Imwrite("C:\\Users\\jmarinflorez\\Documents\\Uni\\service_communication_monitors\\service_monitors_communication\\monitors_comunication\\bin\\Debug\\net5.0\\video\\hola1.png", arrayOfImages[6]);
            CvInvoke.Imwrite("C:\\Users\\jmarinflorez\\Documents\\Uni\\service_communication_monitors\\service_monitors_communication\\monitors_comunication\\bin\\Debug\\net5.0\\video\\hola2.png", arrayOfImages[10]);
            CvInvoke.Imwrite("C:\\Users\\jmarinflorez\\Documents\\Uni\\service_communication_monitors\\service_monitors_communication\\monitors_comunication\\bin\\Debug\\net5.0\\video\\hola3.png", arrayOfImages[24]);
            return vitalSigns;
        }

    }
}
