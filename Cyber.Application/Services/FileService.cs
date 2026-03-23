using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Services;

public class FileService
{
    private readonly GenericService<MediaFile> _service;

    public FileService(GenericService<MediaFile> service)
    {
        _service = service;
    }

    public async Task AddImage(MediaFile req)
    {
        await _service.Add(req);
    }
}
