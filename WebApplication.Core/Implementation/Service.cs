using System;
using System.IO;

namespace WebApplication.Core.Implementation
{
    public class Service
        : IService
    {
        private readonly ServiceSettings _settings;

        public Service(ServiceSettings settings)
        {
            _settings = settings;
        }

        public void Execute(Input input)
        {
            var fullPath = GetFileName(input);
            using (var writer = File.Create(fullPath))
            {
                input.Content.CopyTo(writer);
            }
        }

        private string GetFileName(Input input)
        {
            var baseDirectory = _settings.BaseDirectory;
            var fullFileName = Path.Combine(baseDirectory, input.FileName);
            if (Path.GetFullPath(fullFileName).StartsWith(baseDirectory))
            {
                if (File.Exists(fullFileName) == false) return fullFileName;

                var directory = Path.GetDirectoryName(fullFileName);
                var fileName = Path.GetFileNameWithoutExtension(fullFileName);
                var extension = Path.GetExtension(fullFileName);
                var next = Directory.GetFiles(directory, $"{fileName}*{extension}").Length + 1;
                return Path.Combine(directory, $"{fileName} ({next}){extension}");
            }


            var message = $"Invalid file path. the file must be written within the base directory. Full path: {fullFileName}. Base directory: {baseDirectory}.";
            throw new InvalidOperationException(message);
        }
    }
}