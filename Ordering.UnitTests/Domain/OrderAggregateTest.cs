using Ordering.Domain.AggregatesModel.Order;
using Ordering.Domain.Events;
using Ordering.Domain.Exceptions;
using Xunit;

namespace Ordering.UnitTests.Domain
{
    public class OrderAggregateTest
    {
        public OrderAggregateTest()
        { }

        [Fact]
        public void Create_order_item_success()
        {
            //Arrange
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var units = 5;

            //Act
            var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, units);

            //Assert
            Assert.NotNull(fakeOrderItem);
        }

        [Fact]
        public void Invalid_number_of_units()
        {
            //Arrange
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var units = -1;

            //Act - Assert
            Assert.Throws<OrderingDomainException>(() => new OrderItem(productId, productName, unitPrice, discount, units));
        }

        [Fact]
        public void Invalid_total_of_order_item_lower_than_discount_applied()
        {
            //Arrange
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var units = 1;

            //Act - Assert
            Assert.Throws<OrderingDomainException>(() => new OrderItem(productId, productName, unitPrice, discount, units));
        }

        [Fact]
        public void Invalid_discount_setting()
        {
            //Arrange
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var units = 5;

            //Act
            var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, units);

            //Assert
            Assert.Throws<OrderingDomainException>(() => fakeOrderItem.SetNewDiscount(-1));
        }

        [Fact]
        public void Invalid_units_setting()
        {
            //Arrange
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var units = 5;

            //Act
            var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, units);

            //Assert
            Assert.Throws<OrderingDomainException>(() => fakeOrderItem.AddUnits(-1));
        }

        [Fact]
        public void when_add_two_times_on_the_same_item_then_the_total_of_order_should_be_the_sum_of_the_two_items()
        {
            var order = new OrderBuilder()
                .AddOne(1, "cup", 10.0m, 0, string.Empty)
                .AddOne(1, "cup", 10.0m, 0, string.Empty)
                .Build();

            Assert.Equal(20.0m, order.GetTotal());
        }

        [Fact]
        public void Add_new_Order_raises_new_event()
        {
            //Arrange
            var userName = "fakeName";
            var userId = "1";
            var expectedResult = 1;

            //Act
            var fakeOrder = new Order(userId, userName);

            //Assert
            Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
        }

        [Fact]
        public void Add_event_Order_explicitly_raises_new_event()
        {
            //Arrange
            var userName = "fakeName";
            var userId = "1";
            var expectedResult = 2;

            //Act
            var fakeOrder = new Order("1", "fakeName");
            fakeOrder.AddDomainEvent(new OrderStartedDomainEvent(fakeOrder, "fakeName", "1"));
            //Assert
            Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
        }

        [Fact]
        public void Remove_event_Order_explicitly()
        {
            //Arrange

            var fakeOrder = new Order("1", "fakeName");
            var @fakeEvent = new OrderStartedDomainEvent(fakeOrder, "1", "fakeName");
            var expectedResult = 1;

            //Act
            fakeOrder.AddDomainEvent(@fakeEvent);
            fakeOrder.RemoveDomainEvent(@fakeEvent);
            //Assert
            Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
        }
    }
}