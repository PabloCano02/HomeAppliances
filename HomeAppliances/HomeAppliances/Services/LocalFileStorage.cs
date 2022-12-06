namespace HomeAppliances.Services
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalFileStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task DeleteFile(string route, string container)
        {
            if (route != null)
            {
                var fileName = Path.GetFileName(route);
                string fileDirectory = Path.Combine(env.WebRootPath, container, fileName);

                if (File.Exists(fileDirectory))
                {
                    File.Delete(fileDirectory);
                }
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditFile(string container, IFormFile image, string route)
        {
            await DeleteFile(route, container);
            return await SaveFile(container, image);
        }

        public Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFile(string container, IFormFile image)
        {

            var extension = Path.GetExtension(image.FileName);
            var fileName = $"{(image.FileName)}";
            string folder = Path.Combine(env.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string route = Path.Combine(folder, fileName);

            using (var memoryStream = new MemoryStream())
            {

                await image.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                await File.WriteAllBytesAsync(route, content);
            }

            var urlCurrent = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlForBD = Path.Combine(urlCurrent, container, fileName).Replace("\\", "/");
            return urlForBD;
        }
    }
}
