using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.ProductSpecs
{
    public class ProductWithCountSpecifications : BaseSpecification<Product>
    {
        public ProductWithCountSpecifications(ProductSpecification specs) :
            base(prod=>(!specs.BrandId.HasValue||prod.BrandId==specs.BrandId.Value)
            &&(!specs.TypeId.HasValue||prod.TypeId==specs.TypeId.Value)
           && (string.IsNullOrEmpty(specs.Search) || prod.Name.Trim().ToLower().Contains(specs.Search)
        )
            )
        {
        }
    }
}
