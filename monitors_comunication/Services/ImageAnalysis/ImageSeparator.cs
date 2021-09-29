using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Awareness_Desktop.BackEnd.Conexion_Monitores
{
    /* ImageSeparator: 
     * This class allows you create new images from one image having the coordinates where to cut */
    class ImageSeparator
    {
        private readonly List<Image<Bgr, byte>> _imagesCreated;
        private readonly List<int> _xCoordinates;
        readonly List<int> _yCoordinates;


        public ImageSeparator()
        {
            
        }
        public ImageSeparator(List<int> xCoordinates, List<int> yCoordinates)
        {
            _imagesCreated = new();
            this._xCoordinates = xCoordinates;
            this._yCoordinates = yCoordinates;
        }
        public List<Image<Bgr, byte>> GetImagesCreated()
        {
            return this._imagesCreated;
        }
        public bool SplitImage(Image<Bgr, byte> imageToSplit)
        {
            int[] xSizeImages = CalculateSize(_xCoordinates);
            int[] ySizeImages = CalculateSize(_yCoordinates);
            Image<Bgr, byte>[] auxiliarArrayOfImages;
            HashSet<int> yCoordinatesToPudateStack = new();

            if ((_xCoordinates.Count < 1 || _xCoordinates.Count > imageToSplit.Cols) &&
                   (_yCoordinates.Count < 1 || _yCoordinates.Count > imageToSplit.Rows))
                return false;
            foreach (int yCounter in ySizeImages)
            {
                foreach (int xCounter in xSizeImages)
                {
                    Image<Bgr, Byte> image = new(xCounter, yCounter);
                    _imagesCreated.Add(image);
                }
            }
            auxiliarArrayOfImages = this._imagesCreated.ToArray();
            int[] xCount = new int[auxiliarArrayOfImages.Length];
            int[] yCount = new int[auxiliarArrayOfImages.Length];
            for (int yCounter = 0; yCounter < _yCoordinates.ToArray()[^1]; yCounter++)
            {
                for (int xCounter = 0; xCounter < _xCoordinates.ToArray()[^1]; xCounter++)
                {
                    int index = ObteinIndexImageFromCoordinates(xCounter, yCounter);
                    auxiliarArrayOfImages[index].Data[yCount[index], xCount[index], 0] = imageToSplit.Data[yCounter, xCounter, 0];
                    auxiliarArrayOfImages[index].Data[yCount[index], xCount[index], 1] = imageToSplit.Data[yCounter, xCounter, 1];
                    auxiliarArrayOfImages[index].Data[yCount[index], xCount[index], 2] = imageToSplit.Data[yCounter, xCounter, 2];
                    xCount[index]++;
                    yCoordinatesToPudateStack.Add(index);
                }
                Stack<int> pila = new();
                int[] array = new int[yCoordinatesToPudateStack.Count];
                yCoordinatesToPudateStack.CopyTo(array);
                foreach (int y in array)
                {
                    pila.Push(y);
                }
                while (pila.Count != 0)
                {
                    int index = pila.Pop();
                    yCount[index]++;
                    xCount[index] = 0;
                }
            }
            return false;
        }
        public static int[] CalculateSize(List<int> coordinates)
        {
            int[] arrayCopyFromCoordinates = coordinates.ToArray();
            int previousValue = 0;
            for (int i = 0; i < coordinates.Count; i++)
            {
                int actualValue = arrayCopyFromCoordinates[i];
                arrayCopyFromCoordinates[i] -= previousValue;
                previousValue = actualValue;
            }
            return arrayCopyFromCoordinates;
        }
        public int ObteinIndexImageFromCoordinates(int xCoordinate, int yCoordinate)
        {
            int index = -1;
            int[] CopyFromxCoordinates = _xCoordinates.ToArray();
            int[] CopyFromyCoordinates = _yCoordinates.ToArray();
            for (int x = 0; x < CopyFromxCoordinates.Length; x++)
            {
                for (int y = 0; y < CopyFromyCoordinates.Length; y++)
                {
                    if (xCoordinate < CopyFromxCoordinates[x] &&
                         yCoordinate < CopyFromyCoordinates[y])
                    {
                        if ((x == 0 && y == 0) ||
                            (x > 0 && xCoordinate >= CopyFromxCoordinates[x - 1] && y == 0) ||
                            (y > 0 && yCoordinate >= CopyFromyCoordinates[y - 1] && x == 0) ||
                            (x > 0 && y > 0 && xCoordinate >= CopyFromxCoordinates[x - 1]
                              && yCoordinate >= CopyFromyCoordinates[y - 1])
                           )
                        {
                            return (y * _xCoordinates.Count) + x;
                        }
                    }
                }
            }
            return index;
        }
    }
}

