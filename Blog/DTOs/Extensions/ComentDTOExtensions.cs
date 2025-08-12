using Blog.Application.DTOs.ComentDTOModel;
using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.Extensions
{
  public static class ComentDTOExtensions 
    {
        public static IEnumerable<ComentsDTO> ComentsForDTOLIst(this IEnumerable<Coment> Coments)
        {
            return Coments.Select(coments => (ComentsDTO)coments);
        }
    }
}
