import { SafeUrl } from "@angular/platform-browser";

export class FileModel {
  constructor(
    public url: string,
    public safeUrl: SafeUrl,
    public format: string,
    public dateCreated: Date,
    public name: string,
    public fullUrl: string
  ) { }
}
