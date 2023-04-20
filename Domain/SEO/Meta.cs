using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SEO
{
    public class Meta
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 65)]
        public string Title { get; set; }
        [StringLength(maximumLength:250)]
        public string Keywords { get; set; }
        [StringLength(maximumLength: 200)]
        public string Description { get; set; }
    }
}
