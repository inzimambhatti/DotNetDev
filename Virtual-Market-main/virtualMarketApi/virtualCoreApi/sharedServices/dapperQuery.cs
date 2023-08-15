using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Options;
using virtualCoreApi.Entities;
using Npgsql;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
// using System.Drawing;

namespace virtualCoreApi.Services
{
    public class dapperQuery
    {
        public static IEnumerable<T> Qry<T>(string sql, IOptions<conStr> conStr)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(conStr.Value.dbCon))
            {
                return con.Query<T>(sql);
            }
        }
        
        public static IEnumerable<T> QryResult<T>(string sql, IOptions<conStr> conStr)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(conStr.Value.dbCon))
            {
                return con.Query<T>(sql).ToList();
            }
        }

        public static string saveImageFile(string regPath, string name, string binData, string ext)
        {
            String path = regPath; //Path
            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = name + "." + ext;

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(binData);

            System.IO.File.WriteAllBytes(imgPath, imageBytes);

            return "Ok";
        }

        public static void SaveCompressedImage(string regPath, string name, string binData, string ext)
        {
            string path = regPath; // Path
            // Check if directory exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); // Create directory if it doesn't exist
            }

            string imageName = name + "." + ext;

            // Set the image path
            string imagePath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(binData);

            using (Image image = Image.Load(imageBytes))
            {
                // Compress the image to a maximum size of 400KB
                const int maxFileSizeInBytes = 400 * 1024;
                using (MemoryStream compressedStream = new MemoryStream())
                {
                    IImageEncoder encoder;

                    // Choose the appropriate image encoder based on the file extension
                    switch (ext.ToLower())
                    {
                        case "jpeg":
                        case "jpg":
                            encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder();
                            break;
                        case "png":
                            encoder = new SixLabors.ImageSharp.Formats.Png.PngEncoder();
                            break;
                        // Add more cases for other supported image formats if needed
                        default:
                            throw new NotSupportedException($"Unsupported file format: {ext}");
                    }

                    // Adjust the image quality or compression level if needed
                    encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder()
                    {
                        Quality = 75 // Adjust the image quality here (0-100)
                    };

                    // Resize the image if needed
                    const int maxWidth = 800;
                    const int maxHeight = 800;
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new SixLabors.Primitives.Size(maxWidth, maxHeight),
                        Mode = ResizeMode.Max
                    }));

                    // Save the compressed image to the output stream
                    image.Save(compressedStream, encoder);

                    // Check if the compressed image size exceeds the maximum allowed size
                    if (compressedStream.Length > maxFileSizeInBytes)
                    {
                        // Handle the case when the image cannot be compressed to the desired size
                        throw new InvalidOperationException("The image cannot be compressed to the desired file size.");
                    }

                    // Save the compressed image to the file
                    using (FileStream outputFile = new FileStream(imagePath, FileMode.Create))
                    {
                        compressedStream.Seek(0, SeekOrigin.Begin);
                        compressedStream.CopyTo(outputFile);
                    }
                }
            }
        }



    }
}