export class CreateRoleApiRequest {
  constructor(
    public name: string,
    public code: string,
    public permissions: string[]
  ) { }
}
