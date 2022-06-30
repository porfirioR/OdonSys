export class UpdateClientRequest {
  constructor(
    protected id: string,
    protected name: string,
    protected middleName: string,
    protected lastName: string,
    protected middleLastName: string,
    protected phone: string,
    protected country: string,
    protected email: string
  ) { }
}
