namespace DAL.Entities
{
    public class Role : IdentityRole
    {
        public Role() { }
        public Role(string name):base(name) { }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
    }
}
