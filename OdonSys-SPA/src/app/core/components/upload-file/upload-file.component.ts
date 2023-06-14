import { Component, Input, OnInit } from '@angular/core';
import { NgxDropzoneChangeEvent } from 'ngx-dropzone';
import { UploadFileModel } from '../../models/view/upload-file-model';
import { AlertService } from '../../services/shared/alert.service';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.scss']
})
export class UploadFileComponent implements OnInit {
  @Input() uploadFileConfig!: UploadFileModel

  public files: Array<File> = []
  constructor(
    private readonly alertService: AlertService
  ) { }

  ngOnInit() {
  }

  protected onSelect = (event: NgxDropzoneChangeEvent): void => {
    if (event.addedFiles.length > 0) {
      this.files.push(...event.addedFiles)
      if (!this.uploadFileConfig.multiple && this.files.length > 1) {
        this.files.splice(0, 1)
      }
    }
    if (event.rejectedFiles.length > 0) {
      let messageError = ''
      event.rejectedFiles.forEach((file, i) => {
        if (file['reason'] === 'type') {
          messageError = `${messageError} Archivo no válido "${file.name}" asegúrese de que su tipo de dato válido`
        } else if (file['reason'] === 'size') {
          messageError = `${messageError} Archivo "${file.name}" debe ser ${Math.floor(this.uploadFileConfig.maxFileSize / 1048576)} Mb. mas pequeño\n`
        }
      })
      if(messageError && messageError !== '') {
        this.alertService.showError(`${messageError}`)
      }
    }
  }

  protected onRemove = (file: File): void => {
    this.files.splice(this.files.indexOf(file), 1);
  }
}
