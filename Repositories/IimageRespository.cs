using NGWALKSAPI.Models.Domain;

namespace NGWALKSAPI.API.Repositories
{
    public interface IimageRespository
    {
        Task<Image> Upload(Image image);  
    }
}
