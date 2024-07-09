using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UpdateDto
    {
        public required string Name {get; set;} 
        public required int ExistingId {get; set;}
    }
}