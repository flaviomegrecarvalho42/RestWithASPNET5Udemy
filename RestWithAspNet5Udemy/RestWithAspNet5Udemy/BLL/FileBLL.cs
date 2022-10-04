using Microsoft.AspNetCore.Http;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestWithAspNet5Udemy.BLL
{
    public class FileBLL : IFileBLL
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBLL(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadFiles\\";
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;

            if (!File.Exists(filePath))
            {
                throw new Exception("Arquivo não encontrado");
            }

            return File.ReadAllBytes(filePath);
        }

        public async Task<FileResponseDto> GetFileStreamResult(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new Exception("Nome do arquivo está vazio");

            FileResponseDto fileResponseDto = await GenerateDownloadFile(fileName);

            return fileResponseDto;
        }

        public async Task<FileDetailDto> SaveFile(IFormFile file)
        {
            FileDetailDto fileDetail = new FileDetailDto();
            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() != ".pdf" && fileType.ToLower() != ".jpg" &&
                fileType.ToLower() != ".png" && fileType.ToLower() != ".jpeg")
                throw new Exception("Extensão de arquivo não permitida");

            var documentName = Path.GetFileName(file.FileName);

            if (file == null || file.Length <= 0)
                throw new Exception("Arquivo vazio");

            fileDetail = await DownloadFile(file, fileDetail, baseUrl, fileType, documentName);

            return fileDetail;
        }

        public async Task<List<FileDetailDto>> SaveFiles(IList<IFormFile> files)
        {
            List<FileDetailDto> filesDetail = new List<FileDetailDto>();

            foreach (var file in files)
            {
                filesDetail.Add(await SaveFile(file));
            }

            return filesDetail;
        }

        private async Task<FileResponseDto> GenerateDownloadFile(string fileName)
        {
            var path = Path.Combine(_basePath, fileName);

            if (!File.Exists(path))
                throw new Exception("Arquivo não encontrado");

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
                stream.Close();

                await stream.DisposeAsync();

                memory.Position = 0;
            }

            FileResponseDto fileResponseDto = new FileResponseDto
            {
                Memory = memory,
                ExtensionType = "application/octetstream",
                FileName = fileName
            };

            return fileResponseDto;
        }

        private async Task<FileDetailDto> DownloadFile(IFormFile file, FileDetailDto fileDetail, HostString baseUrl, string fileType, string documentName)
        {
            var destination = Path.Combine(_basePath, "", documentName);
            fileDetail.DocumentName = documentName;
            fileDetail.DocumentType = fileType;
            fileDetail.DocumentUrl = Path.Combine(baseUrl + "/api/File/v1" + fileDetail.DocumentName);

            var stream = new FileStream(destination, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();
            stream.Dispose();

            return fileDetail;
        }
    }
}
