namespace BLL.ModelVM.Product
{
    public class DeletedProductsVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}
