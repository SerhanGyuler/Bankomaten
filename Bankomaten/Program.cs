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

                // Call login check method
                logInSuccessful = LoginCheck(usernames, passwords, inputUsername, inputPassword);

                // Login successfull

                while (logInSuccessful)
                {
                    Console.WriteLine("1. View your accounts and balance\n2. Transfer between accounts\n3. Withdraw money\n4. Log out");
                    int userChoiceInMenu = Convert.ToInt32(Console.ReadLine());
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
            for (int i = 0; i < usernames.Length; i++)
            {
                // Goes thourgh the arrays at the same time to check if it the same in the same position of the array
                if (usernames[i] == inputUsername && passwords[i] == inputPassword)
                {
                    return true;  // Login successful
                }
            }

            return false;  // Login failed
        }
    }
}
