using System.Security.Principal;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Welcome message
            Console.WriteLine("Welcome to the ATM");

            // Username and passwords saved as arrays
            string[] usernames = { "petter", "serhan", "pär", "egzon", "hej" };
            string[] passwords = { "petter123", "serhan123", "pär123", "egzon123", "hej123" };

            // Accounts
            string[][] accounts = {  ["Main account"],
                                     ["Main account", "Savings account"],
                                     ["Main account", "Savings account", "Broakrege account"],
                                     ["Main account", "Savings account", "Broakrege account", "House Account"],
                                     ["Main account", "Savings account", "Broakrege account", "House Account", "Wife Account"] };

            double[][] balances = {    [ 3000 ],
                                       [ 3000, 2000 ],
                                       [ 3000, 2000, 2000 ],
                                       [ 3000, 2000, 2000, 1234 ],
                                       [ 3000, 2000, 2000, 1234, 5000 ] };

            // Max attempts
            int logInMaxAttempts = 3;
            // Count for max attempts
            int LogInAttempts = 0;
            bool logInSuccessful = false;



            // Ask to login untill successfull or fail
            while (!logInSuccessful)
            {
                // Ask for username
                Console.WriteLine("Enter Username:");
                string inputUsername = Console.ReadLine();

                // Ask for password
                Console.WriteLine("Enter Password:");
                string inputPassword = Console.ReadLine();

                // Store the index of the usernames array
                var userIndex = Array.IndexOf(usernames, inputUsername);


                // Remove this later, write out the index number of the array
                Console.WriteLine(userIndex);


                // Call login check method
                logInSuccessful = LoginCheck(usernames, passwords, inputUsername, inputPassword);

                // Login successfull
                while (logInSuccessful)
                {


                    Console.WriteLine("Welcome to the ATM\n1. View your accounts and balance\n2. Transfer between accounts\n3. Withdraw money\n4. Log out");
                    int userChoiceInMenu = Convert.ToInt32(Console.ReadLine());

                    // If user choose number over the options
                    if (userChoiceInMenu > 4 || userChoiceInMenu <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Choice");
                    }

                    // View Accounts and balance
                    if (userChoiceInMenu == 1)
                    {
                        Console.Clear();
                        SeeAccounts(userIndex, accounts, logInSuccessful, balances);
                    }












                    // Exit the program from menu
                    if (userChoiceInMenu == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("Successfully logged out");
                        System.Environment.Exit(1);
                    }


                }

                if (!logInSuccessful)
                {
                    LogInAttempts++;
                    // Failed to log in
                    if (LogInAttempts < logInMaxAttempts)
                    {
                        Console.WriteLine("Login failed. Invalid username or password. Please try again.");
                        Console.WriteLine($"You have {logInMaxAttempts - LogInAttempts} attempts left");
                    }
                    // If max amount of attempts is fullfilled this exit the program
                    else
                    {
                        Console.WriteLine("Login failed. You have reached max amount of attempts. Try again later.");
                        System.Environment.Exit(1);
                    }

                }
            }


        }



        static bool LoginCheck(string[] usernames, string[] passwords, string inputUsername, string inputPassword)
        {
            // Goes thourgh the entire array indexes
            for (int i = 0; i < usernames.Length; i++)
            {
                // Goes thourgh the arrays at the same time to check if it the same in the same position of the array
                if (usernames[i] == inputUsername && passwords[i] == inputPassword)
                {
                    // Login successful
                    return true;
                }
            }
            // Login failed
            return false;
        }

        static void SeeAccounts(int userIndex, string[][] accounts, bool logInSuccessful, double[][] balances)
        {
            // Goes through the strins in accounts untill it finds the string for the index number of users,EX userIndex = 4, Account string = 4

            for (int i = 0; i < accounts[userIndex].Length; i++)
            {
                Console.WriteLine($"{accounts[userIndex][i]}: {balances[userIndex][i]}");
            }

            Console.WriteLine("1. Press any button to go back to the menu\n2. Press E to exit");

            // Describe the pressed key and save it
            ConsoleKeyInfo userChoiceMyKeyPress = Console.ReadKey();

            // If the pressed key is specified E it will run the if code
            if (userChoiceMyKeyPress.Key == ConsoleKey.E)
            {
                System.Environment.Exit(1);

            }
            // If the pressed key is not E it will run this
            else
            {
                logInSuccessful = true;
            }


        }
    }
}
