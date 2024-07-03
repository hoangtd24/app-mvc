using System.ComponentModel.DataAnnotations.Schema;

namespace aspnetcoremvc.Models.Blog
{
    [Table("PostCategory")]
    public class PostCategory
    {
        public int PostID { set; get; }

        public int CategoryID { set; get; }

        [ForeignKey("PostID")]
        public Post Post { set; get; } = null!;

        [ForeignKey("CategoryID")]
        public Category Category { set; get; } = null!;
    }
}