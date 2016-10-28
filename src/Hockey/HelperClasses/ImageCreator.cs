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
            var path = _uploads + "/" + _fileName;
            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(_data))
            {
                image = System.Drawing.Image.FromStream(ms);
                if (File.Exists(path) || File.Exists(Path.Combine(Directory.GetParent(Path.GetDirectoryName(path)).FullName, Path.GetFileName(path))) == true)
                {
                    int countFInDir = Directory.GetFiles(_uploads, "*", SearchOption.TopDirectoryOnly).Length; // count existing files in topdirectory ie. uploads
                    int addCount = countFInDir + 1; // increment filecount 
                    string ext = ImageFromDOMHelper._fileExtension;
                    string noExt = _fileName.Replace("." + ext, "");
                    string tmpNameThree = string.Format("{0}_{1}.{2}", noExt, addCount, ext);
                    string newFnameExists = tmpNameThree.Replace(" ", "_");
                    path = _uploads + "/" + newFnameExists;
                    //TODO: Add new file to Image in db
                    image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                }
                else { image.Save(path, System.Drawing.Imaging.ImageFormat.Png); }
            }
            image.Dispose();
        }
    }
}