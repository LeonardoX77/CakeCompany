using CakeCompany.Models.Transport;

namespace CakeCompany.Provider;

public interface ITransportProvider
{
    public static ITransport GetTransport(double quantityAmount) => throw new NotImplementedException();
}
