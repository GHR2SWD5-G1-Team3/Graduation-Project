namespace BLL.ModelVM.User
{
    public class EditUser
    {
       public string Id { get; set; }
       public string EditBy { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Address { get; set; }
       public string Phone { get; set; }
       public IFormFile? Image { get; set; }
    }
}
