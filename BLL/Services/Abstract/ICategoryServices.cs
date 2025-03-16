namespace BLL.Services.Abstract
{
    public interface ICategoryServices
    {
        (bool, string?) Create(string? name, string? description, string? image);
    }
}
