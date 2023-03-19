using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayCalculator.Domain.Services
{
    public interface IBirthdayService
    {
        bool IsBirthdayToday(DateTime birthdate);
    }
}
