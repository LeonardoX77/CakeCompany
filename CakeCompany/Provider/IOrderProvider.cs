using CakeCompany.Models;

namespace CakeCompany.Provider;

public interface IOrderProvider
{
    public Order[] GetLatestOrders();

}


