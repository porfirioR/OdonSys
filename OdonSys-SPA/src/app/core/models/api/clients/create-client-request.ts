export class CreateClientRequest {
  constructor(
    protected name: string,
    protected middleName: string,
    protected lastName: string,
    protected middleLastName: string,
    protected document: string,
    protected ruc: string,
    protected country: string,
    protected phone: string,
    protected email: string
  ) { }
}
