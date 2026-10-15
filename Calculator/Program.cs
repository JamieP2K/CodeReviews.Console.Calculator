using System.Text.RegularExpressions;
using CalculatorLibrary;

bool endApp = false;
Console.WriteLine("Console Calcultor in C#\r");
Console.WriteLine("-----------------------\n");

Calculator calculator = new();
while (!endApp)
{
    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\t1: Add");
    Console.WriteLine("\t2: Subtract");
    Console.WriteLine("\t3: Multiply");
    Console.WriteLine("\t4: Divide");
    Console.WriteLine("\t-----------");
    Console.WriteLine("\t5: Square Root");
    Console.WriteLine("\t6: Power");
    Console.WriteLine("\t7: 10^n");
    Console.WriteLine("\t8: Trigonometry");
    Console.WriteLine("\n");
    Console.WriteLine("Q: Quit Calculator");
    string? op;
    while (true)
    {
        Console.Write("Your option? ");
        op = Console.ReadLine();

        // Break only when the input is valid
        if (op != null && Regex.IsMatch(op, "^[1-8Qq]$"))
        {
            break;
        }
        Console.WriteLine("Error: Unrecognized input.\n");
    }


    //Close app if input = q
    if (op == "Q" || op == "q")
    {
        Console.WriteLine("Closing App");
        endApp = true;
        break;
    }

    // Declare variables and set to empty.
    // Use Nullable types (with ?) to match type of System.Console.ReadLine
    string? numInput1 = "";
    string? numInput2 = "";
    double result = 0;
    double cleanNum1 = 0;
    double cleanNum2 = 0;

    if (op == "5" || op == "7" || op == "8")
    {
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();

        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value");
            numInput1 = Console.ReadLine();
        }
    }
    else
    {
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();

        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value");
            numInput1 = Console.ReadLine();
        }

        Console.Write("Type another number. and then press Enter: ");
        numInput2 = Console.ReadLine();

        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput2 = Console.ReadLine();
        }
    }

    if (op == "8")
    {
        op = ChooseTrigFunction();   // op is now "sin", "cos" or "tan"
    }

    //Perform operation
    try
    {
        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
        if (double.IsNaN(result))
        {
            Console.WriteLine("This operation will result in a mathematical error.\n");
        }
        else Console.WriteLine("Your result: {0:0.##}\n", result);
    }
    catch (Exception e)
    {
        Console.WriteLine("Oh no! An exception occured trying to do the math.\n - Details: " + e.Message);
    }



    Console.WriteLine("------------------------\n");
    Console.Write("Press any key to continue: ");
    Console.WriteLine("\n");
    Console.ReadKey();
    Console.Clear();
}
calculator.Finish();

static string? ChooseTrigFunction()
{
    while (true)
    {
        Console.WriteLine("\t1: Sin");
        Console.WriteLine("\t2: Cos");
        Console.WriteLine("\t3: Tan");
        Console.Write("Your option? ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1": return "sin";
            case "2": return "cos";
            case "3": return "tan";
        }
        Console.WriteLine("Error: Unrecognized input.\n");
    }
}