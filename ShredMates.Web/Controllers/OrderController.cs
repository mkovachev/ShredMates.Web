using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
using ShredMates.Web.Infrastructure.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ShredMates.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IShoppingCartService shoppingCartServices;
        private readonly ShoppingCart shoppingCart;
        private readonly IOrderService orderService;
        private readonly IEmailService emailService;
        private IHostingEnvironment env;

        public OrderController(IShoppingCartService shoppingCartServices, ShoppingCart shoppingCart, IOrderService orderService, IEmailService emailService, IHostingEnvironment env)
        {
            this.shoppingCartServices = shoppingCartServices;
            this.shoppingCart = shoppingCart;
            this.orderService = orderService;
            this.emailService = emailService;
            this.env = env;
        }

        public async Task<IActionResult> Checkout(Order order)
        {
            var items = this.shoppingCartServices.AllProducts();
            this.shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("Empty cart", "Your cart is empty, go grab some products");
                TempData.AddErrorMessage("Your cart is empty, go back and add some products");
            }

            if (!ModelState.IsValid)
            {
                // TempData.AddErrorMessage("Please fill in all fields");
                return View(order);
            }

            await this.orderService.CreateOrderAsync(order);
            shoppingCartServices.ClearCart();

            // send email with order details to customer

            var webRoot = env.WebRootPath; //get wwwroot folder

            var pathToFile = env.WebRootPath // get file
                     + Path.DirectorySeparatorChar.ToString()
                     + "templates"
                     + Path.DirectorySeparatorChar.ToString()
                     + "Email"
                     + Path.DirectorySeparatorChar.ToString()
                     + "orderConfirmation.html";

            // read template
            var builder = new BodyBuilder();
            using (StreamReader reader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = reader.ReadToEnd();
            }

            var emailMessage = new EmailMessage
            {
                ToAddresses =
                {
                    new EmailAddress { Name = order.FirstName, Address = order.Email }
                },
                FromAddresses =
                {
                    new EmailAddress { Name = "test", Address = "test@muimail.com" }
                },
                Subject = $"ShredMated Shop Order {order.OrderId}",
                Content = builder.ToString()
            };

            this.emailService.SendEmail(emailMessage);

            TempData.AddSuccessMessage("Thank you for your order! Check your email for the order details");
            return RedirectToAction("Index", "Home");
        }
    }
}