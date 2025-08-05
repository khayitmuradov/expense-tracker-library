using expense_tracker_library;

namespace expense_tracker_library.core
{
    public class Expense
    {
        public decimal Amount { get; }
        public string Category { get; }
        public DateTime Date { get; }
        public string? Description { get; }

        public Expense(decimal amount, string category, DateTime date, string? description = null)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Category is required.");

            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }
    }
}
