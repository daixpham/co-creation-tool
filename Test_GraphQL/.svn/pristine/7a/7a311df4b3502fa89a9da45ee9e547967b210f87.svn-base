using System.ComponentModel.DataAnnotations.Schema;
using System;
using GraphQL.Types;
public class BlogPost
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Author Author { get; set; }
    public int? AuthorId {get;set;}
}