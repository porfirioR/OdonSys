﻿using System.Collections.Generic;

namespace Access.Contract.Roles
{
    public class CreateRoleAccessRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}