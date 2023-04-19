export class UpdateRoleApiRequest {
  constructor(
    public name: string,
    public code: string,
    public active: boolean,
    public permissions: string[],
    public users: string[],
  ) { }
}
