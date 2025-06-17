using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Blog_Domain.Models;

public class User : IdentityUser<int>
{
   

    public ICollection<Post>?Posts { get; set; }
    public ICollection<Coment>? Coments { get; set; }

}
