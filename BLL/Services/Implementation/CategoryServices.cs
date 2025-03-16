using BLL.Services.Abstract;
using DAL.Enities;
using DAL.Repo.Abstract;
using DAL.Repo.Implementation;

namespace BLL.Services.Implementation
{
    public class CategoryServices : ICategoryServices
    {
        public (bool, string?) Create(string? name, string? description, string? image)
        {
            throw new NotImplementedException();
        }
    }
}
