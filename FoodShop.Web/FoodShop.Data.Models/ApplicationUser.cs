using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodShop.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Comments = new HashSet<Comment>();
            this.UserProducts = new HashSet<UserProduct>();
        }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserProduct> UserProducts { get; set; }

    }
}
