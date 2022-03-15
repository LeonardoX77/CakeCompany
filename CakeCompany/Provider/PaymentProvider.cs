using CakeCompany.Models;

namespace CakeCompany.Provider;
public class PaymentProvider: IPaymentProvider
{
    public static PaymentIn Process(Order order)
    {
        return new PaymentIn
        {
            HasCreditLimit = !order.ClientName.Contains("Important"),
            IsSuccessful = true
        };
    }
}

