namespace AngularAuthAPI.Models
{
    public class User
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username {  get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordKey { get; set; }

    }
}
