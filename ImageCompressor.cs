using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageCompressBase64
{
    public class ImageCompressor:IImageCompressor
    {
        public string CompressImage(string InputImage, int Quality, string OutPutDirectory)
        {
            using (Bitmap mybitmab = new Bitmap(@InputImage))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                System.Drawing.Imaging.Encoder myEncoder =
                       System.Drawing.Imaging.Encoder.Quality;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, Quality);
                myEncoderParameters.Param[0] = myEncoderParameter;

                string NewOutputPath = @OutPutDirectory + "\\" + Path.GetFileNameWithoutExtension(InputImage) + Path.GetExtension(InputImage);

                mybitmab.Save(NewOutputPath, jpgEncoder, myEncoderParameters);

                return NewOutputPath;
            }

        }

        public string CompressImageFromBase64(string InputPath, int Quality, string OutPutDirectory)
        {
            string base64 = System.IO.File.ReadAllText(InputPath);
            byte[] bytes = Convert.FromBase64String(base64);
            var ImageDataStream = new MemoryStream(bytes);
            ImageDataStream.Position = 0;
            var ImageLocation = @$"Resources/{Guid.NewGuid().ToString("N")}.jpg";
            using (FileStream image = new FileStream(ImageLocation, FileMode.Create, FileAccess.Write))
            {
                ImageDataStream.WriteTo(image);
                image.Close();
                ImageDataStream.Close();
            }
            string ResourcePath = ImageLocation;
            string CompressedImagePath = CompressImage(ImageLocation, Quality, OutPutDirectory);
            byte[] imageArray = System.IO.File.ReadAllBytes(CompressedImagePath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            File.Delete(ResourcePath);
            //File.Delete(CompressedImagePath);
            return base64ImageRepresentation;

        }

        public string ConvertImageToBase64(string ImagePath) 
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(ImagePath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

        public bool deleteFile(string Path) 
        {
            if (File.Exists(Path))
            {
                Console.WriteLine("File Exists!");
                File.Delete(Path);
                return true;
            }
            else 
            {
                return false;
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }


        

    }
}
