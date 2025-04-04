using DAL.Shared;

namespace DAL.Enities
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
        public void UpdateImagePath(string imagePath)
        {
            Image = imagePath;
        }

        public bool Delete(string? User)
        {
            if (User == null) return false;
            if (IsDeleted)
                return false;
            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user, string name, string description, string image)
        {
            if (user == null) return false;
            Name = name;
            Description = description;
            if (image is not null) // Only update if a new image is uploaded
            {
                Image = image;
            }
            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }
    }
}
