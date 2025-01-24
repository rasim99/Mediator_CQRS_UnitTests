﻿using Business.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Product.Queries.GetProduct
{
    public class GetProductQuery :IRequest<Response<ProductDto>>
    {
        public int Id { get; set; }
    }
}
