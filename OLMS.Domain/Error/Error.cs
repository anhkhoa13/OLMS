﻿using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Error;

public static class Error
{   
    public static class User
    {
        public static readonly string EmptyGuid = "Guid cannot be empty";
        public static readonly string EmptyUsername = "Username cannot be empty";
        public static readonly string EmptyName = "Full name cannot be empty";
        public static readonly string EmptyEmail = "Email cannot be empty";
        public static readonly string EmptyPassword = "Password cannot be empty";

        public static readonly string InvalidUsername = "Username must be 3-20 characters long and contain only letters, numbers, and underscores.";
        public static readonly string InvalidFullName = "FullName must be 3-50 characters long and contain only letters, numbers, and spaces.";
        public static readonly string InvalidEmail = "Invalid email format.";
        public static readonly string InvalidPassword = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";

        public static readonly string NonUniqueUsername = "Username already exists";
        public static readonly string NonUniqueEmail = "Email already exists";

    }
    
}
