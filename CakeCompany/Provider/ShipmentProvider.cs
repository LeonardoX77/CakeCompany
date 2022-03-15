using System.Diagnostics;
using CakeCompany.Models;
using CakeCompany.Models.Transport;

namespace CakeCompany.Provider;

public class ShipmentProvider
{
    private IOrderProvider orderProvider;
    private ICakeProvider cakeProvider;

    public ShipmentProvider(IOrderProvider orderProvider, ICakeProvider cakeProvider)
    {
        this.orderProvider = orderProvider;
        this.cakeProvider = cakeProvider;
    }

    public List<Product> GetShipment()
    {
        //Call order to get new orders
        List<Product> result = new();
        var orders = orderProvider.GetLatestOrders();

        if (!orders.Any())
        {
            return result;
        }

        foreach (var order in orders)
        {
            var estimatedBakeTime = cakeProvider.GetEstimatedBakeTime(order);

            if (estimatedBakeTime <= order.EstimatedDeliveryTime)
            {
                if (PaymentProvider.Process(order).IsSuccessful)
                {
                    var product = cakeProvider.Bake(order);
                    result.Add(product);
                }
            }
        }
            
        var transport = TransportProvider.GetTransport(result.Sum(p => p.Quantity));
        if (transport.Deliver(result))
        {
            return result;
        }
        else
        {
            return new();
        }
    }
}
