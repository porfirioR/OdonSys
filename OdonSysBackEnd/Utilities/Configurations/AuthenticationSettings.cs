﻿using System.Diagnostics.CodeAnalysis;

namespace Utilities.Configurations
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationSettings
    {
        public const string ConfigSection = "Authentication";
        public AzureB2CSettings AzureB2C { get; set; }
    }
}
