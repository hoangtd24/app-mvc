using System.ComponentModel.DataAnnotations.Schema;

namespace aspnetcoremvc.Models.Blog
{
    [Table("PostCategory")]
    public class PostCategory
    {
        public int PostID { set; get; }

        public int CategoryID { set; get; }

        [ForeignKey("PostID")]
        public required Post Post { set; get; }

        [ForeignKey("CategoryID")]
        public required Category Category { set; get; }
    }
}