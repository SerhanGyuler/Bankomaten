namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Username and passwords saved as arrays
            string[] usernames = { "petter", "serhan", "pär", "egzon", "hej" };
            string[] passwords = { "petter123", "serhan123", "pär123", "egzon123", "hej123" };

            // Accounts
            string[][] accounts = {  ["Main account", "Savings account"],
                                     ["Main account", "Savings account"],
                                     ["Main account", "Savings account", "Broakrege account"],
                                     ["Main account", "Savings account", "Broakrege account", "House Account"],
                                     ["Main account", "Savings account", "Broakrege account", "House Account", "Wife Account"] };

            decimal[][] balances = {   [ 1000.12m, 432.04m ],
                                       [ 2334.54m, 6542m ],
                                       [ 10342.23m, 15432m, 54321m ],
                                       [ 6543.93m, 65464.43m, 432423m, 1234m ],
                                       [ 3000.14m, 2000.43m, 2000m, 1234m, 54221m ] };

            // Max attempts
            int logInMaxAttempts = 3;
            // Count for max attempts
            int LogInAttempts = 0;
            bool logInSuccessful = false;
            bool failedLogin = false;

            // Ask to login untill successfull or fail
            while (!logInSuccessful)
            {
                // Welcome message
                Console.WriteLine("Welcome to the ATM");
                // Ask for username
                Console.WriteLine("Enter Username:");
                string inputUsername = Console.ReadLine();

                // Ask for password
                Console.WriteLine("Enter Password:");
                string inputPassword = Console.ReadLine();

                // Store the index of the usernames array
                var userIndex = Array.IndexOf(usernames, inputUsername);

                // Call login check method
                logInSuccessful = LoginCheck(usernames, passwords, inputUsername, inputPassword);

                // Login successfull
                while (logInSuccessful)
                {

                    Console.Clear();
                    Console.WriteLine("Welcome to the ATM\n1. View your accounts and balance\n2. Transfer between accounts\n3. Withdraw money\n4. Log out");

                    // Check if user input is a string and if it is it goes to default statement
                    if (!int.TryParse(Console.ReadLine(), out int userChoiceInMenu))
                    {

                    }
                    switch (userChoiceInMenu)
                    {
                        // View Accounts and balance
                        case 1:
                            Console.Clear();
                            SeeAccounts(userIndex, accounts, logInSuccessful, balances);
                            KeyPressFunction(logInSuccessful);
                            break;

                        // Transfer Money
                        case 2:
                            Console.Clear();
                            TransferMoney(userIndex, accounts, balances, logInSuccessful);
                            break;

                        // Withdraw Money
                        case 3:
                            Console.Clear();
                            WithdrawMoney(userIndex, accounts, balances, logInSuccessful, usernames, passwords, inputUsername, inputPassword);
                            break;

                        // Exit the program from menu
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Successfully logged out");
                            logInSuccessful = false;
                            failedLogin = true;
                            break;

                        // If user choose number over the options
                        default:
                            Console.WriteLine("Invalid Choice");
                            KeyPressFunction(logInSuccessful);
                            failedLogin = false;
                            break;

                    }
                }
                if (!failedLogin)
                {
                    LogInAttempts++;
                    // Failed to log in
                    if (LogInAttempts < logInMaxAttempts)
                    {
                        Console.Clear();
                        Console.WriteLine("Login failed. Invalid username or password. Please try again.");
                        Console.WriteLine($"You have {logInMaxAttempts - LogInAttempts} attempts left");
                    }
                    // If max amount of attempts is fullfilled this exit the program
                    else
                    {
                        Console.Clear();
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

        static void SeeAccounts(int userIndex, string[][] accounts, bool logInSuccessful, decimal[][] balances)
        {
            // Goes through the strins in accounts untill it finds the string for the index number of users,EX userIndex = 4, Account string = 4

            for (int i = 0; i < accounts[userIndex].Length; i++)
            {
                Console.WriteLine($"{i + 1}.{accounts[userIndex][i]}: {balances[userIndex][i]}");
            }
        }

        static void TransferMoney(int userIndex, string[][] accounts, decimal[][] balances, bool logInSuccessful)
        {
            // Show the accounts, Ask from which account to transfer from
            SeeAccounts(userIndex, accounts, logInSuccessful, balances);
            Console.WriteLine($"From what account would you like to transfer from:");
            int userAccountToTransferFrom = Convert.ToInt32(Console.ReadLine());

            // Ask user to which account to transfer to
            Console.WriteLine($"To what account would you like to transfer to:");
            int userAccountToTransferTo = Convert.ToInt32(Console.ReadLine());

            // Amount the user wants to transfer
            Console.WriteLine($"How much would you like to transfer:");
            decimal userAmountToTransfer = Convert.ToDecimal(Console.ReadLine());



            // If user try to transfer more balance than there is:
            if (balances[userIndex][userAccountToTransferFrom - 1] < userAmountToTransfer)
            {
                Console.WriteLine("Insufficient Funds");
            }

            // If user try to transfer enought funds in the account:
            else if (balances[userIndex][userAccountToTransferFrom - 1] >= userAmountToTransfer)
            {
                balances[userIndex][userAccountToTransferFrom - 1] -= userAmountToTransfer;

                balances[userIndex][userAccountToTransferTo - 1] += userAmountToTransfer;
                Console.Clear();
                Console.WriteLine("Transfer Completed");
                Console.WriteLine($"{accounts[userIndex][userAccountToTransferFrom - 1]}: {balances[userIndex][userAccountToTransferFrom - 1]}\n{accounts[userIndex][userAccountToTransferTo - 1]}: {balances[userIndex][userAccountToTransferTo - 1]}");
            }
            // Option to go back or exit
            KeyPressFunction(logInSuccessful);
        }
        static void WithdrawMoney(int userIndex, string[][] accounts, decimal[][] balances, bool logInSuccessful, string[] usernames, string[] passwords, string inputUsername, string inputPassword)
        {
            // Show the accounts, Ask from which account to withdraw from
            SeeAccounts(userIndex, accounts, logInSuccessful, balances);
            Console.WriteLine($"From what account would you like to withdraw money from:");
            int userAccountToWithdrawFrom = Convert.ToInt32(Console.ReadLine());

            // Amount the user wants to withdraw
            Console.WriteLine($"How much would you like to withdraw:");
            decimal userAmountToWithdraw = Convert.ToDecimal(Console.ReadLine());

            // Ask the user for password to withdraw
            Console.WriteLine($"Please enter your password to withdraw:");
            string userPasswordForWithdraw = Console.ReadLine();

            if (!LoginCheck(usernames, passwords, inputUsername, userPasswordForWithdraw))
            {
                Console.WriteLine("Wrong Password!");
                // Option to go back or exit
                KeyPressFunction(logInSuccessful);
                return;

            }

            // If user try to withdraw more money than there is:
            if (balances[userIndex][userAccountToWithdrawFrom - 1] < userAmountToWithdraw)
            {
                Console.WriteLine("Insufficient Funds");
                KeyPressFunction(logInSuccessful);
                return;
            }

            // Takes away the amount the user wants to withdraw
            balances[userIndex][userAccountToWithdrawFrom - 1] -= userAmountToWithdraw;

            Console.Clear();
            Console.WriteLine($"Withdraw Completed\nDon't forget your card!\n{accounts[userIndex][userAccountToWithdrawFrom - 1]}: {balances[userIndex][userAccountToWithdrawFrom - 1]}");

            // Option to go back or exit
            KeyPressFunction(logInSuccessful);


        }
        static void KeyPressFunction(bool logInSuccessful)
        {

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
                Console.Clear();
                logInSuccessful = true;
            }
        }
    }
}