namespace CakeCompany.Models.Transport;

public class Truck : ITransport
{
    public bool Deliver(List<Product> products)
    {
        return true;
    }
}