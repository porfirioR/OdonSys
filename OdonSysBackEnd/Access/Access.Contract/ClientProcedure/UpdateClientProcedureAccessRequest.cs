﻿using Utilities.Enums;

namespace Access.Contract.ClientProcedure
{
    public record UpdateClientProcedureAccessRequest(
        string UserClientId,
        string ProcedureId,
        int Price,
        ProcedureStatus Status
    ) { }
}