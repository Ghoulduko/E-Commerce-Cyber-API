using Cyber.Core.Entities;

namespace Cyber.Application.Interfaces;

public interface IFileService
{
    Task AddImage(MediaFile req);
}