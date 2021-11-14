using Awareness_Desktop.BackEnd.Conexion_Monitores;
using Emgu.CV;
using Emgu.CV.Structure;
using System;

namespace monitors_comunication.Services
{
    public class ListennerMonitor
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory + @"/video/videoPrueba.mp4";
        public string Path { get => path; set => path = value; }
       
        public void ListenerMonitores()
        {
            VideoCapture capture = new VideoCapture(path);
            if (!capture.IsOpened)
            {
                throw new Exception("Capture intialization failed");
            }
            else
            {
                Mat imageAux = new Mat();
                capture.Read(imageAux);
                
                string[] colors = { "Green", "Yellow", "Red", "Yellow", "White", "Red", "Yellow" };
                CoordinatesFinder coordinates = new(colors);
                ImageSeparator imageSeparator = new(coordinates.FindxCoordinates(imageAux), coordinates.FindyCoordinates(imageAux));
                do
                {
                    capture.Read(imageAux);                                 
                    Image<Bgr, byte>[] arrayOfImages = imageSeparator.SplitImage(imageAux.ToImage<Bgr, Byte>()).ToArray();
                    if (arrayOfImages.Length > 0)
                    {
                        CvInvoke.Imwrite(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola1.png", arrayOfImages[6]);
                        CvInvoke.Imwrite(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola2.png", arrayOfImages[10]);
                        CvInvoke.Imwrite(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola3.png", arrayOfImages[24]);
                    }
                    arrayOfImages = null;
                   
                    imageAux = null;
                    GC.Collect();
                    imageAux = new Mat();
                    capture.Read(imageAux);                    

                } while (!imageAux.IsEmpty);

                coordinates = null;
                imageSeparator = null;
            }
        }

    }
}
