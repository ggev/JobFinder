using System.Threading.Tasks;
using JobFinder.Application.Dtos.FileModels;
using JobFinder.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Application.Services.Files
{
    public interface IFilesService
    {
        Task<string> FileUploader(IFormFile file, UploadType type, bool isRelativeRequested = false);
        Task<DownloadFileModel> Resize(UploadType type, string fileName, int maxWidth, int maxHeight);
        bool RemoveFile(string fileName, UploadType type);
    }
}