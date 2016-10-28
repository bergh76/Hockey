using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hockey.HelperClasses
{

    public class ImageFromDOMHelper
    {
        public static string _imageData { get; set; }
        public static string _fileExtension { get; set; }
        public static Match _imageMatch { get; set; }
        public static string _mimeType { get; set; }

        public static byte[] _data { get; set; }
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
    }
}
