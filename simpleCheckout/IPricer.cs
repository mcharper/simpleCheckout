
namespace simpleCheckout
{
    public interface IPricer
    {
        int GetPrice(string itemCode, int quantity);
    }
}
