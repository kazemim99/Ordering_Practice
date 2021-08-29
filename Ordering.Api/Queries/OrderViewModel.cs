using System;
using System.Collections.Generic;

namespace Ordering.Api.Queries
{
    public record Orderitem
    {
        public string productname { get; init; }
        public int units { get; init; }
        public double unitprice { get; init; }
    }

    public record Order
    {
        public int ordernumber { get; init; }
        public DateTime date { get; init; }
        public string status { get; init; }
        public List<Orderitem> orderitems { get; set; }
        public decimal total { get; set; }
    }
}