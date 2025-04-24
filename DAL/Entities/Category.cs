
namespace DAL.Entities
{
    public class Category
    {
        public Category() { }
        public Category(string? name, string? description, string? image, string userId)
        {
            Name = name;
            Description = description;
            Image = image;
            UserId = userId;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public List<SubCategory>? SubCategories { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; private set; }

        public bool Delete(string? user)
        {
            if (user == null) return false;
            if (IsDeleted)
                return false;
            IsDeleted = !IsDeleted;
            DeletedBy = user;
            DeletedOn = DateTime.Now;
            return true;
        }

        public bool Edit(string user, string name, string description, string image)
        {
            if (user == null) return false;
            Name = name;
            Description = description;
            if (!string.IsNullOrEmpty(image))
            {
                Image = image;
            }
            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }
    }

}
