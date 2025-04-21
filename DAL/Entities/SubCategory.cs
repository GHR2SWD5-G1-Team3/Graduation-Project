
namespace DAL.Entities
{
    public class SubCategory
    {
        public SubCategory() { }
        public SubCategory(string name, string description, string imagePath, int categoryId,string userId)
        {
            Name = name;
            Description = description;
            ImagePath = imagePath;
            CategoryId = categoryId;
            UserId = userId;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; } 
        public string? ImagePath { get; private set; }
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; private set; } 
        public Category? Category { get; set; }
        public List<Product>? Products { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; private set; }
        public void UpdateImagePath(string imagePath)
        {
            ImagePath = imagePath;
        }
        public bool Delete(string? user)
        {
            if (user == null) return false;
            if (IsDeleted)
                return false;
            DeletedBy = user;
            IsDeleted = !IsDeleted;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user,string name, string description, string imagePath, int categoryId)
        {
            if (user == null) return false;
            Name = name;
            Description = description;
            if (imagePath is not null) 
            {
                ImagePath = imagePath;
            }
            CategoryId = categoryId;
            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }
    }
}
