import { FieldId } from "../app/core/enums/field-id.enum";

export const environment = {
  production: true,
  apiUrl: 'https://localhost:44310/api',
  systemAttributeModel: [
    { id: FieldId.Active, value: 'active' },
    { id: FieldId.Approved, value: 'approved' },
    { id: FieldId.Id, value: 'id' },
  ]
};
