namespace Cyber.Application.Interfaces;

public interface ICartQuantityCalculator
{
    int UpdateQuantity(int currentQuantity, string action);
}