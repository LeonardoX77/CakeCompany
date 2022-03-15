using System.Reflection.Metadata.Ecma335;
using CakeCompany.Models;

namespace CakeCompany.Provider;
public class OrderProvider: IOrderProvider
{
    public Order[] GetLatestOrders()
    {
        return new Order[]
        {
            new("CakeBox", DateTime.Now.AddHours(2), 1, CakeEnum.RedVelvet, 120.25),
            new("ImportantCakeShop", DateTime.Now.AddHours(2), 1, CakeEnum.RedVelvet, 120.25)
        };
    }
}


