﻿namespace expense_tracker_library.core
{
    public class Expense
    {
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }

    }
}
