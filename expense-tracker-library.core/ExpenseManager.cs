namespace expense_tracker_library.core
{
    public class ExpenseManager
    {
        private readonly List<Expense> _expenses = new();

        public void AddExpense(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }
            if (expense.Amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(expense.Amount), $"{expense.Amount} cannot be negative.");
            }
            if (string.IsNullOrWhiteSpace(expense.Category))
            {
                throw new ArgumentNullException(nameof(expense.Category), $"{expense.Category} cannot be null.");
            }
            if (expense.Date == null)
            {
                throw new ArgumentNullException(nameof(expense.Date), $"{expense.Date} cannot be null.");
            }

            _expenses.Add(expense);
        }

        public decimal GetTotalExpenses()
        {
            return _expenses.Sum(e => e.Amount);
        }

        public decimal GetTotalByCategory(string category)
        {
            return _expenses
                .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Sum(e => e.Amount);
        }

        public List<Expense> GetExpensesByDateRange(DateTime from, DateTime to)
        {
            return _expenses
                .Where(e => e.Date >= from && e.Date <= to)
                .ToList();
        }

        public List<Expense> GetAllExpenses()
        {
            return new List<Expense>(_expenses);
        }
    }
}
