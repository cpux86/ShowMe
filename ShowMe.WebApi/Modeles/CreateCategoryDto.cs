using Application.Features.Catalog.Commands.AddCategory;
using Application.Mappings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Modeles
{
    
    public class CreateCategoryDto
    {
        [Required]
        [ModelBinder(Name = "title")]
        [StringLength(32, ErrorMessage = "Недопустимая длинна заголовка")]
        public string Name { get; set; } = string.Empty;

        [ModelBinder(Name = "parent_id")]
        public long Parent { get; set; }
    }
}
