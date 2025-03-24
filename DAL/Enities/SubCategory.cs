namespace DAL.Enities
{
    public class SubCategory(string name, string description, string imagePath, int categoryId)
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public string ImagePath { get; private set; } = imagePath;
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; private set; } = categoryId;
        public Category? Category { get; set; }
        public List<Product>? Products { get; set; }
    }
}
