using CakeCompany.Models;
using CakeCompany.Models.Transport;
using CakeCompany.Provider;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompany.UnitTest
{
    internal class UnitTest
    {
        [TestCase]
        public void GivenOrderProviderWhenGetLatestOrdersThenShouldReturnAtLeastOneOrder()
        {
            // Arrange
            var orderProvider = new OrderProvider();

            // Act
            var orders = orderProvider.GetLatestOrders();

            // Assert
            Assert.That(orders, Is.Not.Empty);
        }

        [TestCase]
        public void GivenCakeProviderWhenEstimatingBakeTimeForAllTypesOfCakesThenShouldCalculateBakeTimeProperly()
        {
            // Arrange
            var orders = new List<Order>();
            // create orders of all cake types
            foreach (var cakeType in Enum.GetValues(typeof(CakeEnum)).Cast<CakeEnum>())
            {
                orders.Add(new Order("CakeBox", DateTime.Now, 1, cakeType, 120.25));
            }

            //Act
            foreach (var order in orders)
            {
                var cakeProvider = new CakeProvider();

                var estimatedBakeTime = cakeProvider.GetEstimatedBakeTime(order);

                DateTime bakeTime = DateTime.Now.Add(TimeSpan.FromHours(15));
                switch (order.CakeType)
                {
                    case CakeEnum.Chocolate:
                    case CakeEnum.RedVelvet:
                        bakeTime = DateTime.Now.Add(TimeSpan.FromMinutes((int)order.CakeType));
                        break;
                    case CakeEnum.Vanilla:
                        break;
                    default:
                        break;
                }

                //Assert
                Assert.AreEqual(bakeTime.ToString("yyyyMMddhhmmss"), estimatedBakeTime.ToString("yyyyMMddhhmmss"));
            }
        }

        [TestCase(true,  TestName = "GivenOrderWhenProcessedThenShouldReturnPaymentWithCreditLimit")]
        [TestCase(false, TestName = "GivenOrderWhenProcessedThenShouldReturnPaymentWithNoCreditLimit")]
        public void GivenOrderWhenProcessedThenShouldReturnPayment(bool hasCreditLimit)
        {
            // Arrange
            var orderProvider = new OrderProvider();
            Order[]? orders = orderProvider.GetLatestOrders();
            PaymentIn paymentInExpected = new PaymentIn() { HasCreditLimit = hasCreditLimit, IsSuccessful = true};

            if (!orders.Any())
            {
                Assert.Fail("There are no orders to test!");
            }

            // Act
            var order = hasCreditLimit ? orders.First() : orders.Last();
            PaymentIn paymentIn = PaymentProvider.Process(order);

            // Assert
            AssertEx.PropertyValuesAreEquals(paymentIn, paymentInExpected);
            Assert.IsTrue(paymentIn.HasCreditLimit == hasCreditLimit);

        }

        [TestCase]
        public void GivenOrderWhenIsBakedThenProductShouldBeReturned()
        {
            // Arrange
            var orderProvider = new OrderProvider();

            var orders = orderProvider.GetLatestOrders();

            if (!orders.Any())
            {
                Assert.Fail("There are no orders to test!");
            }

            // Act
            foreach (var order in orders)
            {
                var cakeProvider = new CakeProvider();
                Product product = cakeProvider.Bake(order);

                // Assert
                Assert.IsNotNull(product);
                Assert.AreEqual(product.CakeType, order.CakeType);
                Assert.AreEqual(product.Quantity, order.Quantity);
            }
        }

        [TestCase(1,    TestName = "Given1ProductQuantityWhenGetTransportThenShouldBeShippedByVan")]
        [TestCase(1000, TestName = "Given1000ProductQuantityWhenGetTransportThenShouldBeShippedByVan")]
        [TestCase(1001, TestName = "Given1001ProductQuantityWhenGetTransportThenShouldBeShippedByTruck")]
        [TestCase(4999, TestName = "Given4999ProductQuantityWhenGetTransportThenShouldBeShippedByTruck")]
        [TestCase(5000, TestName = "Given5000ProductQuantityWhenGetTransportThenShouldBeShippedByShip")]
        public void GivenProductQuantityWhenGetTransportThenShouldBeShippedBy(int productCount)
        {
            // Arrange
            Order order = new("CakeBox", DateTime.Now.AddHours(2), 1, CakeEnum.RedVelvet, productCount);
            var products = new List<Product>();
            var cakeProvider = new CakeProvider();

            // Act
            var estimatedBakeTime = cakeProvider.GetEstimatedBakeTime(order);

            if (estimatedBakeTime <= order.EstimatedDeliveryTime)
            {
                var payment = new PaymentProvider();

                if (PaymentProvider.Process(order).IsSuccessful)
                {
                    var product = cakeProvider.Bake(order);
                    products.Add(product);
                }
            }

            var transportProvider = new TransportProvider();
            var transport = TransportProvider.GetTransport(products.Sum(p => p.Quantity));

            // Assert
            Assert.IsTrue(transport.Deliver(products));

            if (productCount <= 1000)
                Assert.IsTrue(transport.GetType() == typeof(Van));

            else if (productCount > 1000 && productCount < 5000)
                Assert.IsTrue(transport.GetType() == typeof(Truck));

            else
                Assert.IsTrue(transport.GetType() == typeof(Ship));
        }

        [TestCase(false, TestName = "GivenShipmentProviderWhenGetShipmentThenProductsAreNotReturned")]
        [TestCase(true, TestName = "GivenShipmentProviderWhenGetShipmentThenProductsAreReturned")]
        public void GivenShipmentProviderWhenGetShipmentThenProductsReturned(bool returnProducts)
        {
            var mockOrderProvider = new Mock<IOrderProvider>();

            List<Order> orders = new();
            if (returnProducts)
            {
                orders.Add(new("CakeBox", DateTime.Now.AddHours(2), 1, CakeEnum.RedVelvet, 120.25));
                orders.Add(new("ImportantCakeShop", DateTime.Now.AddHours(2), 1, CakeEnum.RedVelvet, 120.25));
            }

            mockOrderProvider.Setup(library => library.GetLatestOrders())
                             .Returns(orders.ToArray());

            var shipmentProvider = new ShipmentProvider(mockOrderProvider.Object, new CakeProvider());
            var products = shipmentProvider.GetShipment();
            if (returnProducts)
                Assert.IsTrue(products.Any());
            else
                Assert.IsFalse(products.Any());
        }

    }
} 
