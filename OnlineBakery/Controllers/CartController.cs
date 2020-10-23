using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineBakery.Models;
using OnlineBakery.ViewModels;

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
    
    public CartController(OnlineBakeryContext db)
    {
      _db = db;
    }
    // Index *****************
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userOrders = _db.Carts.Where(entry => entry.User.Id == currentUser.Id).OrderBy(entry => entry.OrderId).ToList();
      return View(userOrders);
    }

    // New Cart ****************
    public async Task<ActionResult> NewCart()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      List<Cart> userCart = _db.Carts.Where(y => y.User.Id == currentUser.Id && y.OrderId == 0).ToList();
      if (userCart.Any())
      {
        ViewBag.Cart = userCart;
      }
      List<FlavorTreat> model = _db.FlavorTreat.OrderBy(x => x.FlavorId).ToList();
      return View(model);
    }
    // AddToCart
    [HttpPost]
    public async Task<ActionResult> AddToCart(Cart cart)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      cart.OrderId = 0;
      cart.User = currentUser;
      _db.Carts.Add(cart);
      _db.SaveChanges();
      return RedirectToAction("AddMore");
    }
    // DeleteItem
    [HttpPost]
    public ActionResult DeleteItem(int id)
    {
      var thisCart = _db.Carts.FirstOrDefault(cart => cart.LineId == id);
      _db.Carts.Remove(thisCart);
      _db.SaveChanges();
      return RedirectToAction("NewCart");
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
      return RedirectToAction("Index");
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