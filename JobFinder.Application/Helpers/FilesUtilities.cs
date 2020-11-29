using System;
using System.IO;
using System.Text.RegularExpressions;
using JobFinder.Application.Enums;
using SixLabors.ImageSharp;

namespace JobFinder.Application.Helpers
{
    public static class FilesUtilities
    {
        public static string GetFileName(this string fileName)
        {
            fileName = Regex.Replace(fileName, @"\s+", "");
            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                fileName = fileName.Trim('"');
            if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                fileName = Path.GetFileName(fileName);
            return fileName;
        }

        public static string GetDestFileName(this string fileName)
        {
            fileName = fileName.GetFileName();
            return $"{Path.GetFileNameWithoutExtension(fileName)}-{DateTime.Now.Ticks}{Path.GetExtension(fileName)}";
        }

        public static string GetRelativePath(string fileName, UploadType uploadType)
        {
            if (string.IsNullOrEmpty(fileName))
                return fileName;
            fileName = Path.GetFileName(fileName);
            return uploadType switch
            {
                UploadType.Company => $"Medias/Company/{fileName}",
                _ => throw new ArgumentOutOfRangeException(nameof(uploadType))
            };
        }

        public static void CreateDirectory(this string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static string SlashConverter(this string value)
        {
            return value.Replace("/", "\\");
        }

        public static string CacheKeyGenerator(this string fileName, UploadType type, int width = 0, int height = 0)
        {
            if (width != 0 && height != 0)
                return $"Cache{fileName}/{type}/{width}/{height}";
            return $"Cache{fileName}/{type}";
        }

        public static byte[] ToByteArray(this Image current, string extension)
        {
            using var stream = new MemoryStream();
            if (string.IsNullOrEmpty(extension) || !extension.ToLower().Contains("png"))
                current.SaveAsJpeg(stream);
            else
                current.SaveAsPng(stream);
            return stream.ToArray();
        }
    }
}