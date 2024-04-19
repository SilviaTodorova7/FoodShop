﻿using System.ComponentModel.DataAnnotations;

namespace FoodShop.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

    }
}
