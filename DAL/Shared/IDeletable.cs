namespace DAL.Shared
{
    public interface IDeletable
    {
        bool Delete(string deletedBy);
    }
}
