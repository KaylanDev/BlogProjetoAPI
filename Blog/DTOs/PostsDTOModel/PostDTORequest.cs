using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.PostsDTOModel
{
    public class PostDTORequest
    {
       
        public string Title { get; set; }
        public string Content { get; set; }
     
        public PostDTORequest()
        {
            
        }
    }
}
