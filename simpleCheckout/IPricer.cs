
namespace simpleCheckout
{
    public interface IPricer
    {
        int GetPrice(char itemCode, int quantity);
    }
}
