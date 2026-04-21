using Cyber.Application.Interfaces;

namespace Cyber.Application.Services;

public class CartQuantityCalculator : ICartQuantityCalculator
{
    public int UpdateQuantity(int currentQuantity, string action)
    {
        switch (action)
        {
            case "increment":
                if (currentQuantity < 10)
                    return currentQuantity + 1;
                else
                    throw new ArgumentException("Quantity cannot be more than 10.");

            case "decrement":
                if (currentQuantity > 1)
                    return currentQuantity - 1;
                else
                    throw new ArgumentException("Quantity cannot be less than 1.");

            default:
                throw new ArgumentException("Invalid action. Use 'increment' or 'decrement'.");
        }
    }
}