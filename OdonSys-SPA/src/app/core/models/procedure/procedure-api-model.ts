export interface ProcedureApiModel {
  id: string
  active: boolean
  dateCreated: Date
  dateModified: Date
  name: string
  description: string
  procedureTeeth: string[]
  price: number
}
