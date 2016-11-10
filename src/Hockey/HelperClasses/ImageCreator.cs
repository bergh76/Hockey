using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            //_data = ImageFromDOMHelper._data;
            //_imageData = ImageFromDOMHelper._imageData;

        }

        public static string _fileExtension { get; set; }
        public static Match _imageMatch { get; set; }
        public static string _mimeType { get; set; }

        
        public string ImageData(string imageData)
        {
            if (string.IsNullOrEmpty(imageData))
                throw new ArgumentNullException(nameof(imageData), "No image data received");

            Match imageMatch = Regex.Match(imageData, @"^data:(?<mimetype>[^;]+);base64,(?<data>.+)$");
            if (!imageMatch.Success)
                throw new ArgumentException("imageData is in unknown format", nameof(imageData));

            string mimeType = imageMatch.Groups["mimetype"].Value;
            Match imageType = Regex.Match(mimeType, @"^[^/]+/(?<type>.+?)$");
            if (!imageType.Success)
                throw new ArgumentException($"mimeType format invalid for {mimeType}", nameof(mimeType));

            string fileExtension = imageType.Groups["type"].Value;
            byte[] data = Convert.FromBase64String(imageMatch.Groups["data"].Value);

            _data = data;
            _imageData = imageData;
            _fileExtension = fileExtension;
            _imageMatch = imageMatch;
            _mimeType = mimeType;

            return imageData;
        }

        public void ImageCreate()
        {
            string ext = _fileExtension;
            var path = _uploads + "/" + _fileName;
            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(_data))
            {
                image = System.Drawing.Image.FromStream(ms);
                if (File.Exists(path) || File.Exists(Path.Combine(Directory.GetParent(Path.GetDirectoryName(path)).FullName, Path.GetFileName(path))) == true)
                {
                    int countFInDir = Directory.GetFiles(_uploads, "*", SearchOption.TopDirectoryOnly).Length; // count existing files in topdirectory ie. uploads
                    int addCount = countFInDir + 1; // increment filecount 
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