using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetirementAgeCalcClient.Helpers
{
    public class Input
    {
        public static int GetAge()
        {
            Console.Write("Age (Should be greater than 18): ");
            int id = 0;

            while (!int.TryParse(Console.ReadLine(), out id))// || age < 18
            {
                Console.WriteLine("Please Enter a valid numerical value!");
                Console.Write("Please Enter your Age: ");
            }
            return id;
        }

        public static string GetFullName()
        {
            Console.Write("FullName: ");
            string name = Console.ReadLine();

            while (string.Compare(name,string.Empty) == 0)// || name.Length > 35)
            {
                Console.WriteLine("Please Enter a valid name(less than 35 characters)!");
                Console.Write("Please Enter your Name: ");
                name = Console.ReadLine();
            }
            return name;
        }
        public static string GetGender()
        {
            Console.Write("Gender(M/F): ");
            string gender = Console.ReadLine();

            while (string.Compare(gender.ToUpper(), "M") != 0 && string.Compare(gender.ToUpper(), "F") != 0)
            {
                Console.WriteLine("Please Enter a valid value!");
                Console.Write("Please Enter Gender: ");
                gender = Console.ReadLine();
            }
            return gender;
        }
        public static bool CheckSave()
        {
            Console.Write("Do you want to save the Retirement details(Y/N)?: ");
            string strSave = Console.ReadLine().ToUpper();
         
            while (!(string.Compare(strSave, "Y") == 0 || string.Compare(strSave, "N") == 0))
            {
                Console.WriteLine("Please Enter a valid value!");
                Console.Write("Do you want to save the Retirement details(Y/N) ?: ");
                strSave = Console.ReadLine().ToUpper();
            }
            return string.Compare(strSave, "Y") == 0 ? true : false;
        }

        public static long GetINRValue(string input)
        {
            string inputString = $"{input}(INR): ";
            Console.Write(inputString);
            long value = 0;

            while (!long.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Please Enter a valid numerical value!");
                Console.Write(inputString);
            }
            return value;
        }

        public static decimal GetPercentValue(string input)
        {
            string inputString = $"{input}(%): ";
            Console.Write(inputString);
            decimal value = 0;

            while (!decimal.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Please Enter a valid percent value!");
                Console.Write(inputString);
            }
            return value;
        }
    }
}

