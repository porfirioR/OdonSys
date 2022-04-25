export class UpdateProcedureRequest {
    constructor(public id: string, public description: string, public procedureTeeth: string[]) {
    }
}
