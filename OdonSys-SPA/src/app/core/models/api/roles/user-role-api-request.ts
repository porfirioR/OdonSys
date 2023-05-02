export class UserRoleApiRequest {
  constructor(
    public userId: string,
    public roles: string[]
  ) { }
}
