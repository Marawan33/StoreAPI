using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.ProductSpecs
{
    public class ProductWithSpecification : BaseSpecification<Product>
    {
        public ProductWithSpecification(ProductSpecification specs) : base(prod => (!specs.BrandId.HasValue || prod.BrandId == specs.BrandId.Value)
        && (!specs.TypeId.HasValue || prod.TypeId == specs.BrandId.Value))
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            AddOrderBy(x => x.Name);

            if(!string.IsNullOrEmpty(specs.Sort))
            {
                switch(specs.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(x=>x.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(x=>x.Price);
                        break;
                    default:
                        AddOrderBy(x=>x.Name); break;
                        
                }
            }
        }
        public ProductWithSpecification(int? id) : base(prod => prod.Id == id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
        }
    }
}
