using System.ComponentModel.DataAnnotations;

namespace proj.Models
{
    public class UserSetup
    {
        [Required]
        public int User_Id { get; set; } // Assuming this is assigned by the database

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = "";

        [Required(ErrorMessage = "Login type is required")]
        public string LoginType { get; set; } = "";

        [Required(ErrorMessage = "Active status is required")]
        public bool Active { get; set; } // Changed to boolean for better data representation

        public string flag { get; set; }


    }
}
