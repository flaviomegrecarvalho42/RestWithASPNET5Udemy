using RestWithAspNet5Udemy.BLL.Interfaces;
using System.IO;

namespace RestWithAspNet5Udemy.BLL
{
    public class FileBLL : IFileBLL
    {
        public byte[] GetPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fulPath = path + "\\Other\\aspnet-life-cycles-events.pdf";
            return File.ReadAllBytes(fulPath);
        }
    }
}
