using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace the_wall.Models
{
    public class Message
    {
        [Key]
        [Column("id")]
        public int Messageid { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Message must be more than 5 characters long!")]
        [Column("content")]
        public string Content { get; set; }
        
        [Column("created_at")]
        public DateTime Created_At { get; set; } = DateTime.Now;
        [Column("updated_at")]
        public DateTime Updated_At { get; set; } = DateTime.Now;
        [Required]
        public int Userid {get;set;}
        public User Creator {get;set;}
        public List<Comment> MessagesComments {get;set;}
    }
}
