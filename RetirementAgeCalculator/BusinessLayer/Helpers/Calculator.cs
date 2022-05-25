using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Helpers
{
    public static class Calculator
    {
        /// <summary>
        /// This method calculates the Retirement age based on the some
        /// assumptions -
        /// Annual Interest Rate on invested savings - 5% (ROI)
        /// Current Retirement Savings = 0
        /// The yearly savings are calculated and based on that 
        /// the number of years to accumulate the targetRetirementFunds is 
        /// calculated using Annual Compound interest.
        /// </summary>
        /// <param name="currentAge">Current Age</param>
        /// <param name="targetRetirementFunds">Expected Retirement Corpus</param>
        /// <param name="monthlySaving">Monthly Savings</param>
        /// <returns>Retirement Age</returns>
        public static int GetRetirementAge(int currentAge, long targetRetirementFunds, long monthlySaving)
        {
            targetRetirementFunds.Validate(nameof(targetRetirementFunds));
            monthlySaving.Validate(nameof(monthlySaving));
            try
            {
                //assuming 5 percent annual returns on savings
                double roiOnSaving = 0.05;

                var numOfYearsToRetirement = GetYears(targetRetirementFunds, roiOnSaving, monthlySaving);
                var retirementAge = currentAge + numOfYearsToRetirement;

                return retirementAge;
            }
            catch(Exception ex)
            {
                throw ex; 
            }
        }

        /// <summary>
        /// Calculates the number of years to accumulate the required amount
        /// </summary>
        /// <param name="targetRetirementFunds"></param>
        /// <param name="roi"></param>
        /// <param name="monthlySaving"></param>
        /// <returns></returns>
        private static int GetYears(long targetRetirementFunds, double roi, long monthlySaving)
        {
            double numOfYears = 1;
            long yearlySaving = monthlySaving * 12;
            double savings = 0;//initial savings 0

            if (targetRetirementFunds <= yearlySaving)
                return 1;

            while (targetRetirementFunds > savings)
            {
                double principal = yearlySaving + savings;
                savings = GetSavings(principal, roi, numOfYears++);

                if (numOfYears > 100) break;
            }

            return (int)numOfYears;
        }

        /// <summary>
        /// Calculate the total savings in one year 
        /// </summary>
        /// <param name="yearlySavings"></param>
        /// <param name="roiOnSavings"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static double GetSavings(double savings, double roiOnSavings, double n)
                       => savings * (Math.Pow((1 + roiOnSavings), n));

    }
}
