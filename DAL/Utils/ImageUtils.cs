using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DAL.Utils
{
    public class ImageUtils
    {
        public static BitmapImage ByteArrayToBitmapImage(byte[] picture)
        {
            using (var memoryStrem = new MemoryStream(picture))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memoryStrem;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }
        public static byte[] BitmapImageToByteArray(BitmapImage image)
        {
            var jpegEncoder = new JpegBitmapEncoder();
            jpegEncoder.Frames.Add(BitmapFrame.Create(image));
            using (var memoryStrem = new MemoryStream())
            {

                jpegEncoder.Save(memoryStrem);
                return memoryStrem.ToArray();
            }
        }

        internal static byte[] ByteArrayFromDataReader(SqlDataReader dr, string column)
        {
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];

            int currentBytes = 0;

            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    int readBytes;
                    do
                    {

                        readBytes = (int)dr.GetBytes(
                            dr.GetOrdinal(column),
                            currentBytes,
                            buffer,
                            0, bufferSize
                            );
                        binaryWriter.Write(buffer, 0, bufferSize);
                        currentBytes += readBytes;
                    } while (readBytes == bufferSize);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
