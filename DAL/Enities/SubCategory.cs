using static System.Net.Mime.MediaTypeNames;

namespace DAL.Enities
{
    public class SubCategory(string name, string description, string imagePath, int categoryId)
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public string ImagePath { get; private set; } = imagePath;
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; private set; } = categoryId;
        public Category? Category { get; set; }
        public List<Product>? Products { get; set; }

        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user,string name, string description, string imagePath, int categoryId)
        {
            if (user == null) return false;
            Name = name;
            Description = description;
            imagePath = imagePath;
            CategoryId = categoryId;
            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }
    }
}
