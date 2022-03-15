using CakeCompany.Models;

namespace CakeCompany.Provider;

public interface ICakeProvider
{
    public DateTime GetEstimatedBakeTime(Order order);

    public Product Bake(Order order);
}
