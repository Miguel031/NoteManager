using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Note
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Tag { get; set; }
    public bool IsArchived { get; set; }
    public List<string> Categories { get; set; } = new List<string>();
    public Note()
    {
    }
}
