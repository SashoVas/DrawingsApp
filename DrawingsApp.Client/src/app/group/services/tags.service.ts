import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITag } from 'src/app/core/interfaces/ITag';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TagsService {

  constructor(private http:HttpClient) { }
  getTags():Observable<any>{
    return this.http.get<Array<ITag>>(environment.groupApi+"tag/")
  }
  createTag(tagName:string,tagInfo:string):Observable<any>{
   return this.http.post(environment.groupApi+"tag/",{tagName,tagInfo}); 
  }
}
