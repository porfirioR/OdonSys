export interface ProcedureModel {
    id: string;
    active: boolean;
    dateCreate: Date;
    dateModified: Date;
    name: string;
    description: string;
    estimatedSessions: string;
    procedureTeeth: string[];
}
