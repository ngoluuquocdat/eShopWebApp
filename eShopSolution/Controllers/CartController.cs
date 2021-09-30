using eShopSolution.ApiIntegration;
using eShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public CartController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddToCart(int productId, string languageId)
        {
            var product = await _productApiClient.GetById(productId, languageId);
            
            // lấy cart có trong session ra và deserialized nó ra list cartItemVm
            // currentCart chính là list các CartItems
            var session = HttpContext.Session.GetString("CartSession");
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session!=null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);

            int quantity = 1;

            // nếu đã tồn tại product này sẵn trong cart
            if(currentCart.Any(x => x.ProductId == productId))
            {
                // thì tăng số lượng cho product đó trong cart
                // ở đây thực tế thao tác với CartItemVm
                quantity = currentCart.FirstOrDefault(x => x.ProductId == productId).Quantity + 1;
            }    

            
            var cartItem = new CartItemViewModel()
            {
                ProductId = productId,
                Name = product.Name,
                Description = product.Description,
                Quantity = quantity,
                ThumbnailImage = product.ThumbnailImage,
                Price = product.Price
            };

            // thêm cartItem vào list
            currentCart.Add(cartItem);

            // chuyển currentCart thành dạng json và đẩy vào session
            HttpContext.Session.SetString("CartSession", JsonConvert.SerializeObject(currentCart));

            // trả về các cart items hiện có
            return Ok(currentCart);
        }

        [HttpGet]
        public IActionResult GetListCartItem()
        {

            // lấy cart có trong session ra và deserialized nó ra list cartItemVm
            // currentCart chính là list các CartItems
            var session = HttpContext.Session.GetString("CartSession");
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);

            return Ok(currentCart);
        }

        public IActionResult UpdateCart(int productId, int quantity)
        {
            // lấy cart có trong session ra và deserialized nó ra list cartItemVm
            // currentCart chính là list các CartItems
            var session = HttpContext.Session.GetString("CartSession");
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);

            foreach(var item in currentCart)
            {
                if (item.ProductId == productId)
                {
                    if (quantity == 0)      // tức là sản phẩm bị xóa
                    {
                        currentCart.Remove(item);
                        break;
                    }    
                    else
                        item.Quantity = quantity;
                }
                    
            }    

            // chuyển currentCart thành dạng json và đẩy vào session
            HttpContext.Session.SetString("CartSession", JsonConvert.SerializeObject(currentCart));

            // trả về các cart items hiện có
            return Ok(currentCart);
        }
    }
}
