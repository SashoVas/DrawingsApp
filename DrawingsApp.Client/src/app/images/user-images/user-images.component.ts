import { Component, Input, OnInit } from '@angular/core';
import { IImage } from 'src/app/core/interfaces/IImage';
import { environment } from 'src/environments/environment';
import { ImageService } from '../services/image.service';
@Component({
  selector: 'app-user-images',
  templateUrl: './user-images.component.html',
  styleUrls: ['./user-images.component.css']
})
export class UserImagesComponent implements OnInit {
  imagesUrl:string=environment.imageApi;
  data:Array<IImage>=[];
  selectedImage:IImage|null=null;
  @Input()imagesMaxCount!:number;
  imagesCount:number=0;
  page:number=0;
  constructor(private imageService:ImageService) { 
  }
  getSelectedImages(){
    return this.data.filter(i=>i.isSelected).map(i=>i.imgUrl);
  }
  fetchData():void{
    this.imageService.getImages(null,this.page)
      .subscribe(data=>this.data=this.data.concat(data));
  }
  ngOnInit(): void {
    this.fetchData();
  }
  selectImage(imgIndex:number):void{
    this.selectedImage=this.data[imgIndex];
  }
  selectForPost():void{
    this.selectedImage!.isSelected=!this.selectedImage?.isSelected;
    if(this.selectedImage!.isSelected)this.imagesCount++;
    else this.imagesCount--;
    if(this.imagesCount>this.imagesMaxCount){
      this.selectedImage!.isSelected=false;
      this.imagesCount--;
    }
  }
  loadMore(){
    this.page++;
    this.fetchData();
  }
}
