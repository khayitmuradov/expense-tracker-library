using expense_tracker_library.core;

namespace expense_tracker_library.tests
{
    public class ExpenseManagerTests
    {
        [Theory]
        [InlineData(30, "Lunch", "2023-10-01T00:00:00Z", "Picnic With Friend")]
        [InlineData(1, "Bus", "2023-11-01T00:00:00Z", "Ticket")]
        [InlineData(5, "Gym", "2023-10-01T00:10:12Z", "Thai Boxing")]
        [InlineData(35, "Dinner", "2023-10-01T00:00:00Z", "Dinner with Family")]
        public void AddExpense_ShouldWork(int amount, string category, string dateString, string description)
        {
            //arrange
            DateTime parsedDate = DateTime.Parse(dateString);
            Expense newExpense = new Expense
            {
                Amount = amount,
                Category = category,
                Date = parsedDate,
                Description = description,
            };
            ExpenseManager expenseManager = new ExpenseManager();

            //act
            expenseManager.AddExpense(newExpense);
            var allExpenses = expenseManager.GetAllExpenses();

            //assert
            Assert.True(allExpenses.Count == 1);
            Assert.Contains<Expense>(newExpense, allExpenses);
        }
    }
}