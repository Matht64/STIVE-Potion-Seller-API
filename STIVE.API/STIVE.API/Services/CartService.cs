using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;
using STIVE.API.Models;

namespace STIVE.API.Services;

public class CartService : ICartService
{
    private readonly StiveDbContext _context;

    public CartService(StiveDbContext context)
    {
        _context = context;
    }

    public async Task<List<CartDto>> GetCartByUserAsync(string userId)
    {
        var carts = await _context.Cart
            .Where(c => c.UserId == userId)
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Bonus)
            .ToListAsync();

        if (!carts.Any())
            return new List<CartDto>();

        return carts.Select(cart => new CartDto
        {
            UserId = cart.UserId,
            GameDataId = cart.GameDataId,
            CartItems = cart.CartItems.Select(ci => new CartItemDto
            {
                BonusId = ci.Bonus.Id,
                BonusName = ci.Bonus.Name,
                BonusPrice = ci.Bonus.Price,
                Quantity = ci.Quantity
            }).ToList()
        }).ToList();
    }
    public async Task<CartDto> AddToCartAsync(string userId, int gameDataId, int bonudId, int quantity)
    {
        var bonus = await _context.Bonus.FindAsync(bonudId);
        if (bonus == null)
            return null;
        
        var gameData = await _context.GameData.FirstOrDefaultAsync(gd => gd.Id == gameDataId && gd.UserId == userId);
        if (gameData == null)
            return null;
        
        var cart = await _context.Cart.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId && c.GameDataId == gameDataId);
        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                GameDataId = gameDataId,
                CartItems = new List<CartItem>()
            };
            _context.Cart.Add(cart);
        }
        
        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BonusId == bonudId);
        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                BonusId = bonudId,
                Quantity = quantity
            });
        }
        await _context.SaveChangesAsync();

        return new CartDto
        {
            GameDataId = cart.GameDataId,
            CartItems = cart.CartItems.Select(ci => new CartItemDto
            {
                BonusId = ci.BonusId,
                Quantity = ci.Quantity
            }).ToList()
        };
    }

    public async Task<bool> RemoveFromCartAsync(string userId, int bonusId, int gameDataId)
    {
        var cart = await _context.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.GameDataId == gameDataId);

        if (cart == null)
            return false;

        var itemToRemove = cart.CartItems.FirstOrDefault(ci => ci.BonusId == bonusId);

        if (itemToRemove == null)
            return false;

        cart.CartItems.Remove(itemToRemove);
        
        if (cart.CartItems.Count == 0)
            _context.Cart.Remove(cart);
            
        await _context.SaveChangesAsync();

        return true;
    }



    public async Task<bool> ClearCartAsync(string userId, int gameDataId)
    {
        var cart = await _context.Cart.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId && c.GameDataId == gameDataId);
        if (cart == null)
            return false;
        
        _context.CartItem.RemoveRange(cart.CartItems);
        _context.Cart.Remove(cart);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ValidateCartAsync(string userId, int gameDataId)
    {
        var cart = await _context.Cart.Include(c => c.CartItems).ThenInclude(ci => ci.Bonus).FirstOrDefaultAsync(c => c.UserId == userId && c.GameDataId == gameDataId);
        if (cart == null || !cart.CartItems.Any())
            return false;

        var order = new Order
        {
            UserId = userId,
            GameDataId = gameDataId,
            CreatedAt = DateTime.UtcNow,
            OrderItems = cart.CartItems.Select(ci => new OrderItem
            {
                BonusId = ci.Bonus.Id,
                Quantity = ci.Quantity,
                PriceAtPurchase = ci.Bonus.Price
            }).ToList()
        };
        
        _context.Order.Add(order);
        
        _context.CartItem.RemoveRange(cart.CartItems);
        _context.Cart.Remove(cart);
        
        await _context.SaveChangesAsync();
        return true;
    }
}