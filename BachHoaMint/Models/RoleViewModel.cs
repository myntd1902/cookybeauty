using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BachHoaMint.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Please enter Role Name")]
        [Display(Name = "Role Name")]
        public string Name { get; set; }

    }
}