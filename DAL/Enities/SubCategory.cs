namespace DAL.Enities
{
    public class SubCategory(string name, string description, string image)
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public string Image { get; private set; } = image;
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Product>? Products { get; set; }
    }
}
