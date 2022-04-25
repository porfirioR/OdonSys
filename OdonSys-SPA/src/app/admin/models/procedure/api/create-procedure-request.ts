export class CreateProcedureRequest {
    constructor(public name: string, public description: string, public estimatedSessions: string, public procedureTeeth: string[]) {
    }
}
