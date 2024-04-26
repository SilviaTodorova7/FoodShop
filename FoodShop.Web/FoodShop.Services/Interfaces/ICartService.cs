﻿using FoodShop.Web.ViewModels.Product;

namespace FoodShop.Services.Interfaces
{
    public interface ICartService
    {
        Task<ICollection<CartProductViewModel>> GoToCart(string userId);

        Task BuyProductsAsync(string userId);
    }
}
