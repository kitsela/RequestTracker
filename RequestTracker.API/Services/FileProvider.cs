using ImageMagick;

namespace RequestTracker.API.Services
{
    public class FileProvider : IFileProvider
    {
        public byte[] TrackImgBytes { get; private set; }

        public FileProvider()
        {
            TrackImgBytes = CreateImgBytes(1, 1);
        }

        private byte[] CreateImgBytes(int imageWidth, int imageHeight)
        {
            byte[] data;

            // Create image 1x1
            using (var image = new MagickImage(MagickColor.FromRgb(0, 255, 0), imageWidth, imageHeight))
            {
                image.Format = MagickFormat.Gif;
                data = image.ToByteArray();
            }
            return data;
        }



        /// IT doesn't work inside linux container =(
        /// So I changed implementation
        

        //private byte[] CreateImgBytes(int imageWidth, int imageHeight)
        //{
        //    var imageStream = new MemoryStream();
        //    using (var bmp = new Bitmap(imageWidth, imageHeight))
        //    {
        //        using (var g = Graphics.FromImage(bmp))
        //        {
        //            g.Clear(Color.Green);
        //        }
        //        bmp.Save(imageStream, System.Drawing.Imaging.ImageFormat.Gif);
        //    }

        //    return imageStream.ToArray();
        //}
    }
}
