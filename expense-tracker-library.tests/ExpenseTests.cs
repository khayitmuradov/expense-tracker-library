using expense_tracker_library.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expense_tracker_library.tests
{
    public class ExpenseTests
    {
        [Theory]
        [InlineData(-10)]
        [InlineData(-1)]
        public void Expense_ShouldThrow_IfAmountNegative(decimal amount)
        {
            //arrange
            Expense expense = new Expense()
            {
                Amount = amount,
            };
            var expenseManager = new ExpenseManager();

            //act + assert
            Assert.Throws<ArgumentOutOfRangeException>(() => expenseManager.AddExpense(expense));
        }

        [Fact]
        public void Expense_ShouldThrow_IfCategoryIsNull()
        {
            // arrange
            var expense = new Expense
            {
                Amount = 10m,
                Category = null!,
                Date = DateTime.UtcNow,
                Description = "Test expense with null category"
            };
            var expenseManager = new ExpenseManager();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => expenseManager.AddExpense(expense));
        }

        [Fact]
        public void Expense_ShouldCreateObject_WithValidData()
        {
            decimal amount = 12.5m;
            string category = "Lunch";
            DateTime date = new DateTime(2025, 08, 06, 13, 30, 00, DateTimeKind.Utc);
            string description = "Burger and Fries";

            Expense expense = new Expense
            {
                Amount = amount,
                Category = category,
                Date = date,
                Description = description
            };

            Assert.Equal(amount, expense.Amount);
            Assert.Equal(category, expense.Category);
            Assert.Equal(date, expense.Date);
            Assert.Equal(description, expense.Description);
        }
    }
}
