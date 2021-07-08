//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Runtime.Serialization;

namespace CovidPassReader
{
    [DataContract]
    public enum BlurType
    {
        Both,
        HorizontalOnly,
        VerticalOnly,
    }

    [DataContract]
    public class GaussianBlur
    {
        #region Private Fields

        private BlurType _blurType;
        private int[] _kernel;
        private int _kernelSum;
        private int[,] _multable;
        private int _radius = 6;

        #endregion Private Fields

        #region Public Constructors

        public GaussianBlur()
        {
            PreCalculateSomeStuff();
        }

        public GaussianBlur(int radius)
        {
            _radius = radius;
            PreCalculateSomeStuff();
        }

        #endregion Public Constructors

        #region Public Properties

        public BlurType BlurType
        {
            get { return _blurType; }
            set
            {
                _blurType = value;
            }
        }

        public int Radius
        {
            get { return _radius; }
            set
            {
                if (value < 2)
                {
                    throw new InvalidOperationException("Radius must be greater then 1");
                }
                _radius = value;
                PreCalculateSomeStuff();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public Image ProcessImage(Image inputImage)
        {
            Bitmap origin = new Bitmap(inputImage);
            Bitmap blurred = new Bitmap(inputImage.Width, inputImage.Height);

            using (RawBitmap src = new RawBitmap(origin))
            {
                using (RawBitmap dest = new RawBitmap(blurred))
                {
                    int pixelCount = src.Width * src.Height;
                    int[] b = new int[pixelCount];
                    int[] g = new int[pixelCount];
                    int[] r = new int[pixelCount];

                    int[] b2 = new int[pixelCount];
                    int[] g2 = new int[pixelCount];
                    int[] r2 = new int[pixelCount];

                    int offset = src.GetOffset();
                    int index = 0;
                    unsafe
                    {
                        byte* ptr = src.Begin;
                        for (int i = 0; i < src.Height; i++)
                        {
                            for (int j = 0; j < src.Width; j++)
                            {
                                b[index] = *ptr;
                                ptr++;
                                g[index] = *ptr;
                                ptr++;
                                r[index] = *ptr;
                                ptr++;

                                ++index;
                            }
                            ptr += offset;
                        }

                        int bsum;
                        int gsum;
                        int rsum;
                        int sum;
                        int read;
                        int start = 0;
                        index = 0;
                        if (_blurType != BlurType.VerticalOnly)
                        {
                            for (int i = 0; i < src.Height; i++)
                            {
                                for (int j = 0; j < src.Width; j++)
                                {
                                    bsum = gsum = rsum = sum = 0;
                                    read = index - _radius;

                                    for (int z = 0; z < _kernel.Length; z++)
                                    {
                                        if (read >= start && read < start + src.Width)
                                        {
                                            bsum += _multable[z, b[read]];
                                            gsum += _multable[z, g[read]];
                                            rsum += _multable[z, r[read]];
                                            sum += _kernel[z];
                                        }
                                        ++read;
                                    }

                                    b2[index] = (bsum / sum);
                                    g2[index] = (gsum / sum);
                                    r2[index] = (rsum / sum);

                                    if (_blurType == BlurType.HorizontalOnly)
                                    {
                                        byte* pcell = dest[j, i];
                                        pcell[0] = (byte)(bsum / sum);
                                        pcell[1] = (byte)(gsum / sum);
                                        pcell[2] = (byte)(rsum / sum);
                                    }

                                    ++index;
                                }
                                start += src.Width;
                            }
                        }
                        if (_blurType == BlurType.HorizontalOnly)
                        {
                            return blurred;
                        }

                        int tempy;
                        for (int i = 0; i < src.Height; i++)
                        {
                            int y = i - _radius;
                            start = y * src.Width;
                            for (int j = 0; j < src.Width; j++)
                            {
                                bsum = gsum = rsum = sum = 0;
                                read = start + j;
                                tempy = y;
                                for (int z = 0; z < _kernel.Length; z++)
                                {
                                    if (tempy >= 0 && tempy < src.Height)
                                    {
                                        if (_blurType == BlurType.VerticalOnly)
                                        {
                                            bsum += _multable[z, b[read]];
                                            gsum += _multable[z, g[read]];
                                            rsum += _multable[z, r[read]];
                                        }
                                        else
                                        {
                                            bsum += _multable[z, b2[read]];
                                            gsum += _multable[z, g2[read]];
                                            rsum += _multable[z, r2[read]];
                                        }
                                        sum += _kernel[z];
                                    }
                                    read += src.Width;
                                    ++tempy;
                                }

                                byte* pcell = dest[j, i];
                                pcell[0] = (byte)(bsum / sum);
                                pcell[1] = (byte)(gsum / sum);
                                pcell[2] = (byte)(rsum / sum);
                            }
                        }
                    }
                }
            }

            return blurred;
        }

        #endregion Public Methods

        #region Private Methods

        private void PreCalculateSomeStuff()
        {
            int sz = _radius * 2 + 1;
            _kernel = new int[sz];
            _multable = new int[sz, 256];
            for (int i = 1; i <= _radius; i++)
            {
                int szi = _radius - i;
                int szj = _radius + i;
                _kernel[szj] = _kernel[szi] = (szi + 1) * (szi + 1);
                _kernelSum += (_kernel[szj] + _kernel[szi]);
                for (int j = 0; j < 256; j++)
                {
                    _multable[szj, j] = _multable[szi, j] = _kernel[szj] * j;
                }
            }
            _kernel[_radius] = (_radius + 1) * (_radius + 1);
            _kernelSum += _kernel[_radius];
            for (int j = 0; j < 256; j++)
            {
                _multable[_radius, j] = _kernel[_radius] * j;
            }
        }

        #endregion Private Methods
    }

    public unsafe class RawBitmap : IDisposable
    {
        #region Private Fields

        private byte* _begin;
        private BitmapData _bitmapData;
        private Bitmap _originBitmap;

        #endregion Private Fields

        #region Public Constructors

        public RawBitmap(Bitmap originBitmap)
        {
            _originBitmap = originBitmap;
            _bitmapData = _originBitmap.LockBits(new Rectangle(0, 0, _originBitmap.Width, _originBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            _begin = (byte*)(void*)_bitmapData.Scan0;
        }

        #endregion Public Constructors

        #region Public Properties

        public unsafe byte* Begin
        {
            get { return _begin; }
        }

        public int Height
        {
            get { return _bitmapData.Height; }
        }

        public Bitmap OriginBitmap
        {
            get { return _originBitmap; }
        }

        public int Stride
        {
            get { return _bitmapData.Stride; }
        }

        //public unsafe void SetColor(int x, int y, int color)
        //{
        //    *(int*)(_begin + y * (_bitmapData.Stride) + x * 3) = color;
        //}
        public int Width
        {
            get { return _bitmapData.Width; }
        }

        #endregion Public Properties

        #region Public Indexers

        public unsafe byte* this[int x, int y]
        {
            get
            {
                return _begin + y * (_bitmapData.Stride) + x * 3;
            }
        }

        public unsafe byte* this[int x, int y, int offset]
        {
            get
            {
                return _begin + y * (_bitmapData.Stride) + x * 3 + offset;
            }
        }

        #endregion Public Indexers

        #region Public Methods

        public void Dispose()
        {
            _originBitmap.UnlockBits(_bitmapData);
            GC.SuppressFinalize(this);
        }

        public int GetOffset()
        {
            return _bitmapData.Stride - _bitmapData.Width * 3;
        }

        #endregion Public Methods
    }
}