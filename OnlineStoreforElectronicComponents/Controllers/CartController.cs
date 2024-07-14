using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreforElectronicComponents.Models;
using System.Security.Claims;

namespace OnlineStoreforElectronicComponents.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly IServiceProvider _serviceProvider; // Добавьте объявление 
       
        public CartController(ICartRepository cartRepo, IServiceProvider serviceProvider) // Добавьте сервис провайдер в конструктор
        {
            _cartRepo = cartRepo;
            _serviceProvider = serviceProvider;
        }
        public async Task<IActionResult> AddItem(int componentId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(componentId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int componentId)
        {
            var cartCount = await _cartRepo.RemoveItem(componentId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }

        public  async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        public  IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            bool isCheckedOut = await _cartRepo.DoCheckout(model);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));
            
            var rabbitMqMessage = new RabbitMqMessage
            {
                to = new List<string> { model.Email }, 
                bcc = new List<string> { "vasinlesha1234@yandex.ru" },
                cc = new List<string> { "vasinlesha1234@yandex.ru" },
                from = "vasinlesha1234@yandex.ru", 
                displayName = "Alex",
                replyTo= "Alex",
                replyToName= "Alex",
                subject = "Новый заказ",
                body = $"Добрый день, {model.Name}. Ваш заказ от {DateTime.Now:d} принят в работу."
            };
            var rabbitMqService = _serviceProvider.GetRequiredService<RabbitMqService>();
            rabbitMqService.SendCheckoutMessage(rabbitMqMessage);
            return RedirectToAction(nameof(OrderSuccess));
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }

    }
}
