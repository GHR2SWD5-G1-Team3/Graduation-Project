namespace BLL.ModelVM.User
{
    public class AddNewUserVM
    {
        public required string  FirstName { get; set; }
        public required string LastName { get; set; }
        public required IFormFile UploadImage { get; set; }
        public string? Image { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string RoleName { get; set; }
        public required string City { get; set; }
        public required string Government { get; set; }
    }
}
