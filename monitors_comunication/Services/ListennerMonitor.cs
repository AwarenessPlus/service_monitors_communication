using Awareness_Desktop.BackEnd.Conexion_Monitores;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

                while (true)
                {
                    Mat imageAux = new Mat();
                    for (int i = 0; i < 8; i++) {
                        capture.Read(imageAux);
                        if (imageAux.IsEmpty)
                        {
                            break;
                        }
                    }
                    
                    
                    string[] colors = { "Green", "Yellow", "Red", "Yellow", "White", "Red", "Yellow" };
                    CoordinatesFinder coordinates = new(colors);
                    ImageSeparator imageSeparator = new(coordinates.FindxCoordinates(imageAux), coordinates.FindyCoordinates(imageAux));
                    imageSeparator.SplitImage(imageAux.ToImage<Bgr, Byte>());
                    Image<Bgr, byte>[] arrayOfImages = imageSeparator.GetImagesCreated().ToArray();
                    CvInvoke.Imwrite(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola1.png", arrayOfImages[6]);
                    CvInvoke.Imwrite(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola2.png", arrayOfImages[10]);
                    CvInvoke.Imwrite(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola3.png", arrayOfImages[24]);
                    arrayOfImages = null;
                    coordinates = null;
                    imageAux = null;
                    imageSeparator = null;
                    imageAux = null;
                    GC.Collect();
                    
                }
            }
        }

    }
}
