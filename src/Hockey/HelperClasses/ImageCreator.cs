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
            var bytes = _data;
            using (var imageFile = new FileStream(_imageData, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                using (var fileStream = new FileStream(Path.Combine(_uploads, _fileName), FileMode.Create))
                {
                    imageFile.CopyToAsync(fileStream);
                }
                imageFile.Flush();
            }
        }
    }
}
