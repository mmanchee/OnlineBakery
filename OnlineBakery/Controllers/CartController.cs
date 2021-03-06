using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBakery.Models;

namespace OnlineBakery.Controllers
{
  [Authorize]
  public class CartController : Controller
  {
    private readonly OnlineBakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public CartController(UserManager<ApplicationUser> userManager, OnlineBakeryContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    
    // Index *****************
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var model = _db.Carts.Where(entry => entry.User.Id == currentUser.Id).OrderBy(entry => entry.OrderId).ToList();
      return View(model);
    }
    // New Cart ****************
    public async Task<ActionResult> Create()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      List<Cart> userCart = _db.Carts.Where(y => y.User.Id == currentUser.Id && y.OrderId == 0).ToList();
      if (userCart.Any())
      {
        ViewBag.Cart = userCart;
      }
      ViewBag.ClientId = currentUser.Id;
      List<FlavorTreat> model = _db.FlavorTreat.OrderBy(x => x.FlavorId).ToList();
      return View(model);
    }
    // AddToCart
    public async Task<ActionResult> AddToCart(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      _db.Carts.Add(new Cart {FlavorTreatId = id, OrderId = 0, Quantity = 0, User = currentUser});
      _db.SaveChanges();
      return RedirectToAction("Create");
    }
    // DeleteItem
    public ActionResult DeleteItem(int id)
    {
      var thisCart = _db.Carts.FirstOrDefault(cart => cart.CartId == id);
      _db.Carts.Remove(thisCart);
      _db.SaveChanges();
      return RedirectToAction("Create");
    }
    
    public ActionResult ChangeItem(int id, int qty)
    {
      if(qty >= 0)
      {
        var cart = new Cart { CartId = id, Quantity = qty };
        _db.Carts.Attach(cart);
        _db.Entry(cart).Property(x => x.Quantity).IsModified = true;
        _db.SaveChanges();
      }
      return RedirectToAction("Create");
    }
    // BuyCart
    public async Task<ActionResult> BuyCart()
    {
      int order = _db.Carts.Select(x => x.OrderId).DefaultIfEmpty(0).Max();
      order++;
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      List<Cart> userCart = _db.Carts.Where(y => y.User.Id == currentUser.Id && y.OrderId == 0).ToList();
      foreach(Cart item in userCart)
      {
        item.OrderId = order;
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = order});
    }
    // Details **************
    public async Task<ActionResult> Details(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      List<Cart> model = _db.Carts.Where(y => y.User.Id == currentUser.Id && y.OrderId == id).ToList();
      return View(model);
    }
  }
}