using Cyber.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Entities;

[Table("MediaFiles")]
public class MediaFile
{
    [Key]
    public int Id { get; set; }

    public Guid? Identificator { get; set; }

    [Required]
    public required string FileName { get; set; }

    [Required]
    public required string FileType { get; set; }

    public required byte[] Content { get; set; }

    [Required]
    public required ContentType ContentType { get; set; }
}
