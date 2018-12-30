using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IdleMouse.Interop.IcoCurAni
{
    internal class CursorModel
    {
        public CursorModel(string name, string path, IntPtr handle, string type, long size, CursorFrame[] frames, uint hotspotX, uint hotspotY, uint defaultFrameRate, uint[] frameRates, uint[] frameNums)
        {
            Name = name;
            Path = path;
            Handle = handle;
            Type = type;
            Size = size;
            Frames = frames;
            HotspotX = hotspotX;
            HotspotY = hotspotY;
            DefaultFrameRate = defaultFrameRate;
            FrameRates = frameRates;
            FrameNums = frameNums;
        }

        public string Name { get; }
        public string Path { get; }
        public IntPtr Handle { get; private set; }
        public string Type { get; }
        public long Size { get; }

        public CursorFrame[] Frames { get; }
        public uint HotspotX { get; }
        public uint HotspotY { get; }
        public uint DefaultFrameRate { get; }
        public uint[] FrameRates { get; }
        public uint[] FrameNums { get; }

        public void CreateHandle()
        {
            if (Handle == IntPtr.Zero && !string.IsNullOrEmpty(Path))
            {
                Handle = Win32.LoadCursorFromFileW(Path);
                if (Handle == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }
    }

    internal class CursorFrame
    {
        public CursorFrame(Image image)
        {
            Width = (ushort)image.Width;
            Height = (ushort)image.Height;
            Frame = image;
        }

        public ushort Width { get; }
        public ushort Height { get; }
        public Image Frame { get; }
    }

    internal static class CursorReader
    {
        private const string FOURCC_ACON = "ACON";
        private const string FOURCC_anih = "anih";
        private const string FOURCC_fram = "fram";
        private const string FOURCC_IART = "IART";
        private const string FOURCC_icon = "icon";
        private const string FOURCC_INAM = "INAM";
        private const string FOURCC_INFO = "INFO";
        private const string FOURCC_LIST = "LIST";
        private const string FOURCC_rate = "rate";
        private const string FOURCC_seq = "seq ";
        private const string FOURCC_RIFF = "RIFF";

        private const uint PNG_SIG = 0x474E5089;

        private class RiffChunk
        {
            public RiffChunk(string type, uint size, string subType)
            {
                Type = type;
                Size = size;
                SubType = subType;
            }

            public string Type { get; }
            public uint Size { get; }
            public string SubType { get; }
        }

        private struct IcoHeader
        {
            public ushort Reserved;
            public ushort Type; // 1=icon, 2=cursor
            public ushort Count;
        }

        private struct DirectoryEntry
        {
            public byte bWidth;
            public byte bHeight;
            public byte bColorCount;
            public byte bReserved;
            public ushort Planes_XHotspot;     // "Union" for planes (icons) and xhotspot (cursors)
            public ushort BitCount_YHotspot;   // "Union" for bit count (icons) and yhotspot (cursors)
            public uint dwBytesInRes;
            public uint dwImageOffset;
        }

        private struct BITMAPINFOHEADER
        {
            public uint StructSize;
            public int Width;
            public int Height;
            public ushort Planes;
            public ushort BitCount;
            public uint biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
        }

        private struct PaletteEntry
        {
            public byte R;
            public byte G;
            public byte B;
            //public byte A;
        }

        public static CursorModel Load(string name, string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                if (string.IsNullOrEmpty(name)) name = Path.GetFileNameWithoutExtension(filename);
                return Load(name, filename, IntPtr.Zero, stream);
            }
        }

        public static CursorModel Load(string name, IntPtr handle, byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                return Load(name, null, handle, stream);
            }
        }

        public static CursorModel Load(string name, string path, IntPtr handle, Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (!stream.CanRead) throw new ArgumentException("stream does not support reading");
            if (!stream.CanSeek) throw new ArgumentException("stream does not support seeking");

            string type;
            long size = stream.Length;

            int byteZero = stream.ReadByte();
            if (byteZero < 0) return null;
            stream.Seek(0, SeekOrigin.Begin);

            // CUR/ICO format
            if (byteZero == 0)
            {
                ushort hotspotX, hotspotY;
                var frame = ReadCur(stream, out hotspotX, out hotspotY, out type);
                return new CursorModel(name, path, handle, type, size, new CursorFrame[] { frame }, hotspotX, hotspotY, 0, new uint[] { 0 }, new uint[] { 0 });
            }

            // ANI format (uses RIFF container)
            using (var reader = new BinaryReader(stream, Encoding.ASCII))
            {
                type = "ANI";
                var root = ReadRiffChunk(reader);
                if (root == null || root.Type != FOURCC_RIFF || root.SubType != FOURCC_ACON) return null;

                uint? hotspotX = null, hotspotY = null;
                uint frameCount = 0, stepCount, defWidth, defHeight, bitcount, planes, defaultFrameRate = 0, flags;
                uint[] frameRates = null;
                uint[] frameNums = null;

                var frames = new List<CursorFrame>();

                RiffChunk chunk;
                while ((chunk = ReadRiffChunk(reader)) != null)
                {
                    if (chunk.Type == FOURCC_anih)
                    {
                        reader.ReadUInt32(); // anih size, again
                        frameCount = reader.ReadUInt32();
                        stepCount = reader.ReadUInt32();
                        defWidth = reader.ReadUInt32();
                        defHeight = reader.ReadUInt32();
                        bitcount = reader.ReadUInt32();
                        planes = reader.ReadUInt32();
                        defaultFrameRate = reader.ReadUInt32();
                        flags = reader.ReadUInt32();
                    }
                    else if (chunk.Type == FOURCC_rate)
                    {
                        frameRates = ReadUInt32Array(reader, chunk.Size / 4);
                    }
                    else if (chunk.Type == FOURCC_seq)
                    {
                        frameNums = ReadUInt32Array(reader, chunk.Size / 4);
                    }
                    else if (chunk.Type == FOURCC_icon)
                    {
                        var buffer = reader.ReadBytes((int)chunk.Size);
                        using (var memoryStream = new MemoryStream(buffer))
                        {
                            ushort _hotspotX, _hotspotY;
                            string _type; // Ignored
                            var frame = ReadCur(memoryStream, out _hotspotX, out _hotspotY, out _type);
                            frames.Add(frame);
                            if (!hotspotX.HasValue) hotspotX = _hotspotX;
                            if (!hotspotY.HasValue) hotspotY = _hotspotY;
                        }
                    }
                    else if (chunk.Type != FOURCC_LIST)
                    {
                        reader.ReadBytes((int)chunk.Size);
                    }
                    // Padding byte where chunk size is not even
                    if (chunk.Size % 2 == 1) reader.ReadByte();
                }

                if (frameRates == null) frameRates = Enumerable.Repeat(defaultFrameRate, (int)frameCount).ToArray();
                if (frameNums == null) frameNums = Enumerable.Range(0, (int)frameCount).Select(i => (uint)i).ToArray();

                return new CursorModel(name, path, handle, type, size, frames.ToArray(), hotspotX ?? 0, hotspotY ?? 0, defaultFrameRate, frameRates, frameNums);
            }
        }

        private static CursorFrame ReadCur(Stream stream, out ushort hotspotX, out ushort hotspotY, out string type)
        {
            using (var reader = new BinaryReader(stream))
            {
                var header = new IcoHeader();
                header.Reserved = reader.ReadUInt16();
                header.Type = reader.ReadUInt16();
                header.Count = reader.ReadUInt16();

                var entries = new DirectoryEntry[header.Count];
                for (int i = 0; i < header.Count; i++)
                {
                    entries[i].bWidth = reader.ReadByte();
                    entries[i].bHeight = reader.ReadByte();
                    entries[i].bColorCount = reader.ReadByte();
                    entries[i].bReserved = reader.ReadByte();
                    entries[i].Planes_XHotspot = reader.ReadUInt16();
                    entries[i].BitCount_YHotspot = reader.ReadUInt16();
                    entries[i].dwBytesInRes = reader.ReadUInt32();
                    entries[i].dwImageOffset = reader.ReadUInt32();
                }

                // Find the closest entry to 32x32 will resize down later if necessary
                var entry = entries.Where(de => de.bWidth >= 32 && de.bHeight >= 32).OrderBy(de => de.bWidth).First();

                hotspotX = entry.Planes_XHotspot;
                hotspotY = entry.BitCount_YHotspot;

                // Seek to the start of the data
                reader.BaseStream.Seek(entry.dwImageOffset, SeekOrigin.Begin);

                // Read image data
                uint sig = reader.ReadUInt32();
                if (sig == PNG_SIG)
                {
                    type = "CUR (PNG)";
                    // Icon is a PNG 
                    using (var pngStream = new MemoryStream())
                    {
                        // Write back the header
                        pngStream.Write(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, 0, 4);

                        // Copy the rest of the PNG
                        byte[] png = reader.ReadBytes((int)entry.dwBytesInRes);
                        pngStream.Write(png, 0, png.Length);

                        var bitmap = new Bitmap(pngStream);
                        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY); // Cursor PNG is stored upside down

                        return new CursorFrame(bitmap);
                    }
                }
                else
                {
                    type = "CUR";
                    uint w = 0, h = 0, bpp = 0;
                    var bitmapInfoHeader = new BITMAPINFOHEADER();
                    bitmapInfoHeader.StructSize = sig; // Already read above
                    bitmapInfoHeader.Width = reader.ReadInt32();
                    bitmapInfoHeader.Height = reader.ReadInt32();
                    bitmapInfoHeader.Planes = reader.ReadUInt16();
                    bpp = bitmapInfoHeader.BitCount = reader.ReadUInt16();
                    bitmapInfoHeader.biCompression = reader.ReadUInt32();
                    bitmapInfoHeader.biSizeImage = reader.ReadUInt32();
                    bitmapInfoHeader.biXPelsPerMeter = reader.ReadInt32();
                    bitmapInfoHeader.biYPelsPerMeter = reader.ReadInt32();
                    bitmapInfoHeader.biClrUsed = reader.ReadUInt32();
                    bitmapInfoHeader.biClrImportant = reader.ReadUInt32();

                    // Determine size
                    uint actualSize = SizeComp(w, h, bpp) + SizeComp(w, h, 1);
                    if (w == 0 && h == 0)
                    {
                        w = (uint)bitmapInfoHeader.Width;
                        h = (uint)bitmapInfoHeader.Height / 2;
                    }
                    else if (actualSize != entry.dwBytesInRes)
                    {
                        if (w == 255) w = 256;
                        if (h == 255) h = 256;
                    }
                    if (w == 0) w = 256;
                    if (h == 0) h = 256;

                    var palette = new PaletteEntry[256];
                    if (bpp <= 8)
                    {
                        int paletteSize = (1 << (int)bpp);
                        for (int i = 0; i < paletteSize; i++)
                        {
                            palette[i].R = reader.ReadByte();
                            palette[i].G = reader.ReadByte();
                            palette[i].B = reader.ReadByte();
                        }
                    }

                    byte[] imgBytes = new byte[SizeComp(w, h, bpp)];
                    reader.Read(imgBytes, 0, imgBytes.Length);
                    var bm = CreateBitmap(imgBytes, palette, (int)w, (int)h, (int)bpp, true);
                    
                    // Alpha mask handling for images other than 32bpp
                    if (bm != null && bpp != 32)
                    {
                        // Generate an alpha channel from the mask
                        int maskSize = (int)SizeComp(w, h, 1);
                        byte[] maskBytes = new byte[maskSize];
                        reader.Read(maskBytes, 0, maskBytes.Length);

                        var pixelCount = bm.Width * bm.Height;
                        if (pixelCount == maskSize * 8)
                        {
                            var region = new Rectangle(0, 0, bm.Width, bm.Height);
                            var bitmapData = bm.LockBits(region, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                            byte[] pixelData = new byte[bm.Width * bm.Height * 4];
                            Marshal.Copy(bitmapData.Scan0, pixelData, 0, pixelData.Length);
                            for (int i = 0; i < pixelCount; i++)
                            {
                                var maskBit = (uint)maskBytes[i / 8];
                                maskBit = (maskBit >> (7 - (i % 8))) & 1;
                                pixelData[i * 4] = (byte)(maskBit == 0 ? 0xFF : 0x00); // Set alpha
                            }
                            Marshal.Copy(pixelData, 0, bitmapData.Scan0, pixelData.Length);
                            bm.UnlockBits(bitmapData);
                        }
                    }

                    if (bm.Width > 32)
                    {
                        // TODO May need to respect aspect ratio, although cursors are usually square
                        bm = new Bitmap(bm, new Size(32, 32));
                    }
                    
                    return new CursorFrame(bm);
                }
            }
        }

        private static RiffChunk ReadRiffChunk(BinaryReader reader)
        {
            if (reader.PeekChar() < 0)
            {
                return null;
            }

            var typeRaw = reader.ReadBytes(4);
            var type = Encoding.ASCII.GetString(typeRaw);
            string subType = null;
            var size = reader.ReadUInt32();
            if (type == FOURCC_RIFF || type == FOURCC_LIST)
            {
                var subTypeRaw = reader.ReadBytes(4);
                subType = Encoding.ASCII.GetString(subTypeRaw);
            }
            return new RiffChunk(type, size, subType);
        }

        private static uint[] ReadUInt32Array(BinaryReader reader, uint count)
        {
            var result = new uint[count];
            for (uint i = 0; i < count; i++)
            {
                result[i] = reader.ReadUInt32();
            }
            return result;
        }

        private static int SizeComp(int w, int h, int bpp)
        {
            // Compute the row size
            int RowSize = w * bpp / 8;
            if (RowSize % 4 != 0)
            {
                RowSize += (4 - (RowSize % 4));
            }
            return h * RowSize;
        }

        private static uint SizeComp(uint w, uint h, uint bpp)
        {
            // Compute the row size
            uint RowSize = w * bpp / 8;
            if (RowSize % 4 != 0)
            {
                RowSize += (4 - (RowSize % 4));
            }
            return h * RowSize;
        }

        private static Bitmap CreateBitmapArgb(byte[] pixelData, int w, int h)
        {
            var bm = new Bitmap(w, h);
            var rect = new Rectangle(0, 0, w, h);
            var bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixelData, 0, bd.Scan0, w * h * 4);
            bm.UnlockBits(bd);
            bm.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bm;
        }

        private static Bitmap CreateBitmap(byte[] pixelData, PaletteEntry[] palette, int w, int h, int bpp, bool paddedTo32Bit)
        {
            switch (bpp)
            {
                case 32: // ARGB_8888
                    return CreateBitmapArgb(pixelData, w, h);
                case 24: // RGB_888, add opaque alpha channel
                    {
                        int rowSize = w * 3;
                        if (paddedTo32Bit && rowSize % 4 != 0)
                        {
                            rowSize += (4 - (rowSize % 4));
                        }

                        var argbPixelData = new byte[w * h * 4];
                        for (int y = 0; y < h; y++)
                        {
                            int destRowIndex = y * w * 4;
                            int srcRowIndex = y * rowSize;
                            for (int x = 0; x < w; x++)
                            {
                                int destIndex = destRowIndex + x * 4;
                                int srcIndex = srcRowIndex + x * 3;
                                argbPixelData[destIndex] = 0xFF;
                                argbPixelData[destIndex + 1] = pixelData[srcIndex];
                                argbPixelData[destIndex + 2] = pixelData[srcIndex + 1];
                                argbPixelData[destIndex + 3] = pixelData[srcIndex + 2];
                            }
                        }
                        return CreateBitmapArgb(argbPixelData, w, h);
                    }
                case 16:
                case 15:
                    // TODO Handle RGB_565 and RGB_555
                    break;
                case 8: // Indexed_8
                    {
                        int rowSize = w;
                        if (paddedTo32Bit && w % 4 != 0)
                        {
                            rowSize += (4 - (w % 4));
                        }
                        var argbPixelData = new byte[w * h * 4];
                        for (int y = 0; y < h; y++)
                        {
                            int destRowIndex = y * w * 4;
                            int srcRowIndex = y * rowSize;
                            for (int x = 0; x < w; x++)
                            {
                                int destIndex = destRowIndex + x * 4;
                                int srcIndex = srcRowIndex + x;
                                argbPixelData[destIndex] = 0xFF;
                                argbPixelData[destIndex + 1] = palette[pixelData[srcIndex]].R;
                                argbPixelData[destIndex + 2] = palette[pixelData[srcIndex]].G;
                                argbPixelData[destIndex + 3] = palette[pixelData[srcIndex]].B;
                            }
                        }
                        return CreateBitmapArgb(argbPixelData, w, h);
                    }
                case 4: // Indexed_4
                    {
                        int rowSize = w / 2;
                        if (paddedTo32Bit && w % 4 != 0)
                        {
                            rowSize += (4 - (w % 4));
                        }
                        var argbPixelData = new byte[w * h * 4];
                        for (int y = 0; y < h; y++)
                        {
                            int destRowIndex = y * w * 4;
                            int srcRowIndex = y * rowSize;
                            for (int x = 0; x < w; x++)
                            {
                                int destIndex = destRowIndex + x * 4;
                                int srcIndex = srcRowIndex + x / 2;
                                int index = (pixelData[srcIndex] & (x % 2 == 0 ? 0xF0 : 0x0F)) >> (x % 2 == 0 ? 4 : 0);
                                argbPixelData[destIndex] = 0xFF;
                                argbPixelData[destIndex + 1] = palette[index].R;
                                argbPixelData[destIndex + 2] = palette[index].G;
                                argbPixelData[destIndex + 3] = palette[index].B;
                            }
                        }
                        return CreateBitmapArgb(argbPixelData, w, h);
                    }
                case 2: // Indexed_2
                    {
                        int rowSize = w / 4;
                        if (paddedTo32Bit && w % 4 != 0)
                        {
                            rowSize += (4 - (w % 4));
                        }
                        var argbPixelData = new byte[w * h * 4];
                        for (int y = 0; y < h; y++)
                        {
                            int destRowIndex = y * w * 4;
                            int srcRowIndex = y * rowSize;
                            for (int x = 0; x < w; x++)
                            {
                                int destIndex = destRowIndex + x * 4;
                                int srcIndex = srcRowIndex + x / 4;
                                int shift = (4 - x % 4) * 2;
                                int index = (pixelData[srcIndex] & (0x3 << shift)) >> shift;
                                argbPixelData[destIndex] = 0xFF;
                                argbPixelData[destIndex + 1] = palette[index].R;
                                argbPixelData[destIndex + 2] = palette[index].G;
                                argbPixelData[destIndex + 3] = palette[index].B;
                            }
                        }
                        return CreateBitmapArgb(argbPixelData, w, h);
                    }
                case 1: // Indexed_1
                    {
                        int rowSize = w / 8;
                        if (paddedTo32Bit && w % 4 != 0)
                        {
                            rowSize += (4 - (w % 4));
                        }
                        var argbPixelData = new byte[w * h * 4];
                        for (int y = 0; y < h; y++)
                        {
                            int destRowIndex = y * w * 4;
                            int srcRowIndex = y * rowSize;
                            for (int x = 0; x < w; x++)
                            {
                                int destIndex = destRowIndex + x * 4;
                                int srcIndex = srcRowIndex + x / 8;
                                int shift = (8 - x % 8);
                                int index = (pixelData[srcIndex] & (1 << shift)) >> shift;
                                argbPixelData[destIndex] = 0xFF;
                                argbPixelData[destIndex + 1] = palette[index].R;
                                argbPixelData[destIndex + 2] = palette[index].G;
                                argbPixelData[destIndex + 3] = palette[index].B;
                            }
                        }
                        return CreateBitmapArgb(argbPixelData, w, h);
                    }
            }
            return null; // Unsupported bits per pixel
        }
    }
}
