// See https://aka.ms/new-console-template for more information

using CakeCompany.Provider;

// dependency injection emulation
var orderProvider = new OrderProvider();
var cakeProvider = new CakeProvider();

var shipmentProvider = new ShipmentProvider(orderProvider, cakeProvider);

Console.WriteLine("Shipment Details..." + Environment.NewLine);
var products = shipmentProvider.GetShipment();
if (products.Any())
{
    foreach (var product in products)
    {
        Console.WriteLine($"Product: {product.Id}");
        Console.WriteLine($"Product type: {product.CakeType}");
        Console.WriteLine($"Quantity: {product.Quantity}" + Environment.NewLine);
    }
}
else
{
    Console.WriteLine("No orders to process");
}

