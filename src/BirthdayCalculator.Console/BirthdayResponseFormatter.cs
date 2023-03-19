using BirthdayCalculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayCalculator.Console
{
    public interface IOutputFormatter
    {
        string Format(BirthdayResponse response);
    }

    public class BirthdayResponseFormatter : IOutputFormatter
    {
        public string Format(BirthdayResponse response)
        {
            if (response == null || response.BirthdayCelebrants == null || !response.BirthdayCelebrants.Any())
            {
                return "No birthdays found for today.";
            }

            var sb = new StringBuilder();

            foreach (var person in response.BirthdayCelebrants)
            {             
                sb.AppendFormat("Happy Birthday {0}! - Year {1}\n", person.Fullname, person.BirthDate.HasValue ? person.BirthDate.Value.ToString("yyyy") : "Unknown");
            }

            return sb.ToString().TrimEnd('\n');
        }
    }
}
