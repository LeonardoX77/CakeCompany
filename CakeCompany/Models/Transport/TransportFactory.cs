namespace CakeCompany.Models.Transport;

internal class TransportFactory
{
    public static TTransport Create<TTransport>() where TTransport : ITransport, new()
    {
        return new TTransport();
    }
}