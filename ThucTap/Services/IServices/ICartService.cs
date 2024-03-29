﻿using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Cart;
using ThucTap.Payloads.Responses;

namespace ThucTap.Services.IServices
{
    public interface ICartService
    {
        ResponseObject<CartItemDTO> AddToCart(AddToCartRequest request);
        ResponseObject<CartItemDTO> HandleQuantity(HandleQuantityRequest request);
        ResponseObject<Cart> DeleteCart(int accountID);
        ResponseObject<CartItemDTO> DeleteCartItem(int cartItemID);

        List<CartItemDTO> GetAll(int accountID);
    }
}
