using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPm_Gackan1.Models;

namespace RPm_Gackan1.Controllers
{
    public class CatalogeController : Controller
    {
        private readonly FlorenceeContext db;

        public CatalogeController(FlorenceeContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Catalog()
        {
            return View(await db.Cataloges.ToListAsync());
        }

        public async Task<IActionResult> AddToCart(int catalogId, int quantity)
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return RedirectToAction("Authorization", "Profile");
            }

            var catalog = await db.Cataloges.FindAsync(catalogId);
            if (catalog == null)
            {
                return NotFound("Такого товара не существует");
            }

            if (catalog.Quantity < quantity)
            {
                return BadRequest("Больше не дам");
            }

            var cart = await db.Carts
                .FirstOrDefaultAsync(c => c.CatalogeId == catalogId && c.UsersId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    Quantity = quantity,
                    Price = catalog.ProductPrice * quantity,
                    CatalogeId = catalogId,
                    UsersId = userId
                };
                db.Carts.Add(cart);
            }
            else
            {
                cart.Quantity += quantity;
                if (cart.Quantity > catalog.Quantity)
                {
                    return BadRequest("Слишком мало товара");
                }
                cart.Price = cart.Quantity * catalog.ProductPrice;
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return RedirectToAction("Authorization", "Profile");
            }

            var cart = db.Carts
                .Where(c => c.UsersId == userId)
                .Include(c => c.Cataloge)
                .ToList();

            return View(cart);
        }

        [HttpPost]
        public IActionResult EditQuantityCart(int CatalogId, int quantity)
        {
            var cart = db.Carts.FirstOrDefault(c => c.CatalogeId == CatalogId);
            var catalog = db.Cataloges.Find(CatalogId);
            if (cart != null)
            {
                cart.Quantity = quantity;
                cart.Price = quantity * catalog.ProductPrice;
                db.SaveChanges();
            }
            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await db.Cataloges
                .Include(c => c.Reviews)
                .ThenInclude(r => r.Users)
                .FirstOrDefaultAsync(c => c.IdCataloge == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(Review review)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var cataloge = db.Cataloges
                .FirstOrDefault(c => c.IdCataloge == review.CatalogeId);

            if (cataloge == null)
            {
                return BadRequest($"Товар с ID {review.CatalogeId} не существует.");
            }

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

            review.CreatedAt = DateTime.Now;
            review.UsersId = db.Users
                .FirstOrDefault(u => u.UserLogin == User.Identity.Name)?.IdUsers ?? 0;

            db.Reviews.Add(review);
            await db.SaveChangesAsync();

            return RedirectToAction("Details", new { id = review.CatalogeId });
        }
    }
}