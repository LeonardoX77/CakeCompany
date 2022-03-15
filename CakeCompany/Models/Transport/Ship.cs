namespace CakeCompany.Models.Transport;

public class Ship : ITransport
{
    public bool Deliver(List<Product> products)
    {
        return true;
    }
}