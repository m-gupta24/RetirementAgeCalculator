using Newtonsoft.Json;
using RetirementAgeCalcClient.Helpers;
using RetirementAgeCalcClient.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace RetirementAgeCalcClient
{
    class Program
    {
        static readonly string baseUrl = "http://localhost:53212";
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Retirement Age Calculator");

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(0, 0, 12);//12 seconds timeout

            try
            {
                var accessToken = await GetToken();

                if (accessToken.auth_token is not null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken.auth_token);
                    Console.WriteLine("\n Login Successfull.");

                    DisplayMenu();

                    string key;
                    while ((key = Console.ReadKey().KeyChar.ToString()) != "3")
                    {
                        try
                        {
                            int.TryParse(key, out int keyValue);

                            switch (keyValue)
                            {
                                case 1:
                                    await CalculateRetirementAge();
                                    break;
                                case 2:
                                    await ShowRecord();
                                    break;
                                case 3:
                                    break;
                                default:
                                    Console.WriteLine("Please try again!");
                                    break;
                            }

                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        DisplayMenu();
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("App interrupted.");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("App closed.");
            }

            Console.ReadLine();
        }

        static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Get Retirement Age");
            Console.WriteLine("2. Get Details");
            Console.WriteLine("3. Close app (X)");
            Console.WriteLine();
            Console.Write("Enter the option (number):  ");
        }
        static async Task ShowRecord()
        {
            Console.WriteLine();
            Console.WriteLine("Please Enter your Id: ");
            var val = Console.ReadLine();
            int id = Convert.ToInt32(val);
            Console.WriteLine();
            var result = await GetRecord(id);

            //if(result is null) Console.WriteLine($"Status: Unprocessable");
            if (result?.Id > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Retirement Details");
                Console.WriteLine();

                Console.WriteLine($"FullName: {result.FullName}");
                Console.WriteLine($"Age: {result.Age}");
                Console.WriteLine($"Gender: {result.Gender}");
                Console.WriteLine($"Monthly Savings: {result.MonthlySavings}");
                Console.WriteLine($"Target Retirement Funds: {result.TargetRetirementFunds}");
                Console.WriteLine($"Retirement Age: {result.RetirementAge}");
            }
            else
            {
                Console.WriteLine($"Status: Connection failed!");
            }

            Console.WriteLine();
        }

        static async Task CalculateRetirementAge()
        {
            RetirementDetails details = new RetirementDetails();
            Console.WriteLine();
            Console.WriteLine("Please Enter your Details - ");
            details.FullName = Input.GetFullName();
            details.Age = Input.GetAge();
            details.Gender = Input.GetGender();
            details.MonthlySavings = Input.GetINRValue("Monthly Savings");
            //details.MonthlyExpense = Input.GetINRValue("Monthly Expense");
            //details.CurrentAccumulation = Input.GetINRValue("Current Accumulation");
            details.TargetRetirementFunds = Input.GetINRValue("Target Retirement Funds");
            //details.ROIDuringAccumulation = Input.GetPercentValue("ROI During Accumulation");
            //details.ReturnOnCorpus = Input.GetPercentValue("Return On Corpus");
            //details.InflationRate = Input.GetPercentValue("Inflation Rate");
            details.SaveDetails = Input.CheckSave();

            var result = await GetRetirementAge(details);
            if (result is null)
                Console.WriteLine($"Status: Connection failed");
            else
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Retirement Age: {result.RetirementAge}");

                if (result.Id > 0)
                    Console.WriteLine($"Retirement Details Id: {result.Id}");
                Console.WriteLine();
            }
        }

        static Login ReadLoginDetails()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Enter the user name: ");
            var username = Console.ReadLine();
            Console.Write("Enter the password: ");
            var password = Console.ReadLine();
            return new Login() { UserName = username, Password = password };

        }
        static async Task<Token> GetToken()
        {
            Token token = null;
            string key;

            Console.WriteLine();
            Console.WriteLine("You will be asked to enter your credentials.");
            Console.Write("Press 'Enter' to continue.");
            while ((key = Console.ReadKey().KeyChar.ToString().ToUpper()) != "N")
            {
                int.TryParse(key, out int keyValue);

                var login = ReadLoginDetails();
                token = await Authenticate(login);
                if (token.auth_token is null)
                    Console.WriteLine("\n Either the username or password is incorrect. Do you want to try again(Y/N)?");
                else
                    break;
            }
           
            return  token;
        }
        static async Task<Token> Authenticate(Login login)
        {
            HttpClient client1 = new HttpClient();
            string url = "http://localhost:50341";
            client1.BaseAddress = new Uri(url);
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            JsonContent content = JsonContent.Create(new { Username = login.UserName, Password = login.Password });
            var response = await client1.PostAsync($"/user/authenticate", content);
            var token = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Token>(token);
        }
        static async Task<RetirementRead> GetRecord(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"/Retirement/{id}");
            return await DeserializeResponseContent(response);
        }
        static async Task<RetirementAgeDetails> GetRetirementAge(RetirementDetails details)
        {
            JsonContent content = JsonContent.Create(details);
            HttpResponseMessage response = await client.PostAsync("/Retirement/GetRetirementAge", content);
            
            return await DeserializeAgeResponseContent(response);
        }

        static async Task<RetirementRead> DeserializeResponseContent(HttpResponseMessage response)
        {
            //string transactionResult = string.Empty;
            RetirementRead retirementRead = new RetirementRead(); ;
            if (response.IsSuccessStatusCode)
            {
                var transactionResult = await response.Content.ReadAsStringAsync();
                retirementRead = JsonConvert.DeserializeObject<RetirementRead>(transactionResult);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Error : {response.StatusCode}");
            }

            return retirementRead;
        }

        static async Task<RetirementAgeDetails> DeserializeAgeResponseContent(HttpResponseMessage response)
        {
            string transactionResult = string.Empty;
            RetirementAgeDetails retirementAgeDetails = new RetirementAgeDetails();
            if (response.IsSuccessStatusCode)
            {
                transactionResult = await response.Content.ReadAsStringAsync();
                retirementAgeDetails = JsonConvert.DeserializeObject<RetirementAgeDetails>(transactionResult);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Error : {response.StatusCode}");
            }

            return retirementAgeDetails;
        }
    }
}



