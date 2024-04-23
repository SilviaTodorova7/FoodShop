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
            this.Cart = new Cart();
        }

        public ICollection<Comment> Comments { get; set; }


        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
