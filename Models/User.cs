using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace the_wall.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        public int Userid { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "First name must be more than 2 characters!")]
        [Column("first_name", TypeName = "VARCHAR(255)")]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Last name must be more than 2 characters!")]
        [Column("last_name", TypeName = "VARCHAR(255)")]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }
        [Required]
        [EmailAddress]
        [Column("email", TypeName = "VARCHAR(255)")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        [Column("password", TypeName = "VARCHAR(255)")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Column("created_at")]
        public DateTime Created_At { get; set; } = DateTime.Now;
        [Column("updated_at")]
        public DateTime Updated_At { get; set; } = DateTime.Now;
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password_confirm { get; set; }
        public List<Comment> CommentsMade {get;set;}
        public List<Message> MessagesMade {get;set;}
    }
}
