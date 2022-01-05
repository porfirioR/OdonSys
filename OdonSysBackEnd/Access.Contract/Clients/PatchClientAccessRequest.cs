using System;

namespace Access.Contract.Clients
{
    public class PatchClientAccessRequest
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
    }
}
