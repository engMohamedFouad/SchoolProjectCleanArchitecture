using Microsoft.AspNetCore.Http;

namespace SchoolProject.Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadImage(string Location, IFormFile file);
    }
}
