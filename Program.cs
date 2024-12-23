using System;

class FamilyBudgetManager
{
    static void Main()
    {
        // Define the constant for minimum salary
        const int MINIMALURI_XELFASI = 1000;
        
        // Array of month names
        string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        
        // 2D array to store monthly budget details: [12 months, 4 categories] 
        int[,] budget = new int[12, 4];  // [12 months, 4 columns: salary, utilities, food, entertainment]
        
        int totalIncome = 0;
        int totalExpenses = 0;
        int highestExpenses = 0;
        string highestExpensesMonth = "";
        int monthsAboveMinimalSalary = 0;

        // Collect input data for each month
        for (int i = 0; i < 12; i++)
        {
            Console.WriteLine($"Enter the budget details for {months[i]}:");
            
            // Input for salary
            int salary;
            Console.Write("Enter salary (min 1000): ");
            while (!int.TryParse(Console.ReadLine(), out salary) || salary < MINIMALURI_XELFASI)
            {
                Console.WriteLine("Invalid input. Please enter a valid salary greater than or equal to 1000.");
                Console.Write("Enter salary (min 1000): ");
            }
            budget[i, 0] = salary;
            totalIncome += salary;

            // Input for utilities expenses
            int utilities;
            Console.Write("Enter utilities expenses: ");
            while (!int.TryParse(Console.ReadLine(), out utilities) || utilities < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number for utilities.");
                Console.Write("Enter utilities expenses: ");
            }
            budget[i, 1] = utilities;

            // Input for food expenses
            int food;
            Console.Write("Enter food expenses: ");
            while (!int.TryParse(Console.ReadLine(), out food) || food < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number for food.");
                Console.Write("Enter food expenses: ");
            }
            budget[i, 2] = food;

            // Input for entertainment expenses
            int entertainment;
            Console.Write("Enter entertainment expenses: ");
            while (!int.TryParse(Console.ReadLine(), out entertainment) || entertainment < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number for entertainment.");
                Console.Write("Enter entertainment expenses: ");
            }
            budget[i, 3] = entertainment;

            // Calculate total expenses for the month
            int totalMonthExpenses = budget[i, 1] + budget[i, 2] + budget[i, 3];
            totalExpenses += totalMonthExpenses;

            // Track the month with the highest expenses
            if (totalMonthExpenses > highestExpenses)
            {
                highestExpenses = totalMonthExpenses;
                highestExpensesMonth = months[i];
            }

            // Count the number of months with salary above the minimum threshold
            if (salary > MINIMALURI_XELFASI)
            {
                monthsAboveMinimalSalary++;
            }

            Console.WriteLine(); // Add a blank line for readability
        }

        // Output the annual budget summary
        Console.WriteLine("\nFamily Budget Summary for the Year:");
        Console.WriteLine($"Total Annual Income: {totalIncome}");
        Console.WriteLine($"Average Monthly Expenses: {totalExpenses / 12}");
        Console.WriteLine($"Month with Highest Expenses: {highestExpensesMonth}");
        Console.WriteLine($"Number of Months with Salary Above {MINIMALURI_XELFASI}: {monthsAboveMinimalSalary}");

        // Request user input for a specific month's detailed report
        Console.WriteLine("\nEnter a month number (1-12) to view the detailed report:");
        int monthNumber = int.Parse(Console.ReadLine()) - 1;
        PrintMonthReport(budget, months, monthNumber);

        // Ask the user for a target amount to save
        Console.WriteLine("\nEnter your target amount to save:");
        int targetAmount = int.Parse(Console.ReadLine());
        CalculateMonthsToSave(targetAmount, totalIncome, budget);

        // Output the annual expenses graph using stars
        PrintExpenseGraph(budget, months);
    }

    // Function to print the detailed report for a specific month
    static void PrintMonthReport(int[,] budget, string[] months, int monthNumber)
    {
        Console.WriteLine($"\nDetailed report for {months[monthNumber]}:");
        Console.WriteLine($"Salary: {budget[monthNumber, 0]}");
        Console.WriteLine($"Utilities Expenses: {budget[monthNumber, 1]}");
        Console.WriteLine($"Food Expenses: {budget[monthNumber, 2]}");
        Console.WriteLine($"Entertainment Expenses: {budget[monthNumber, 3]}");
    }

    // Function to calculate how many months it will take to save a target amount
    static void CalculateMonthsToSave(int targetAmount, int totalIncome, int[,] budget)
    {
        int totalSavings = 0;
        int monthsNeeded = 0;

        // Iterate over each month to calculate savings
        for (int i = 0; i < 12; i++)
        {
            int monthSavings = budget[i, 0] - (budget[i, 1] + budget[i, 2] + budget[i, 3]);
            totalSavings += monthSavings;
            if (totalSavings >= targetAmount)
            {
                monthsNeeded = i + 1;
                break;
            }
        }

        // Output the number of months required to save the target amount
        if (totalSavings >= targetAmount)
        {
            Console.WriteLine($"You will need {monthsNeeded} months to save {targetAmount}.");
        }
        else
        {
            Console.WriteLine($"You will need more than a year to save {targetAmount} with the current savings plan.");
        }
    }

    // Function to print the annual expenses graph with stars
    static void PrintExpenseGraph(int[,] budget, string[] months)
    {
        Console.WriteLine("\nAnnual Expenses Graph (using stars):");
        for (int i = 0; i < 12; i++)
        {
            int totalMonthExpenses = budget[i, 1] + budget[i, 2] + budget[i, 3];
            int stars = totalMonthExpenses / 100; // 1 star per 100 units of expenses
            Console.Write($"{months[i]}: ");
            for (int j = 0; j < stars; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }
}
