using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Server.Service.Core.Dto;
using Server.Service.Domain;
using Server.Service.Domain.Dto;
using Server.Service.Services.Dto;

namespace Server.Service.Services;

/// <summary>
/// 文件接口
/// </summary>
public interface IFileService
{
    Task<PageOutput<FileGetPageOutput>> GetPageAsync(PageInput<FileGetPageDto> input);

    Task DeleteAsync(FileDeleteInput input);

    Task<FileEntity> UploadFileAsync(IFormFile file, string fileDirectory = "", bool fileReName = true);

    Task<List<FileEntity>> UploadFilesAsync([Required] IFormFileCollection files, string fileDirectory = "", bool fileReName = true);
}