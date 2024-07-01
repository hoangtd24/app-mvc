using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace aspnetcoremvc.Areas.Identity.Models.RoleViewModels
{
  public class EditRoleModel
  {
    [Required(ErrorMessage = "Phải nhập {0}")]
    public string Id { get; set; } = string.Empty;

    [Display(Name = "Tên của role")]
    [Required(ErrorMessage = "Phải nhập {0}")]
    [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} ký tự")]
    public string Name { get; set; } = string.Empty;
  }
}