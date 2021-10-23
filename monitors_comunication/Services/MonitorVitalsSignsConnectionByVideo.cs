using Awareness_Desktop.BackEnd.Conexion_Monitores;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using monitors_comunication.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;


namespace monitors_comunication.Services
{
    public class MonitorVitalsSignsConnectionByVideo
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory + @"/video/videoPrueba.mp4";

        public string Path { get => path; set => path = value; }


        public int DisconnectMonitor()
        {
            throw new NotImplementedException();
        }

        public void GetDataMonitor()
        {
            Mat imageAux = CvInvoke.Imread(AppDomain.CurrentDomain.BaseDirectory + "\\video\\holaOriginal.png", ImreadModes.Color);
            string[] colors = { "Green", "Yellow", "Red", "Yellow", "White", "Red", "Yellow" };
            CoordinatesFinder coordinates = new(colors);
            ImageSeparator imageSeparator = new(coordinates.FindxCoordinates(imageAux), coordinates.FindyCoordinates(imageAux));
            imageSeparator.SplitImage(imageAux.ToImage<Bgr, Byte>());
            Image<Bgr, byte>[] arrayOfImages = imageSeparator.GetImagesCreated().ToArray();
            Console.WriteLine("imagen añadida");
            CvInvoke.Imwrite(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola1.png", arrayOfImages[6]);
            CvInvoke.Imwrite(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola2.png", arrayOfImages[10]);
            CvInvoke.Imwrite(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola3.png", arrayOfImages[24]);
            arrayOfImages = null;
            coordinates = null;
            imageAux = null;
            imageSeparator = null;
            GC.Collect();

 

        }

    }
}
