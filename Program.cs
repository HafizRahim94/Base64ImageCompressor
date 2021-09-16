using System;
using System.IO;

namespace ImageCompressBase64
{
    class Program
    {

        static void Main(string[] args)
        {
            IImageCompressor _comp = new ImageCompressor();
            try {
                Console.WriteLine("Enter Path to .txt file that contains base64 image string:");
                string InputPath = Console.ReadLine();
                if (!File.Exists(InputPath))
                {
                    Console.WriteLine("File not exists");
                }
                else 
                {
                    Console.WriteLine("Enter Output Directory:");
                    string OutputDir = Console.ReadLine();
                    Console.WriteLine("Enter Image quality 1 - 100, higher quality equals greater size");
                    int Quality = Int32.Parse(Console.ReadLine());
                    Console.Write(_comp.CompressImageFromBase64(@InputPath, Quality, @OutputDir));

                }
                
                //Console.Write(_comp.CompressImageFromBase64(@"Resources/img.txt", 1, @"CompressedImage/"));
                //Console.WriteLine(_comp.ConvertImageToBase64("Resources/img.jpg"));
                //File.WriteAllText("Resources/img.txt", _comp.ConvertImageToBase64("Resources/img.jpg"));
                //_comp.CompressImage(@"Resources/img.jpg", 5, @"CompressedImage/");

            } catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

    }
}
