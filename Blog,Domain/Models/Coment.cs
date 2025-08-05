using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Domain.Models;

public class Coment
{
    public int ComentId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? PostId { get; set; }
    public int UserId { get; set; }
    // Navigation properties
    public virtual Post Post { get; set; }
    public virtual User User { get; set; }

    
}
