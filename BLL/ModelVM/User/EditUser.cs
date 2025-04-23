namespace BLL.ModelVM.User
{
    public class EditUser
    {
       public string Id { get; set; }
       public string? EditBy { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Phone { get; set; }
       public  string City { get; set; }
        public string Government { get;  set; }
        public IFormFile? UploadImage { get; set; }
    }
}
