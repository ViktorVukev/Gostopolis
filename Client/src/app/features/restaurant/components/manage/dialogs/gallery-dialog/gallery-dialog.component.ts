import {Component, Inject} from '@angular/core';
import {ToastrService} from "ngx-toastr";
import {RestaurantService} from "../../../../services/restaurant.service";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";

@Component({
  selector: 'app-gallery-dialog',
  templateUrl: './gallery-dialog.component.html',
  styleUrl: './gallery-dialog.component.css'
})
export class GalleryDialogComponent {

  photos: any = [];
  index: number = 0;
  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private restaurantService: RestaurantService,
    private toastrService: ToastrService) {
  }

  /* Gallery section */
  onSelect(event: any): void {
    if (event.target.files) {
      for (let i = 0; i < event.target.files.length; i++) {
        if (event.target.files[i].size > 10485760) {
          this.toastrService.error('Файлът не трябва да надвишава 10MB.');
          continue;
        }

        let reader = new FileReader();
        reader.readAsDataURL(event.target.files[i]);
        reader.onload = (events: any) => {
          this.photos.push(events.target.result);
        }
      }
    }
  }

  save(): void {
      for (const file of <Array<File>>this.photos) {
          if (file.size > 10485760) {
              this.toastrService.error('Снимката не трябва да надвишава 10MB.');
              return;
          }

          this.restaurantService
              .uploadImage({
                  imageBase64: file,
                  restaurantId: this.data
              })
              .subscribe();
      }
  }

  onDelete(index: number): void {
    if (index >= 0 && index < this.photos.length) {
      this.photos.splice(index, 1);
    }
  }

}
