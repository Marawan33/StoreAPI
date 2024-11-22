using Store.Repository.Basket.Models;
using Store.Service.Services.BasketServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketServices
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string basketid);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto customerBasket);
        Task<bool> DeleteBasketAsync(string basketid);
    }
}
