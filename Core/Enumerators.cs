﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace AppReadyGo.Core
{
    public enum ThemeType
    {
        SystemMail = 1,
        Mail,
        Page
    }

    public enum ApplicationEvent
    {
        FatalError = 1,
        Error
    }

    [Flags]
    public enum StaffRole
    {
        Administrator = 1
    }

    public enum UserType : byte
    {
        Staff = 1,
        Member,
        ApiMember
    }

    public enum DataGrouping
    {
        Minute,
        Hour,
        Day,
        Month,
        Year
    }

    public enum ErrorCode
    {
        WrongNameParameter,
        WrongImportData,
        WrongEmail,
        WrongPassword,
        EmailExists,
        EmailDoesNotExists,
        TagExists,
        WrongCollection,
        WrongParameter,
        WrongDescription
    }
}
