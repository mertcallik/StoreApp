using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Model;
using StoreAppWeb.Models;

namespace StoreAppWeb.Controllers
{
    public class OrderController : Controller
    {
        private Cart cart;
        private IOrderRepository _orderRepository;

        public OrderController(Cart cartService,IOrderRepository orderRepository)
        {
            cart = cartService;
            _orderRepository= orderRepository;
        }

        public IActionResult CheckOut()
        {
            
            return View(new OrderViewModel(){Cart = cart});
        }
        [HttpPost]
        public async Task<IActionResult>  CheckOut(OrderViewModel model)
        {
            if (cart.Items.Count==0)
            {
                ModelState.AddModelError("","Sepetiniz boş");
            }

            if (!ModelState.IsValid)
            {
                model.Cart = cart;
                return View(model);
            }

            var order = new Order()
            {
                Id = model.Id,
                Name = model.Name,
                AdressLine = model.AdressLine,
                City = model.City,
                Email = model.Email,
                Phone = model.Phone,
                OrderItems = cart.Items.Select(item=>new StoreApp.Data.Model.OrderItem()
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price =item.Product.Price,
                    
                    
                }).ToList()
            };
            model.Cart= cart;
            var paymet = ProccesPayment(model);
            if (paymet.Status!="success")
            {
                ModelState.AddModelError("","Ödeme işlemi başarısız");
                model.Cart = cart;
                return View(model);
            }
            await _orderRepository.SaveOrder(order);
           cart.Clear();
            return RedirectToPage("/Complated",new {orderId=order.Id});
        }

        private Payment ProccesPayment(OrderViewModel model)
        {
            Options options = new Options();
            options.ApiKey = "";
            options.SecretKey = "";
            options.BaseUrl = "";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111, 999999999).ToString();
            request.Price = model?.Cart?.CalculateTotal().ToString();
            request.PaidPrice = model?.Cart?.CalculateTotal().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model?.CardHolderName;
            paymentCard.CardNumber = model?.CardNumber;
            paymentCard.ExpireMonth = model?.ExpireMonth;
            paymentCard.ExpireYear = model?.ExpireYear;
            paymentCard.Cvc = model?.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = Guid.NewGuid().ToString().Substring(1, 5);
            buyer.Name = model?.Name;
            buyer.Surname = model?.SurnName;
            buyer.GsmNumber = model?.Phone;
            buyer.Email = model.Email;
            buyer.IdentityNumber = new Random().Next(111111111, 999999999).ToString();
            buyer.RegistrationAddress = model.AdressLine;
            buyer.City = model.City;
            buyer.Country = "Turkey";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = model?.Name+" "+model.SurnName;
            shippingAddress.City = model.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = model.AdressLine;
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = model?.Name + " " + model.SurnName;
            billingAddress.City = model.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = model.AdressLine;
            billingAddress.ZipCode = "38000";
            request.BillingAddress = billingAddress;



            List<BasketItem> basketItems = new List<BasketItem>();
            foreach (var item in model.Cart.Items??new List<CartItem>())
            {
                BasketItem firstBasketItem = new BasketItem();
                firstBasketItem.Id = item.Product.Id.ToString();
                firstBasketItem.Name = item.Product.Name.ToString();
                firstBasketItem.Category1 = item.Product.Categories
                    .Where(c => c.Products.Any(p => p.Id == item.Product.Id))
                    .Select(c => c.Name)
                    .FirstOrDefault() ?? "No matching category found";

                firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                firstBasketItem.Price = item.Product.Price.ToString();
                basketItems.Add(firstBasketItem);
            }
            request.BasketItems = basketItems;



            Payment payment = Payment.Create(request, options);
            return payment;

        }
    }
}
