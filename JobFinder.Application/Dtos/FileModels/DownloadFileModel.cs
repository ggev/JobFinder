namespace JobFinder.Application.Dtos.FileModels
{
    public sealed class DownloadFileModel
    {
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
    }
}