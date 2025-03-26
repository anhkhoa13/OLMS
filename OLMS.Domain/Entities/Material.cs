using OLMS.Domain.Primitives;


namespace OLMS.Domain.Entities;

public class Material : Entity
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public MaterialType MaterialType { get; set; }
    public long FileSize { get; set; }
    public byte[] Data { get; set; }
    public DateTime UploadDate { get; }
    public Guid UserId { get; set; }
    public UserBase User { get; private set; } = default!;
    
    public Material(Guid id, string fileName, string contentType, long fileSize, byte[] data, Guid userId) : base(id)
    {
        FileName = fileName;
        ContentType = contentType;
        FileSize = fileSize;
        Data = data;
        UploadDate = DateTime.Now;
        UserId = userId;
    }

}