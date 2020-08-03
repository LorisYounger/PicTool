using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;

namespace PicTool
{
    /// <summary>
    /// Canny算法相关C#代码来自 https://github.com/stalko/CannyImage/blob/master/ColorCannyImage/ColorCannyImage/Canny.cs
    /// 经过一些魔改
    /// The Canny edge detector is an edge detection operator that uses a multi-stage algorithm to detect a wide range of edges in images.
    /// It was developed by John F. Canny in 1986. Canny also produced a computational theory of edge detection explaining why the technique works.
    /// </summary>
    class Canny
    {
        public Bitmap Img;
        public int Width, Height;
        public int[,] ImgMatrix;
        //Gaussian Kernel Data
        int[,] GaussianMatrix;
        int MatrixSize = 5;
        int GaussWeight;
        float Sigma = 1;   // for N=2 Sigma =0.85  N=5 Sigma =1, N=9 Sigma = 2    2*Sigma = (int)N/2
        //Canny Edge Detection Parameters
        float MaxHysteresis, MinHysteresis;
        public int[,] FilteredImage;
        int[,] EdgeMatrix;

        /// <summary>
        /// 这个是需要的碎片
        /// </summary>
        public int[,] EdgePosition;
        public int[,] PositionVisited;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Canny(Bitmap Input)
        {
            // Gaussian and Canny Parameters
            MaxHysteresis = 20F;
            MinHysteresis = 10F;
            Img = Input;
            Width = Img.Width;
            Height = Img.Height;
            EdgePosition = new int[Width, Height];
            PositionVisited = new int[Width, Height];

            LoadImage();
            DetectEdges();
            return;
        }

        /// <summary>
        /// Default constructor with top and bottom threshold.
        /// </summary>
        public Canny(Bitmap Input, float ThresholdHigh, float ThresholdLow)
        {

            // Gaussian and Canny Parameters

            MaxHysteresis = ThresholdHigh;
            MinHysteresis = ThresholdLow;

            Img = Input;
            Width = Img.Width;
            Height = Img.Height;

            EdgePosition = new int[Width, Height];
            PositionVisited = new int[Width, Height];

            LoadImage();
            DetectEdges();
            return;
        }

        /// <summary>
        /// Constructor with input image, top and bottom threshold, Gaussian sigma, Gaussian mask size.
        /// </summary>
        public Canny(Bitmap Input, float ThresholdHigh, float ThresholdLow, float SigmaforGaussianMatrix, int GaussianMaskSize)
        {

            // Gaussian and Canny Parameters

            MaxHysteresis = ThresholdHigh;
            MinHysteresis = ThresholdLow;
            MatrixSize = GaussianMaskSize;
            Sigma = SigmaforGaussianMatrix;
            Img = Input;
            Width = Img.Width;
            Height = Img.Height;

            EdgePosition = new int[Width, Height];
            PositionVisited = new int[Width, Height];

            LoadImage();
            DetectEdges();
            return;
        }

        /// <summary>
        /// Creates image to display.
        /// </summary>
        public Bitmap Display()
        {
            int i, j;
            Bitmap image = new Bitmap(Img.Width, Img.Height);
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0, Img.Width, Img.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        // write the logic implementation here
                        imagePointer1[0] = (byte)ImgMatrix[j, i];
                        imagePointer1[1] = (byte)ImgMatrix[j, i];
                        imagePointer1[2] = (byte)ImgMatrix[j, i];
                        imagePointer1[3] = (byte)255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j

                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//end for i
            }//end unsafe
            image.UnlockBits(bitmapData1);
            return image;// col;
        }      // Display Grey DispImg

