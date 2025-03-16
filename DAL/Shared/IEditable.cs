namespace DAL.Shared
{
    public interface IEditable
    {
        bool Edit(string? user, Dictionary<string, object> updatedProperties);
    }
}
