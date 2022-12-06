namespace HomeAppliances.Services
{
    public interface IFileStorage
    {
        Task<string> EditFile(byte[] content, string extension, string container, string route,
            string contentType);
        Task DeleteFile(string route, string container);
        Task<string> SaveFile(string container, IFormFile image);
    }
}
