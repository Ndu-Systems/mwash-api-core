using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreedomCode.Api.Data
{
    public class AppVariableType
    {
        [Key]
        public int AppVariableTypeId { get; set; }
        public string Name { get; set; }
        public int? VariableTypeId { get; set; }
    }
}
