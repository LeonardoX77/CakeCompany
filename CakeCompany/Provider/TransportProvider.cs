using CakeCompany.Models;
using CakeCompany.Models.Transport;

namespace CakeCompany.Provider;
public class TransportProvider: ITransportProvider
{
    public static ITransport GetTransport(double quantityAmount)
    {
        if (quantityAmount <= 1000)
            return TransportFactory.Create<Van>();

        else if (quantityAmount > 1000 && quantityAmount < 5000)
            return TransportFactory.Create<Truck>();

        return TransportFactory.Create<Ship>();
    }
}
