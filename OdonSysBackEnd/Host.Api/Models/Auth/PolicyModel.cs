using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Host.Api.Models.Auth
{
    public sealed class PolicyModel
    {
        public string Name { get; set; }
        public IEnumerable<IAuthorizationRequirement> AuthRequirements { get; set; }
        
        public PolicyModel(string name, IAuthorizationRequirement authRequirement) : this(name, new List<IAuthorizationRequirement> { authRequirement }) { }

        public PolicyModel(string name, IEnumerable<IAuthorizationRequirement> authRequirements)
        {
            Name = name;
            AuthRequirements = authRequirements;
        }
    }
}
