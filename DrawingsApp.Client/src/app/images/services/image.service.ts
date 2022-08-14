import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IImage } from 'src/app/core/interfaces/IImage';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http:HttpClient) { }
  sendImage(blob:Blob){
    let payload=new FormData();
    payload.append("image",blob!);
    return this.http.post(environment.imageApi+"Image",payload);
  }
  getImages(userId:string|null):Observable<any>{
    return this.http.get<Array<IImage>>(environment.imageApi+"Image/"+(userId==null?"":userId));
  }
}
