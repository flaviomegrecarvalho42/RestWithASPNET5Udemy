using Microsoft.AspNetCore.Http;
using RestWithAspNet5Udemy.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface IFileBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        byte[] GetFile(string fileName);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<FileResponseDto> GetFileStreamResult(string fileName);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<FileDetailDto> SaveFile(IFormFile file);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<List<FileDetailDto>> SaveFiles(IList<IFormFile> files);
    }
}
