using BusinessLayer.DTO;
using BusinessLayer.Enums;
using BusinessLayer.Exceptions;
using BusinessLayer.Extensions;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Helpers
{
    public static class Validations
    {
        public static void Validate(this long amount, string argName)
        {
            if (amount <= 0) throw new InvalidValueException(argName);
        }

        public static void Validate(this int arg, string argName)
        {
            if (arg <= 0) throw new InvalidValueException(argName);
        }
        public static void Validate(this RetirementEntity retirementEntity, int value)
        {
            if(retirementEntity is null) throw new InvalidIdException(value);
        }

        public static void Validate(this RetirementCreateDto retirementCreateDto, string argName)
        {
            //check if null
            if (retirementCreateDto is null) throw new ArgumentNullException(argName);

            //name should not be empty and and value should be less than 35 characters
            if (string.IsNullOrWhiteSpace(retirementCreateDto.FullName)
                || retirementCreateDto.FullName.Length > 35)
                throw new InvalidNameException(argName);

            //age should be greater than 18
            if (retirementCreateDto.Age < 18) throw new InvalidValueException(nameof(retirementCreateDto.Age));

            //gender should be as per enum description
            if (!(retirementCreateDto.Gender.ToUpper() == GenderEnum.Male.GetDescription()
               || retirementCreateDto.Gender.ToUpper() == GenderEnum.Female.GetDescription()
               || retirementCreateDto.Gender.ToUpper() == GenderEnum.Other.GetDescription()))
                throw new InvalidValueException(nameof(retirementCreateDto.Gender));

            retirementCreateDto.TargetRetirementFunds.Validate(nameof(retirementCreateDto.TargetRetirementFunds));
            retirementCreateDto.MonthlySavings.Validate(nameof(retirementCreateDto.MonthlySavings));
        }
    }
}
