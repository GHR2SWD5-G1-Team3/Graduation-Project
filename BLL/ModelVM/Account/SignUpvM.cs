namespace BLL.ModelVM.Account
{
    public class SignUpvM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public IFormFile Image { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
