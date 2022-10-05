using System.IO;

namespace RestWithAspNet5Udemy.Data.DTO
{
    public class FileResponseDto
    {
        public MemoryStream Memory { get; set; }

        public string ExtensionType { get; set; }

        public string FileName { get; set; }
    }
}
