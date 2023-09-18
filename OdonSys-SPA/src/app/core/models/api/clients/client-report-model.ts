import { ClientApiModel } from "./client-api-model";
import { ClientInvoiceReportApiModel } from "./client-invoice-report-api-model";

export interface ClientReportModel {
  clientModel: ClientApiModel
  invoiceModels: ClientInvoiceReportApiModel[]
}
