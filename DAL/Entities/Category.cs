
namespace DAL.Entities
{
    public class Category(string? name, string? description, string? image) 
    {
        public int Id { get; set; }
        public string? Name { get; set; } = name;
        public string? Description { get; set; } = description;
        public string? Image { get; set; } = image;
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        public List<SubCategory>? SubCategories { get; set; }
        

        public bool Delete()
        {
            if (IsDeleted)
                return false;
            IsDeleted = !IsDeleted;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string name, string description, string image)
        {
            Name = name;
            Description = description;
            if (image is not null) 
            {
                Image = image;
            }
            ModifiedOn = DateTime.Now;
            return true;
        }
    }
}
