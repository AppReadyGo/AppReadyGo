﻿using AppReadyGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Commands.Users
{
    public class CreateAPIMemberCommand : CreateUserCommand
    {
        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public Gender? Gender { get; protected set; }

        public AgeRange? AgeRange { get; protected set; }

        public int? CountryId { get; protected set; }

        public string Zip { get; protected set; }

        public int[] ApplicationTypes { get; protected set; }

        public CreateAPIMemberCommand(string email, string password, string firstName, string lastName, Gender? gender, AgeRange? ageRange, int? countryId, string zip, int[] applicationTypes)
            : base(email, password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.AgeRange = ageRange;
            this.CountryId = countryId;
            this.Zip = zip;
            this.ApplicationTypes = applicationTypes;
        }
    }
}