import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IImage } from 'src/app/core/interfaces/IImage';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http:HttpClient) { }
  sendImage(blob:Blob,name:string){
    let payload=new FormData();
    payload.append("image",blob!);
    payload.append("name",name);
    return this.http.post(environment.imageApi+"Image",payload,{responseType: 'text'});
  }
  getImages(userId:string|null,page:number):Observable<any>{
    return this.http.get<Array<IImage>>(environment.imageApi+"Image/"+(userId==null?"":userId),{params:{page}});
  }
  deleteImages(images:Array<string>){
    return this.http.delete(environment.imageApi+"Image/",{params:{images}});
  }
}
