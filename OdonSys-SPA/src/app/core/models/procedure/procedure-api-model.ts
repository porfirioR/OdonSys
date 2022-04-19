export class ProcedureApiModel {
    id!: string;
    active: boolean = false;
    dateCreate!: Date;
    dateModified!: Date;
    name!: string;
    description!: string;
    estimatedSessions!: string;
    procedureTeeth: string[] = [];

    constructor() {
        
    }
}