        /// <summary>
        /// Creates image from grey image with float dimensions.
        /// </summary>
        public Bitmap DispImg(float[,] GreyImage)
        {
            int i, j;
            int W, H;
            W = GreyImage.GetLength(0);
            H = GreyImage.GetLength(1);
            Bitmap image = new Bitmap(W, H);
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0, W, H),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {

                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        // write the logic implementation here
                        imagePointer1[0] = (byte)((int)(GreyImage[j, i]));
                        imagePointer1[1] = (byte)((int)(GreyImage[j, i]));
                        imagePointer1[2] = (byte)((int)(GreyImage[j, i]));
                        imagePointer1[3] = (byte)255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }   //end for j
                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//End for i
            }//end unsafe
            image.UnlockBits(bitmapData1);
            return image;// col;
        }      // Display Grey Imag

        /// <summary>
        /// Creates image from grey image with integer dimensions.
        /// </summary>
        public Bitmap DispImg()
        {
            int[,] GreyImage = EdgePosition;
            int i, j;
            int W, H;
            W = GreyImage.GetLength(0);
            H = GreyImage.GetLength(1);
            Bitmap image = new Bitmap(W, H);
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0, W, H),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {

                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        // write the logic implementation here
                        imagePointer1[0] = (byte)GreyImage[j, i];
                        imagePointer1[1] = (byte)GreyImage[j, i];
                        imagePointer1[2] = (byte)GreyImage[j, i];
                        imagePointer1[3] = (byte)255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }   //end for j
                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//End for i
            }//end unsafe
            image.UnlockBits(bitmapData1);
            return image;// col;
        }      // Display Grey DispImg
        public Bitmap[] MakeAGAN()
        {
            List<Bitmap> bitmaps = new List<Bitmap>();
            Random rnd = new Random();
            int subtotal = 0; int maxW = Width / 128; int maxH = Height / 128;

            for (int sw = 0; sw + 1 < maxW; sw++)
            {
                for (int sh = 0; sh + 1 < maxH; sh++)
                {
                    for (int w = sw * 128; w < (sw + 2) * 128; w++)
                    {
                        for (int h = sh * 128; h < (sh + 2) * 128; h++)
                        {
                            subtotal += EdgePosition[w, h];
                        }
                    }
                    if (subtotal > rnd.Next(8388608))
                    {//这个区块合格了,保存起来
                     //创建作图区域
                        Bitmap bitmap = new Bitmap(256, 256);
                        Graphics graphic = Graphics.FromImage(bitmap);
                        //截取原图相应区域写入作图区
                        graphic.DrawImage(Img, 0, 0, new Rectangle(sw * 128, sh * 128, 256, 256), GraphicsUnit.Pixel);
                        bitmaps.Add(bitmap);
                        graphic.Dispose();
                    }
                    subtotal = 0;
                }
            }
            if (subtotal != 0)
                subtotal++;
            return bitmaps.ToArray();
        }
        /// <summary>
        /// Load image.
        /// </summary>
        private void LoadImage()
        {
            int i, j;
            ImgMatrix = new int[Img.Width, Img.Height];  //[Row,Column]
            Bitmap image = Img;
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        ImgMatrix[j, i] = (int)((imagePointer1[0] + imagePointer1[1] + imagePointer1[2]) / 3.0);
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }//end for i
            }//end unsafe
            image.UnlockBits(bitmapData1);
            return;
        }

        /// <summary>
        /// Generate Gaussian matrix.
        /// </summary>
        private void CreateGaussianMatrix(int N, float S, out int Weight)
        {

            float Sigma = S;
            float pi;
            pi = (float)Math.PI;
            int i, j;
            int SizeofKernel = N;

            float[,] Kernel = new float[N, N];
            GaussianMatrix = new int[N, N];
            float[,] OP = new float[N, N];
            float D1, D2;


            D1 = 1 / (2 * pi * Sigma * Sigma);
            D2 = 2 * Sigma * Sigma;

            float min = 1000;

            for (i = -SizeofKernel / 2; i <= SizeofKernel / 2; i++)
            {
                for (j = -SizeofKernel / 2; j <= SizeofKernel / 2; j++)
                {
                    Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] = ((1 / D1) * (float)Math.Exp(-(i * i + j * j) / D2));
                    if (Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] < min)
                        min = Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j];

                }
            }
            int mult = (int)(1 / min);
            int sum = 0;
            if ((min > 0) && (min < 1))
            {

                for (i = -SizeofKernel / 2; i <= SizeofKernel / 2; i++)
                {
                    for (j = -SizeofKernel / 2; j <= SizeofKernel / 2; j++)
                    {
                        Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] = (float)Math.Round(Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] * mult, 0);
                        GaussianMatrix[SizeofKernel / 2 + i, SizeofKernel / 2 + j] = (int)Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j];
                        sum = sum + GaussianMatrix[SizeofKernel / 2 + i, SizeofKernel / 2 + j];
                    }

                }

            }
            else
            {
                sum = 0;
                for (i = -SizeofKernel / 2; i <= SizeofKernel / 2; i++)
                {
                    for (j = -SizeofKernel / 2; j <= SizeofKernel / 2; j++)
                    {
                        Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] = (float)Math.Round(Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j], 0);
                        GaussianMatrix[SizeofKernel / 2 + i, SizeofKernel / 2 + j] = (int)Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j];
                        sum = sum + GaussianMatrix[SizeofKernel / 2 + i, SizeofKernel / 2 + j];
                    }

                }

            }
            //Normalizing matrix Weight
            Weight = sum;

            return;
        }

        /// <summary>
        /// Applays Gaussian filter.
        /// </summary>
        private int[,] Filter(int[,] Data)
        {
            CreateGaussianMatrix(MatrixSize, Sigma, out GaussWeight);

            int[,] Output = new int[Width, Height];
            int i, j, k, l;
            int Limit = MatrixSize / 2;

            float Sum = 0;


            Output = Data; // Removes Unwanted Data Omission due to matrix bias while convolution


            for (i = Limit; i <= ((Width - 1) - Limit); i++)
            {
                for (j = Limit; j <= ((Height - 1) - Limit); j++)
                {
                    Sum = 0;
                    for (k = -Limit; k <= Limit; k++)
                    {

                        for (l = -Limit; l <= Limit; l++)
                        {
                            Sum = Sum + ((float)Data[i + k, j + l] * GaussianMatrix[Limit + k, Limit + l]);

                        }
                    }
                    Output[i, j] = (int)(Math.Round(Sum / (float)GaussWeight));
                }

            }


            return Output;
        }

        /// <summary>
        /// Generate X & Y Gradients of image, implements differentiation using sobel Filter Mask.
        /// </summary>
        private float[,] FindGradients(int[,] Data, int[,] Filter)
        {
            int i, j, k, l, Fh, Fw;

            Fw = Filter.GetLength(0);
            Fh = Filter.GetLength(1);
            float sum = 0;
            float[,] Output = new float[Width, Height];

            for (i = Fw / 2; i <= (Width - Fw / 2) - 1; i++)
            {
                for (j = Fh / 2; j <= (Height - Fh / 2) - 1; j++)
                {
                    sum = 0;
                    for (k = -Fw / 2; k <= Fw / 2; k++)
                    {
                        for (l = -Fh / 2; l <= Fh / 2; l++)
                        {
                            sum = sum + Data[i + k, j + l] * Filter[Fw / 2 + k, Fh / 2 + l];


                        }
                    }
                    Output[i, j] = sum;

                }

            }
            return Output;

        }

        /// <summary>
        /// Detect Canny edges performing non maxima suppression.
        /// </summary>
        private void DetectEdges()
        {
            var GradientMat = new float[Width, Height];
            var NotMax = new float[Width, Height];
            var PostHysteresis = new int[Width, Height];

            var FilteredGradientX = new float[Width, Height];
            var FilteredGradientY = new float[Width, Height];

            //Gaussian Filter Input DispImg 

            FilteredImage = Filter(ImgMatrix);
            //Sobel Masks
            int[,] Dx = {{1,0,-1},
                         {1,0,-1},
                         {1,0,-1}};

            int[,] Dy = {{1,1,1},
                         {0,0,0},
                         {-1,-1,-1}};


            FilteredGradientX = FindGradients(FilteredImage, Dx);
            FilteredGradientY = FindGradients(FilteredImage, Dy);

            int i, j;

            //Compute the gradient magnitude based on derivatives in x and y:
            for (i = 0; i <= (Width - 1); i++)
            {
                for (j = 0; j <= (Height - 1); j++)
                {
                    GradientMat[i, j] = (float)Math.Sqrt((FilteredGradientX[i, j] * FilteredGradientX[i, j]) + (FilteredGradientY[i, j] * FilteredGradientY[i, j]));

                }

            }
            // Perform Non maximum suppression:
            // NotMax = GradientMat;

            for (i = 0; i <= (Width - 1); i++)
            {
                for (j = 0; j <= (Height - 1); j++)
                {
                    NotMax[i, j] = GradientMat[i, j];
                }
            }

            int Limit = MatrixSize / 2;
            int r, c;
            float Tangent;


            for (i = Limit; i <= (Width - Limit) - 1; i++)
            {
                for (j = Limit; j <= (Height - Limit) - 1; j++)
                {

                    if (FilteredGradientX[i, j] == 0)
                        Tangent = 90F;
                    else
                        Tangent = (float)(Math.Atan(FilteredGradientY[i, j] / FilteredGradientX[i, j]) * 180 / Math.PI); //rad to degree



                    //Horizontal Edge
                    if (((-22.5 < Tangent) && (Tangent <= 22.5)) || ((157.5 < Tangent) && (Tangent <= -157.5)))
                    {
                        if ((GradientMat[i, j] < GradientMat[i, j + 1]) || (GradientMat[i, j] < GradientMat[i, j - 1]))
                            NotMax[i, j] = 0;
                    }


                    //Vertical Edge
                    if (((-112.5 < Tangent) && (Tangent <= -67.5)) || ((67.5 < Tangent) && (Tangent <= 112.5)))
                    {
                        if ((GradientMat[i, j] < GradientMat[i + 1, j]) || (GradientMat[i, j] < GradientMat[i - 1, j]))
                            NotMax[i, j] = 0;
                    }

                    //+45 Degree Edge
                    if (((-67.5 < Tangent) && (Tangent <= -22.5)) || ((112.5 < Tangent) && (Tangent <= 157.5)))
                    {
                        if ((GradientMat[i, j] < GradientMat[i + 1, j - 1]) || (GradientMat[i, j] < GradientMat[i - 1, j + 1]))
                            NotMax[i, j] = 0;
                    }

                    //-45 Degree Edge
                    if (((-157.5 < Tangent) && (Tangent <= -112.5)) || ((67.5 < Tangent) && (Tangent <= 22.5)))
                    {
                        if ((GradientMat[i, j] < GradientMat[i + 1, j + 1]) || (GradientMat[i, j] < GradientMat[i - 1, j - 1]))
                            NotMax[i, j] = 0;
                    }

                }
            }


            //PostHysteresis = NotMax;
            for (r = Limit; r <= (Width - Limit) - 1; r++)
            {
                for (c = Limit; c <= (Height - Limit) - 1; c++)
                {

                    PostHysteresis[r, c] = (int)NotMax[r, c];
                }

            }

            //Find Max and Min in Post Hysterisis
            float min, max;
            min = 100;
            max = 0;
            for (r = Limit; r <= (Width - Limit) - 1; r++)
                for (c = Limit; c <= (Height - Limit) - 1; c++)
                {
                    if (PostHysteresis[r, c] > max)
                    {
                        max = PostHysteresis[r, c];
                    }

                    if ((PostHysteresis[r, c] < min) && (PostHysteresis[r, c] > 0))
                    {
                        min = PostHysteresis[r, c];
                    }
                }

            var HighGradients = new float[Width, Height];
            var LowGradients = new float[Width, Height]; ;
            EdgeMatrix = new int[Width, Height];

            for (r = Limit; r <= (Width - Limit) - 1; r++)
            {
                for (c = Limit; c <= (Height - Limit) - 1; c++)
                {
                    if (PostHysteresis[r, c] >= MaxHysteresis)
                    {

                        EdgeMatrix[r, c] = 1;
                        HighGradients[r, c] = 255;
                    }
                    if ((PostHysteresis[r, c] < MaxHysteresis) && (PostHysteresis[r, c] >= MinHysteresis))
                    {

                        EdgeMatrix[r, c] = 2;
                        LowGradients[r, c] = 255;

                    }

                }

            }

            CuttingEdges(EdgeMatrix);

            for (i = 0; i <= (Width - 1); i++)
                for (j = 0; j <= (Height - 1); j++)
                {
                    EdgePosition[i, j] = EdgePosition[i, j] * 255;
                }

            return;

        }

        /// <summary>
        /// Perform hysterisis thresholding.
        /// </summary>
        private void CuttingEdges(int[,] Edges)
        {

            int i, j;
            int Limit = MatrixSize / 2;


            for (i = Limit; i <= (Width - 1) - Limit; i++)
                for (j = Limit; j <= (Height - 1) - Limit; j++)
                {
                    if (Edges[i, j] == 1)
                    {
                        EdgePosition[i, j] = 1;

                    }

                }

            for (i = Limit; i <= (Width - 1) - Limit; i++)
            {
                for (j = Limit; j <= (Height - 1) - Limit; j++)
                {
                    if (Edges[i, j] == 1)
                    {
                        EdgePosition[i, j] = 1;
                        Traverse(i, j);
                        PositionVisited[i, j] = 1;
                    }
                }
            }




            return;
        }

        /// <summary>
        /// Recursive function which performs double thresholding.
        /// </summary>
        private void Traverse(int X, int Y)
        {


            if (PositionVisited[X, Y] == 1)
            {
                return;
            }

            //1
            if (EdgeMatrix[X + 1, Y] == 2)
            {
                EdgePosition[X + 1, Y] = 1;
                PositionVisited[X + 1, Y] = 1;
                Traverse(X + 1, Y);
                return;
            }
            //2
            if (EdgeMatrix[X + 1, Y - 1] == 2)
            {
                EdgePosition[X + 1, Y - 1] = 1;
                PositionVisited[X + 1, Y - 1] = 1;
                Traverse(X + 1, Y - 1);
                return;
            }

            //3

            if (EdgeMatrix[X, Y - 1] == 2)
            {
                EdgePosition[X, Y - 1] = 1;
                PositionVisited[X, Y - 1] = 1;
                Traverse(X, Y - 1);
                return;
            }

            //4

            if (EdgeMatrix[X - 1, Y - 1] == 2)
            {
                EdgePosition[X - 1, Y - 1] = 1;
                PositionVisited[X - 1, Y - 1] = 1;
                Traverse(X - 1, Y - 1);
                return;
            }
            //5
            if (EdgeMatrix[X - 1, Y] == 2)
            {
                EdgePosition[X - 1, Y] = 1;
                PositionVisited[X - 1, Y] = 1;
                Traverse(X - 1, Y);
                return;
            }
            //6
            if (EdgeMatrix[X - 1, Y + 1] == 2)
            {
                EdgePosition[X - 1, Y + 1] = 1;
                PositionVisited[X - 1, Y + 1] = 1;
                Traverse(X - 1, Y + 1);
                return;
            }
            //7
            if (EdgeMatrix[X, Y + 1] == 2)
            {
                EdgePosition[X, Y + 1] = 1;
                PositionVisited[X, Y + 1] = 1;
                Traverse(X, Y + 1);
                return;
            }
            //8

            if (EdgeMatrix[X + 1, Y + 1] == 2)
            {
                EdgePosition[X + 1, Y + 1] = 1;
                PositionVisited[X + 1, Y + 1] = 1;
                Traverse(X + 1, Y + 1);
                return;
            }


            //PositionVisited[X, Y] = 1;
            return;
        }
    }
}
