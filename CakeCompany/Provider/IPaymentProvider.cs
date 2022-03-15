using CakeCompany.Models;

namespace CakeCompany.Provider;

public interface IPaymentProvider
{
    public static PaymentIn Process(Order order) => throw new NotImplementedException();
}

