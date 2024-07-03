using System.ComponentModel.DataAnnotations;
using aspnetcoremvc.Models.Blog;

namespace aspnetcoremvc.Areas.Blog.Model{
    public class CreatePostModel : Post{
        [Required]
        [Display(Name ="Chuyên mục")]
        public int[] CategoriesId {get; set;} = [];
    }
}