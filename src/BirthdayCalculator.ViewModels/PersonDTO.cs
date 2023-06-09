﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayCalculator.ViewModels
{
    public  class PersonDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public string Fullname => $"{FirstName} {LastName}";
    }
}
