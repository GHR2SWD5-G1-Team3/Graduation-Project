
namespace DAL.Entities
{
    public class User(string firstName, string lastName, string image, string phone, string address)  : IdentityUser
    {
        public string FirstName { get; private set; } = firstName;
        public string LastName { get; private set; } =lastName;
        public string? Image { get; private set; } = image;
        public string Phone { get; private set; } = phone;
        public string Address { get; private set; } = address;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public string RoleName { get; set; }
        public DateTime? LastUpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public List<Review>? Reviews { get; set; }
        public List<Product>? Products { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public List<Coupon>? Coupons { get; set; }
        public Cart? Cart { get; set; }
        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? editBy, string fName, string lName,string? imagepath, string address, string phone)
        {
            FirstName = fName;
            LastName = lName;
            Image = imagepath;
            Address = address;
            Phone = phone;
            ModifiedBy = editBy;
            ModifiedOn = DateTime.Now;
            return true;
        }
    }
}
