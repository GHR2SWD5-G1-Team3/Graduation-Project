
namespace DAL.Entities
{
    public class SubCategory(string name, string description, string imagePath, int categoryId)
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public string? ImagePath { get; private set; } = imagePath;
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; private set; } = categoryId;
        public Category? Category { get; set; }
        public List<Product>? Products { get; set; }
        public void UpdateImagePath(string imagePath)
        {
            ImagePath = imagePath;
        }
        public bool Delete()
        {
            if (IsDeleted)
                return false;
            IsDeleted = !IsDeleted;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string name, string description, string imagePath, int categoryId)
        {
            Name = name;
            Description = description;
            if (imagePath is not null) 
            {
                ImagePath = imagePath;
            }
            CategoryId = categoryId;
            ModifiedOn = DateTime.Now;
            return true;
        }
    }
}
