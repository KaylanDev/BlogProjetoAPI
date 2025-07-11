﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Blog_Domain.Models;

public class User 
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public IEnumerable<Post>?Posts { get; set; }
    public IEnumerable<Coment>? Coments { get; set; }

}
