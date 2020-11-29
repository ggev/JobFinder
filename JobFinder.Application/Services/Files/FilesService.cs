using JobFinder.Application.Dtos.FileModels;
using JobFinder.Application.Enums;
using Microsoft.Extensions.Caching.Distributed;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;
using JobFinder.Application.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace JobFinder.Application.Services.Files
{
    public class FilesService : IFilesService
    {
        private readonly string _webRootPath;
        private readonly IDistributedCache _cache;

        public FilesService(IWebHostEnvironment webHostEnvironment, IDistributedCache cache)
        {
            _webRootPath = webHostEnvironment.WebRootPath;
            _cache = cache;
        }


        public async Task<string> FileUploader(IFormFile file, UploadType type, bool isRelativeRequested = false)
        {
            if (file.Length <= 0)
                return null;
            var dest = file.FileName.GetDestFileName();
            dest = FilesUtilities.GetRelativePath(dest, type);
            var newFile = Path.Combine(_webRootPath, dest);
            try
            {
                Path.GetDirectoryName(newFile).CreateDirectory();
                await using (var stream = new FileStream(newFile, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return !isRelativeRequested ? Path.GetFileName(dest) : dest;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                RemoveFile(dest, type);
                return null;
            }
        }

        public async Task<DownloadFileModel> Resize(UploadType type, string fileName, int maxWidth, int maxHeight)
        {
            int width;
            int height;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var mime);
            var resultFileName = FilesUtilities.GetRelativePath(fileName, type);
            var file = Path.Combine(_webRootPath, resultFileName);
            if (mime == null)
                return null;
            if (mime.StartsWith("video"))
                return new DownloadFileModel
                {
                    ContentType = mime,
                    Bytes = await File.ReadAllBytesAsync(file)
                };
            var theFile = new FileInfo(file);
            if (!theFile.Exists)
                return null;
            var key = fileName.CacheKeyGenerator(type, maxWidth, maxHeight);
            var fromCache = await _cache.GetAsync(key);
            if (fromCache != null)
                return new DownloadFileModel
                {
                    Bytes = fromCache,
                    ContentType = mime
                };
            byte[] fileBytes;
            try
            {
                using var current = await Image.LoadAsync(file);
                if (current.Width > current.Height)
                {
                    width = maxWidth;
                    height = Convert.ToInt32(current.Height * maxWidth / (double)current.Width);
                }
                else
                {
                    width = Convert.ToInt32(current.Width * maxHeight / (double)current.Height);
                    height = maxHeight;
                }
                current.Mutate(x => x.Resize(width, height).ApplyProcessors());
                fileBytes = current.ToByteArray(Path.GetExtension(resultFileName));
                current.Dispose();
            }
            catch (IOException)
            {
                return null;
            }
            if (string.IsNullOrEmpty(mime))
                mime = "image/jpg";
            await _cache.SetAsync(key, fileBytes);
            return new DownloadFileModel
            {
                Bytes = fileBytes,
                ContentType = mime
            };
        }

        public bool RemoveFile(string fileName, UploadType type)
        {
            var path = Path.Combine(_webRootPath, FilesUtilities.GetRelativePath(fileName, type)).SlashConverter();
            if (!File.Exists(path))
                return true;
            File.Delete(path);
            return true;
        }
    }
}