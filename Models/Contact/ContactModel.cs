using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace aspnetcoremvc.Models.Contact;

public class ContactModel
{
    [Key]
    public int Id { set; get; }

    [StringLength(50)]
    [DisplayName("Họ Tên")]
    public string Name { set; get; } = string.Empty;

    [StringLength(50)]
    [DisplayName("Email")]
    [EmailAddress]
    public string Email { set; get; } = string.Empty;

    [StringLength(50)]
    [DisplayName("Số điện thoại")]
    [Phone]
    public string Phone { set; get; } = string.Empty;

    [StringLength(500)]
    [DisplayName("Nội dung")]
    public string Content { set; get; } = string.Empty;

    public DateTime? DateSend { set; get; }

}