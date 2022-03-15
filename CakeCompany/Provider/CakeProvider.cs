using CakeCompany.Models;

namespace CakeCompany.Provider;
public class CakeProvider: ICakeProvider
{
    public DateTime GetEstimatedBakeTime(Order order)
    {
        switch (order.CakeType)
        {
            case CakeEnum.Chocolate:
            case CakeEnum.RedVelvet:
                return DateTime.Now.Add(TimeSpan.FromMinutes((int)order.CakeType));
            case CakeEnum.Vanilla:
                break;
        }

        return DateTime.Now.Add(TimeSpan.FromHours(15));
    }

    public Product Bake(Order order)
    {
        return new()
        {
            CakeType = order.CakeType,
            Id = Guid.NewGuid(),
            Quantity = order.Quantity
        };
    }
}
