using System.ComponentModel.DataAnnotations;

namespace TestApp.App.Accounts.Requests
{
    public class EditAccountRequest
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
