import { Component, inject, input, OnInit, output } from '@angular/core';
import { Member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { Photo } from '../../_models/photo';
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-photo-editor',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './photo-editor.component.html',
  styleUrl: './photo-editor.component.css',
})
export class PhotoEditorComponent implements OnInit {
  member = input.required<Member>();
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);

  // uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  memberChange = output<Member>();

  ngOnInit(): void {
    // this.user = this.accountService.currentUser() ?? undefined;
    // this.initializeUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  setMainPhoto(photo: Photo) {
    this.memberService.setMainPhoto(photo).subscribe({
      next: () => {
        const user = this.accountService.currentUser();

        if (user) {
          user.photoUrl = photo.url;
          this.accountService.setCurrentUser(user); //actualizo el user
        }
        const updatedMember = { ...this.member() };

        updatedMember.photoUrl = photo.url;
        updatedMember.photos.forEach((p) => {
          if (p.isMain) p.isMain = false;
          if (p.id === photo.id) p.isMain = true;
        });

        this.memberChange.emit(updatedMember);
      },
    });
  }

  deletePhoto(photoId: number) {
    this.memberService.deletePhoto(photoId).subscribe({
      next: () => {
        if (this.member) {
          this.member().photos = this.member().photos.filter(
            (x) => x.id !== photoId,
          );
        }
      },
    });
  }

  // necesito ponerle el token xq este req no va a pasar xel interceptor
  // initializeUploader() {
  //   this.uploader = new FileUploader({
  //     url: this.baseUrl + 'users/add-photo',
  //     authToken: 'Bearer ' + this.user?.token,
  //     isHTML5: true,
  //     allowedFileType: ['image'],
  //     removeAfterUpload: true,
  //     autoUpload: false,
  //     maxFileSize: 2 * 1024 * 1024, // 2 megas
  //   });
  //   this.uploader.onAfterAddingFile = (file) => {
  //     // no se necesita xq se estan mandando en el token
  //     file.withCredentials = false;
  //   };
  //   this.uploader.onSuccessItem = (item, response, status, headers) => {
  //     if (response) {
  //       const photo: Photo = JSON.parse(response);
  //       this.member?.photos.push(photo);
  //       if (photo.isMain && this.user && this.member) {
  //         // this.user.photoUrl = photo.url;
  //         this.member.photoUrl = photo.url;
  //         this.accountService.setCurrentUser(this.user);
  //       }
  //     }
  //   };
  // }
}
