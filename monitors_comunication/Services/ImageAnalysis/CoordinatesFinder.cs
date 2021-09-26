using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;

namespace Awareness_Desktop.BackEnd.Conexion_Monitores
{
    class CoordinatesFinder
    {
        private List<int> _yCoordinates;
        private List<int> _xCoordinates;
        private readonly string[] colors;

        public CoordinatesFinder(string[] colors)
        {
            this.colors = colors;
        }

        public List<int> GetyCoordinates()
        {
            return this._yCoordinates;
        }

        public List<int> GetxCoordinates()
        {
            return this._xCoordinates;
        }

        public void SetxCoordinates(List<int> xCoordinates)
        {
            this._xCoordinates = xCoordinates;
        }

        public List<int> FindxCoordinates(Mat imageO)
        {
            this._xCoordinates = new List<int>();
            _xCoordinates.Add((int)(imageO.Cols * 0.167));
            _xCoordinates.Add((int)(imageO.Cols * 0.673));
            _xCoordinates.Add((int)(imageO.Cols * 0.84));
            _xCoordinates.Add((int)(imageO.Cols * 0.99));

            return _xCoordinates;
        }
        public List<int> FindyCoordinates(Mat image)
        {
            Image<Bgr, byte> imageToSplit = image.ToImage<Bgr, Byte>();
            int[] minimums = new int[colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                minimums[i] = 1000000;
            }
            for (int j = 0; j < 12; j++)
            {
                for (int i = (int) (image.Rows * 0.046) ; i < image.Rows*0.92; i++)
                {
                    if (!WhatColorIs("Black", imageToSplit.Data[i, j, 0], imageToSplit.Data[i, j, 1], imageToSplit.Data[i, j, 2]))
                    {
                        for (int z = 0; z < minimums.Length; z++)
                        {
                            if (z == 0 &&
                            WhatColorIs(colors[z], imageToSplit.Data[i, j, 0],
                            imageToSplit.Data[i, j, 1], imageToSplit.Data[i, j, 2]) &&
                            i < minimums[z])
                            {
                                minimums[z] = i;
                            }
                            if (z > 0 &&
                            WhatColorIs(colors[z], imageToSplit.Data[i, j, 0],
                            imageToSplit.Data[i, j, 1], imageToSplit.Data[i, j, 2]) &&
                            i < minimums[z] && i > minimums[z - 1])
                            {
                                minimums[z] = i;
                            }
                        }
                    }
                }
            }
            this._yCoordinates = new List<int>();
            _yCoordinates.AddRange(minimums);
            return _yCoordinates;
        }
        public static bool WhatColorIs(String color, int blue, int green, int red)
        {
            if (blue < 20 && green < 20 && red < 20 && color.Equals("Black"))
                return true;
            else if (blue < 80 && green > 210 && red < 80 && color.Equals("Green"))
                return true;
            else if (blue < 65 && green > 215 && red > 215 && color.Equals("Yellow"))
                return true;
            else if (blue < 85 && blue > 60 &&
                 green < 85 && green > 60 && red > 210 && color.Equals("Red"))
                return true;
            else if (blue > 210 && green > 210 && red > 210 && color.Equals("White"))
                return true;
            else
                return false;
        }
    }
}
