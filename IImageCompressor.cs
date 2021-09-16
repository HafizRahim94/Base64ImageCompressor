using System.Drawing;
using System.Drawing.Imaging;

namespace ImageCompressBase64
{
    public interface IImageCompressor
    {
        string CompressImage(string InputImage, int Quality, string OutPutDirectory);
        string CompressImageFromBase64(string InputPath, int Quality, string OutPutDirectory);
        string ConvertImageToBase64(string ImagePath);
        bool deleteFile(string Path);
    }
}