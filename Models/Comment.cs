using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace the_wall.Models
{
    public class Comment
    {
        [Key]
        [Column("id")]
        public int Commentid { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Comment must be more than 2 characters long!")]
        [Column("comment")]
        public string Comment_Message { get; set; }
        
        [Column("created_at")]
        public DateTime Created_At { get; set; } = DateTime.Now;
        [Column("updated_at")]
        public DateTime Updated_At { get; set; } = DateTime.Now;
        [Required]
        public int Userid {get;set;}
        public User Commentor {get;set;}
        public int Messageid {get;set;}
        public Message MessageCommented {get;set;}
    }
}
