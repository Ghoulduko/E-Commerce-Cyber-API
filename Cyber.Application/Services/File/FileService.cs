using Cyber.Core.Entities;
using Cyber.Core.Helper;
using Cyber.Application.Interfaces;
using Cyber.Core.Interfaces;

namespace Cyber.Application.Services;

public class FileService : IFileService
{
    private readonly IGenericService<MediaFile> _service;

    public FileService(IGenericService<MediaFile> service)
    {
        _service = service;
    }

    public async Task AddImage(MediaFile req)
    {
        await _service.Add(req);
    }
}
