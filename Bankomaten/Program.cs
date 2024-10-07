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
            string[,] accounts = {  { "Main account: 2000", "Savings account: 2000", "Broakrege account: 2000" },
                                    { "Main account: 2000", "Savings account: 2000", "Broakrege account: 2000" },
                                    { "Main account: 2000", "Savings account: 2000", "Broakrege account: 2000" },
                                    { "Main account: 2000", "Savings account: 2000", "Broakrege account: 2000" },
                                    { "Main account: 2000", "Savings account: 2000", "Broakrege account: 2000" } };

            int logInMaxAttempts = 3;
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
                    Console.WriteLine("1. View your accounts and balance\n2. Transfer between accounts\n3. Withdraw money\n4. Log out");
                    int userChoiceInMenu = Convert.ToInt32(Console.ReadLine());

                    // View Accounts and balance
                    if (userChoiceInMenu == 1)
                    {
                        // Saving the index of the accounts array for the logged in user
                        string mainaccount = accounts[userIndex, 0];
                        string savingsaccount = accounts[userIndex, 1];
                        string broakregeaccount = accounts[userIndex, 2];

                        // NEED TO CHANGE THE EXIT FUNCTION, WHEN USER PRESS "ENTER" KEY IT SHOULD GO BACK TO THE MENU
                        Console.WriteLine($"{mainaccount}\n{savingsaccount}\n{broakregeaccount}\n1. Go back to menu\n2. Exit");
                        int userChoiseToExitOrStay = Convert.ToInt32(Console.ReadLine());

                        if (userChoiseToExitOrStay == 1)
                        {
                            logInSuccessful = true;
                        }
                        else
                        {
                            System.Environment.Exit(1);
                        }

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
    }
}
