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
        public void AddExpense_ShouldWork(int amount, string category, DateTime dateString, string description)
        {
            //arrange
            Expense newExpense = new Expense
            {
                Amount = amount,
                Category = category,
                Date = dateString,
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

        [Fact]
        public void AddExpense_ShouldThrow_WhenExpenseIsNull()
        {
            //arrange
            ExpenseManager expenseManager = new ExpenseManager();


            //act n assert
            Assert.Throws<ArgumentNullException>(() => expenseManager.AddExpense(null!));
        }

        [Fact]
        public void GetTotalExpenses_ShouldReturnCorrectSum()
        {
            //arrange
            ExpenseManager expenseManager = new ExpenseManager();
            Expense expense1 = new Expense()
            {
                Amount = 10m,
                Category = "Lunch",
                Date = DateTime.UtcNow,
                Description = "Burger"
            };
            Expense expense2 = new Expense()
            {
                Amount = 25.5m,
                Category = "Transport",
                Date = DateTime.UtcNow,
                Description = "Bus Ticket"
            };
            Expense expense3 = new Expense()
            {
                Amount = 5.25m,
                Category = "Coffee",
                Date = DateTime.UtcNow,
                Description = "Morning Coffee"
            };

            //act
            expenseManager.AddExpense(expense1);
            expenseManager.AddExpense(expense2);
            expenseManager.AddExpense(expense3);
            var actualSumOfExpenses = expenseManager.GetTotalExpenses();

            //assert
            Assert.Equal(40.75m, actualSumOfExpenses);
        }

        [Fact]
        public void GetTotalByCategory_ShouldReturnCorrectSum()
        {
            //arrange
            Expense expense1 = new Expense()
            {
                Amount = 23m,
                Category = "Food",
                Date = DateTime.UtcNow,
                Description = "",
            };
            Expense expense2 = new Expense()
            {
                Amount = 12.46m,
                Category = "Food",
                Date = DateTime.UtcNow,
                Description = "Late Kebab"
            };
            Expense expense3 = new Expense()
            {
                Amount = 56.62m,
                Category = "Stationery",
                Date = DateTime.UtcNow,
                Description = "Book And Notebook purchase"
            };
            Expense expense4 = new Expense()
            {
                Amount = 1.5m,
                Category = "Transport",
                Date = DateTime.UtcNow,
                Description = "",
            };
            ExpenseManager expenseManager = new ExpenseManager();

            //act
            expenseManager.AddExpense(expense1);
            expenseManager.AddExpense(expense2);
            expenseManager.AddExpense(expense3);
            expenseManager.AddExpense(expense4);
            var actualTotalByCategory = expenseManager.GetTotalByCategory(expense1.Category);

            //assert
            Assert.Equal(35.46m, actualTotalByCategory);
        }

        [Theory]
        [InlineData("Food")]
        [InlineData("Entertainment")]
        public void GetTotalByCategory_ShouldBeZero_IfCategoryNotFound(string category)
        {
            //arrange
            Expense newExpense = new Expense()
            {
                Amount = 45m,
                Category = category,
            };
            ExpenseManager expenseManager = new ExpenseManager();

            //act
            expenseManager.AddExpense(newExpense);
            var actualResult = expenseManager.GetTotalByCategory("Bills");

            //assert
            Assert.Equal(0m, actualResult);
        }

        [Fact]
        public void GetGetExpensesByDateRange_ShouldReturnCorrectExpense()
        {
            //arrange
            var expense1 = new Expense
            {
                Amount = 10,
                Category = "Food",
                Date = new DateTime(2025, 08, 01),
                Description = "Burger"
            };

            var expense2 = new Expense
            {
                Amount = 20,
                Category = "Transport",
                Date = new DateTime(2025, 08, 03),
                Description = "Bus Ticket"
            };

            var expense3 = new Expense
            {
                Amount = 30,
                Category = "Entertainment",
                Date = new DateTime(2025, 08, 05),
                Description = "Movie"
            };
            ExpenseManager expenseManager = new ExpenseManager();


            DateTime startDate = new DateTime(2025, 08, 01);
            DateTime endDate = new DateTime(2025, 08, 02);

            //act
            expenseManager.AddExpense(expense1);
            expenseManager.AddExpense(expense2);
            expenseManager.AddExpense(expense3);
            var actualResult = expenseManager.GetExpensesByDateRange(startDate, endDate);

            //assert
            Assert.Single(actualResult);
            Assert.Contains(expense1, actualResult);
        }

        [Fact]
        public void GetExpensesByDateRange_ShouldReturnEmpty_IfNoMatches()
        {
            var expense = new Expense
            {
                Amount = 30,
                Category = "Entertainment",
                Date = new DateTime(2025, 08, 05),
                Description = "Movie"
            };
            ExpenseManager expenseManager = new ExpenseManager();
            DateTime startDate = new DateTime(2025, 08, 10);
            DateTime endDate = new DateTime(2025, 08, 15);

            //act
            expenseManager.AddExpense(expense);
            var actualResult = expenseManager.GetExpensesByDateRange(startDate, endDate);

            //assert
            Assert.Empty(actualResult);
        }

        [Fact]
        public void GetAllExpenses_ShouldReturnAllAddedExpenses()
        {
            Expense expense1 = new Expense()
            {
                Amount = 10m,
                Category = "Food",
                Date = new DateTime(2025, 05, 10),
                Description = ""
            };
            Expense expense2 = new Expense
            {
                Amount = 30,
                Category = "Entertainment",
                Date = new DateTime(2025, 08, 05),
                Description = "Movie"
            };
            Expense expense3 = new Expense
            {
                Amount = 20,
                Category = "Transport",
                Date = new DateTime(2025, 08, 03),
                Description = "Bus Ticket"
            };
            ExpenseManager expenseManager = new ExpenseManager();
            
            //act
            expenseManager.AddExpense(expense1);
            expenseManager.AddExpense(expense2);
            expenseManager.AddExpense(expense3);

            var actualResult = expenseManager.GetAllExpenses();

            //assert
            Assert.Equal(3, actualResult.Count);
            Assert.Contains(expense1, actualResult);
            Assert.Contains(expense3, actualResult);
        }
    }
}