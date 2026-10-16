// DAY 2: NAMING CONVENTIONS
// This file is a reference example — the "practical" here is reading the
// conventions below and comparing them against the rest of this project's code.
//
//   Element                    Convention          Example
//   -------                    ----------          -------
//   Class / Interface / Method PascalCase          OrderProcessor, GetTotal()
//   Interface prefix           I + PascalCase       IOrderRepository
//   Local variable / parameter camelCase            orderId, totalPrice
//   Private field               _camelCase          _logger, _context
//   Constant                   PascalCase           MaxRetries
//   Async method               suffix 'Async'       GetByIdAsync()

public interface IDiscountCalculator        // I + PascalCase
{
    decimal CalculateDiscount(decimal totalPrice); // PascalCase method, camelCase param
}

public class DiscountCalculator : IDiscountCalculator
{
    public const decimal MaxDiscountRate = 0.5m;   // PascalCase constant
    private readonly decimal _loyaltyBonus;        // _camelCase private field

    public DiscountCalculator(decimal loyaltyBonus) // camelCase constructor parameter
    {
        _loyaltyBonus = loyaltyBonus;
    }

    public decimal CalculateDiscount(decimal totalPrice)
    {
        var rate = Math.Min(MaxDiscountRate, 0.1m + _loyaltyBonus); // camelCase local variable
        return totalPrice * rate;
    }
}

public static class NamingConventionsDemo
{
    public static void Run()
    {
        Console.WriteLine("=== DAY 2: NAMING CONVENTIONS ===");
        Console.WriteLine("See the comment block at the top of Day2_NamingConventions.cs for the rules.");

        IDiscountCalculator calculator = new DiscountCalculator(loyaltyBonus: 0.05m);
        var discount = calculator.CalculateDiscount(200m);
        Console.WriteLine($"Discount on $200: {discount:C}");

        Console.WriteLine();
    }
}
