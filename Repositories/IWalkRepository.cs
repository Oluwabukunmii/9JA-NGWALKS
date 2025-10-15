using NGWALKSAPI.Models.Domain;

namespace NGWALKSAPI.API.Repositories
{
    public  interface IWalkRepository
    {
     Task<Walk> CreateAsync(Walk Walk);

     Task<List<Walk>>GetAllAsync(string? filterOn = null, string? filerQuery = null,
         string? sortBy =null , bool IsAscendng = true,
        int pageNumber = 1 , int pageSize = 1000  );

     Task<Walk?> GetByIdAsync(Guid id);

     Task<Walk?> UpdateAsync(Guid id, Walk Walk);

      Task<Walk?> DeleteAsync(Guid id);


    }
}
