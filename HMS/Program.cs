using HMS.classes;
using Newtonsoft.Json;

namespace HMS;

class Program
{
    static void Main(string[] args)
    {
        Utility utility = new Utility();
        Authentication auth = new Authentication();

        bool active = true;
        
        utility.InitialiseApplication();

        while (active)
        {
            #region NotAuthenticated

            while (!auth.IsAuthenticated)
            {
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

            
                // Welcome Message
                Console.WriteLine("Welcome to the HMS application! Please enter your credentials.\n");
            
                // Email
                Console.Write("Email: ");
                string? email = Console.ReadLine();
            
                // Password
                Console.Write("Password: ");
                string? password = "";
                // Found information about this on https://stackoverflow.com/questions/23433980/c-sharp-console-hide-the-input-from-console-window-while-typing
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
            
                    password += key.KeyChar.ToString();
                }
            
                if (email == "" || password == "")
                {
                    Console.Clear();
                    Console.Write("\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid credentials entered. Please try again.\n");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to return to login again.");
                    Console.Write("\n");
                    Console.ReadKey();
                }
                else
                {
                    Console.ResetColor();

                    Console.Clear();

                    bool usr = auth.SignInUser(email, password);
            
                    if (usr)
                    {
                        break;
                    } else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Clear();
                        Console.WriteLine("Invalid email or password. Press any key to try again. Press '?' if you need assistance.");
                        var key = Console.ReadKey(true);
                        Console.WriteLine(key.KeyChar.ToString());
                        if (key.KeyChar.ToString() == "?") {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.Clear();
                            Console.WriteLine("Assitance");
                            Console.WriteLine("----------\n");
                            Console.WriteLine("");
                            Console.WriteLine("If you need any assistance, please contact the system administrator.");
                            Console.WriteLine("");
                            Console.WriteLine("Extention: 010");
                            Console.WriteLine("Email: support@hms.local");
                            Console.WriteLine("");
                            Console.WriteLine("Press any key to return to the login screen.");
                            Console.ReadKey();
                        }
                    }
                }
            }

            #endregion

            #region IsAuthenticated

            while (auth.IsAuthenticated)
            {
                Console.Clear();
                
                // Welcome Screen
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine($"Current User: {auth.Name} ({auth.Id}).");
                Console.WriteLine($"Current email: {auth.Email}.");
                Console.Write('\n');
                Console.Write('\n');

                // Options
               
                Console.WriteLine("1. Create Patient");
                Console.WriteLine("2. List Patients");
                Console.WriteLine("0. Sign Out");

                Console.Write("\n");
                
                // Selections
                Console.Write("Please select an option [e.g. 1]: ");
                string userInputOption = Console.ReadLine();

                switch (userInputOption)
                {
                    case "0":
                        auth.SignOutUser();
                        break;
                    case "1":
                        utility.CreatePatient(auth.Name);
                        break;
                    case "2":
                        Console.Clear();

                        string[] files = Directory.GetFiles(utility.patientDirectory);

                        

                        bool activeLoop = true;

                        while (activeLoop)
                        {
                            Console.Clear();
                            Console.WriteLine($"Current User: {auth.Name} ({auth.Id}).");
                            Console.WriteLine($"Current email: {auth.Email}.");
                            Console.Write('\n');
                            Console.Write('\n');
                            foreach (string file in files)
                            {
                                var jsonString = File.ReadAllText(file);
                                List<Patient> patientFile = JsonConvert.DeserializeObject<List<Patient>>(jsonString);

                                Console.WriteLine($"[{patientFile[0].Id}] Name: {patientFile[0].Name} | Email: {patientFile[0].Email} | Phone: {patientFile[0].Phone}");
                            }
                            Console.Write("\n");
                            Console.WriteLine("Options: [q] Exit back to home | [r] Read a patient file | [s] Search a patient");
                            var key = Console.ReadKey(true);

                            switch (key.KeyChar.ToString().ToLower())
                            {
                                case "q":
                                    activeLoop = false;
                                    break;
                                case "r":
                                    Console.WriteLine("\nPlease enter the ID of the user:");
                                    string userId = Console.ReadLine()!;

                                    if (String.IsNullOrEmpty(userId))
                                    {
                                        return;
                                    } else
                                    {
                                        if (File.Exists($"{utility.patientDirectory}"))
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }

                        break;
                    default:
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Clear();
                        Console.WriteLine("Invalid Option selected. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }

            #endregion
        }
    }
}