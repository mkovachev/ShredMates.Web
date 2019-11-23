namespace ShredMates.Tests.Web
{
    public class OrderControllerTest
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;

        public OrderControllerTest()
        {
            TestStartup.GetMapper();
            this.db = TestStartup.GetDataBase();
            this.shoppingCart = TestStartup.GetShoppingCart();
        }

        [Fact]
        public async Task Checkout_ShouldCreate_Order()
        {
            // Arrange
            var shoppingCartServices = new Mock<IShoppingCartService>().Object;
            var orderService = new Mock<IOrderService>().Object;
            var orderController = new Mock<OrderController>(shoppingCartServices, shoppingCart, orderService).Object;

            var product = new Product { Id = 1, Title = "A", Price = 100 };

            var orderDetail = new OrderDetail
            {
                Amount = 1,
                ProductId = product.Id,
                OrderId = 1,
                Price = product.Price
            };

            await this.db.OrderDetails.AddAsync(orderDetail);
            await this.db.SaveChangesAsync();

            var order = new Mock<Order>().Object;

            var result = await this.db.Orders.AddAsync(order);

            // Act
            await orderService.CreateOrderAsync(order);

            // Assert
            // how to check void method TODO
        }
    }
}
