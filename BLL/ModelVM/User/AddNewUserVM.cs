namespace BLL.ModelVM.User
{
    public class AddNewUserVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile UploadImage { get; set; }
        public string? Image { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
        public string City { get; set; }
        public string Government { get; set; }
    }
}
