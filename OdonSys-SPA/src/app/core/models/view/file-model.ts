import { SafeUrl } from "@angular/platform-browser";

export class FileModel {
  constructor(
    public url: SafeUrl,
    public format: string,
    public dateCreated: Date
  ) { }
}
