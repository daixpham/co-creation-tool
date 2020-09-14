using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<BlogPost> Blogs{get;set;}
    }