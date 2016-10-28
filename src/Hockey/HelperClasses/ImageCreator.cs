using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hockey.HelperClasses
{
    public class ImageCreator
    {
        public static byte[] _data;
        public static string _imageData;
        public static string _uploads;
        public static string _fileName;
        public ImageCreator()
        {
            _data = ImageFromDOMHelper._data;
            _imageData = ImageFromDOMHelper._imageData;

        }
        public void ImageCreate()
        {
            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(_data))
            {
                image = System.Drawing.Image.FromStream(ms);
                image.Save(_fileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            //    using (var fileStream = new FileStream(Path.Combine(_uploads, _fileName), FileMode.Create)
            //    {
            //        image. .CopyToAsync(fileStream);
            //}
            //imageFile.Flush();
        }
    }
}