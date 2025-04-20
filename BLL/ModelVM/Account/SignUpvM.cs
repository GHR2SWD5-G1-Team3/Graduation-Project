namespace BLL.ModelVM.Account
{
    public class SignUpvM
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public IFormFile? UploadImage { get; set; }
        public string? Image { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }

    }
}
