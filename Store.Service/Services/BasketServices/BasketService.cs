using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.Services.BasketServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketServices
{
    public class BasketService:IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasketAsync(string basketid)
        => await _basketRepository.DeleteBasketAsync(basketid);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketid)
        {
            var basket = await _basketRepository.GetBasketAsync(basketid);
            if(basket == null)
            {
                return new CustomerBasketDto();
            }
            var MappedBasket = _mapper.Map<CustomerBasketDto>(basket);
            return MappedBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto customerBasket)
        {
            if (customerBasket.Id is null)
            {
                customerBasket.Id = GenerateRandomId();
            }

            var custBasket = _mapper.Map<CustomerBasket>(customerBasket);

            var updatedBasket = await _basketRepository.UpdateBasketAsync(custBasket);

            var mappedUpdatedBasket = _mapper.Map<CustomerBasketDto>(updatedBasket);

            return mappedUpdatedBasket;

        }
        private string GenerateRandomId()
        {
            Random random = new Random();
            int randomDigit = random.Next(1000, 10000);
            return $"BS-{randomDigit}";
        }

    }
}
