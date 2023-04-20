using Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Application.Features.Catalog.Queries.GetMenu
{
    public class MenuViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        //[JsonIgnore]
        public string Sort { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<MenuViewModel> Categories { get ; set; }
    }
    //public class MenuVm
    //{
    //    public IEnumerable<MenuViewModel> Categories { get; set; }
    //}
}
