namespace CakeCompany.Models.Transport;

public class Van : ITransport
{
    public bool Deliver(List<Product> products)
    {
        return true;
    }
}