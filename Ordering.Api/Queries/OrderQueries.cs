using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Api.Queries
{
    public class OrderQueries
        : IOrderQueries
    {
        private readonly string _connectionString;

        public OrderQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var result = await connection.QueryAsync<dynamic>(
                @"select *
                        FROM ordering.Orders o
                        WHERE o.Id=@id"
                , new { id }
            );

            if (result.AsList().Count == 0)
                throw new KeyNotFoundException();

            return MapOrderItems(result);
        }

        private Order MapOrderItems(dynamic result)
        {
            var order = new Order
            {
                ordernumber = result[0].ordernumber,
                date = result[0].date,
                status = result[0].status,
                orderitems = new List<Orderitem>(),
                total = 0
            };

            foreach (dynamic item in result)
            {
                var orderitem = new Orderitem
                {
                    productname = item.productname,
                    units = item.units,
                    unitprice = (double)item.unitprice,
                };

                order.total += item.units * item.unitprice;
                order.orderitems.Add(orderitem);
            }

            return order;
        }
    }
}