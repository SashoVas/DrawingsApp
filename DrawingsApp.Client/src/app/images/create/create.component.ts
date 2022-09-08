import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { ImageService } from '../services/image.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit,AfterViewInit {
  imgSrc:string="";
 constructor(private activatedRoute:ActivatedRoute,private imageService:ImageService,private fb:UntypedFormBuilder,private router:Router,private toastr:ToastrService){
  }
  ngOnInit(): void {
    this.nameForm=this.fb.group({
      name:["",[Validators.required]]
    });
  }
  primaryColors:Array<Array<number>>=[
    [255,0,0,255],[0,255,0,255],[0,0,255,255],[255,255,0,255],[255,0,255,255],[0,255,255,255],
    [255,122,0,255],[255,0,122,255],[122,255,0,255],[0,255,122,255],[122,0,255,255],[0,122,255,255],
    [122,122,122,255],[50,50,50,255],[200,200,200,255],
    [255,122,122,255],[122,255,122,255],[122,122,255,255],[255,255,255,255],[0,0,0,255]
  ];
  nameForm!:UntypedFormGroup;
  @ViewChild('loadImageUrl') private loadImageUrl!:ElementRef;
  @ViewChild('width') private widthInput!:ElementRef;
  @ViewChild('height') private heightInput!:ElementRef;
  @ViewChild('drawingBoard')
  private canvas!: ElementRef<HTMLCanvasElement>;
  private isDrawing:boolean=false;
  private drawingBoard: CanvasRenderingContext2D|null=null;
  private isFilling:boolean=false; 
  private filling:boolean=false;
  private paddingSize=15;
  private nav_height=60;
  private drawing_screen_width=700;
  private drawing_screen_height=700;
  colorR=255;
  colorG=1;
  colorB=1;
  lineWidth=10
  
  ngAfterViewInit(): void {
    this.drawingBoard=this.canvas!.nativeElement.getContext('2d');
    this.canvas.nativeElement.height=this.drawing_screen_height;
    this.canvas.nativeElement.width=this.drawing_screen_width;
    let canvas=this.canvas;
    this.clear();
    let drawing_screen_height=this.drawing_screen_height;
    let drawing_screen_width=this.drawing_screen_width;
    this.activatedRoute.queryParamMap.subscribe(data=>{
      var img1 = new Image();
      let drawingBoard=this.drawingBoard;
      img1.onload = function () {
        if(img1.width>700 || img1.height>700){
          drawingBoard?.drawImage(img1, 0, 0,700,700);
        }
        else{
          drawing_screen_width=img1.width;
          drawing_screen_height=img1.height;
          canvas.nativeElement.height=drawing_screen_height;
          canvas.nativeElement.width=drawing_screen_width;
          drawingBoard?.drawImage(img1, 0, 0,drawing_screen_width,drawing_screen_height);
        }
      }
      img1.crossOrigin ='Anonymous';    
      img1.src =environment.imageApi+'Images/'+data.get("image")+'.jpg';
      this.drawing_screen_height=drawing_screen_height;
      this.drawing_screen_width=drawing_screen_width;
    });
  }
  setColor(color:Array<number>){
    this.colorR=color[0];
    this.colorG=color[1];
    this.colorB=color[2];
  }
  changeDrawingImageDimensions(){
    this.drawing_screen_width=this.widthInput.nativeElement.value;
    this.drawing_screen_height=this.heightInput.nativeElement.value;
    let drawing_screen_width=this.widthInput.nativeElement.value;
    let drawing_screen_height=this.heightInput.nativeElement.value;
    let canvas=this.canvas;
    let img=new Image();
    let drawingBoard=this.drawingBoard;
    img.onload = function () {
      canvas.nativeElement.height=drawing_screen_height;
      canvas.nativeElement.width=drawing_screen_width;
      drawingBoard!.fillStyle = "white";
      drawingBoard?.fillRect(0, 0,canvas.nativeElement.width, canvas.nativeElement.height);
      if(drawing_screen_width<img.width || drawing_screen_height<img.height){
        drawingBoard?.drawImage(img,0,0,drawing_screen_width,drawing_screen_height);
      }    
      else{
        drawingBoard?.drawImage(img,0,0);
      }
    }
    img.src=this.canvas.nativeElement.toDataURL();
  }
  startLine(event:MouseEvent){
    if (this.isFilling)
    {
      let sectionLength=window.innerWidth>1400? window.innerWidth*0.25:((window.innerWidth-this.drawing_screen_width)/2);
      let centerGaps=sectionLength*2 -this.drawing_screen_width;
      if(centerGaps<0)
      {
        centerGaps=0
      }
      this.fillAlgorihm(Math.round(event.clientX-(sectionLength+(centerGaps/2))),Math.round(event.clientY-this.nav_height-this.paddingSize-this.paddingSize));
    }
    else{
      this.drawingBoard?.beginPath();
      this.isDrawing=true;
      this.draw(event)
    }
  }
  finishLine(event:MouseEvent){
    this.isDrawing=false;
  }
  draw(event:MouseEvent){
    if (this.isDrawing && !this.filling){
      this.drawingBoard!.lineWidth=this.lineWidth;
      this.drawingBoard!.lineCap="round";
      this.drawingBoard!.strokeStyle="rgba("+this.colorR.toString()+","+this.colorG.toString()+","+this.colorB.toString()+",255)";
      let sectionLength=window.innerWidth>1400? window.innerWidth*0.25:((window.innerWidth-this.drawing_screen_width)/2);
      let centerGaps=sectionLength*2 -this.drawing_screen_width;
      if(centerGaps<0)
      {
        centerGaps=0
      }
      //mousePos-(leftSection+(paddingBoard/2)),mousePos-nav-paddingBoard-paddingAll  
      if(window.innerWidth>1400){
        this.drawingBoard?.lineTo(event.clientX-(sectionLength+(centerGaps/2)),event.clientY-this.nav_height-this.paddingSize-this.paddingSize);
      }
      else{
        this.drawingBoard?.lineTo(event.clientX-sectionLength+9,event.clientY-this.nav_height-this.paddingSize-this.paddingSize);
      }
      this.drawingBoard?.stroke();
    }
  }
  fillAlgorihm(x:number,y:number){
    
    this.filling=true;
    let stack=[[x,y]]
    let image=this.drawingBoard?.getImageData(0,0,this.drawing_screen_width,this.drawing_screen_height);
    let startPos=(y*this.drawing_screen_width+x)*4;
    if(image?.data[startPos]===this.colorR && image?.data[startPos+1]===this.colorG && image?.data[startPos+2]==this.colorB){
      this.filling=false;
      return;
    }
    let colors=[image?.data[startPos],image?.data[startPos+1],image?.data[startPos+2],image?.data[startPos+3]]
    while(stack.length>0)
    {
      let posArr=stack.pop();
      let pos=(this.drawing_screen_width*posArr![1]+posArr![0])*4
      if(image?.data[pos]===colors[0] &&image?.data[pos+1]===colors[1] &&image?.data[pos+2]===colors[2] &&image?.data[pos+3]===colors[3])
      {
        image!.data[pos]=this.colorR;//r
        image!.data[pos+1]=this.colorG;//g
        image!.data[pos+2]=this.colorB;//b
        image!.data[pos+3]=255;
        if(posArr![0]<this.drawing_screen_width)
        {
          stack.push([posArr![0]+1,posArr![1]])
        }
        if(posArr![0]>0)
        {
          stack.push([posArr![0]-1,posArr![1]])
        }
        if(posArr![1]<this.drawing_screen_height)
        {
          stack.push([posArr![0],posArr![1]+1])
        }
        if(posArr![1]>0)
        {
          stack.push([posArr![0],posArr![1]-1])
        }
      } 
    }
    this.drawingBoard?.putImageData(image!,0,0)
    this.filling=false;
  }
  fill(){
    this.isFilling=!this.isFilling    
  }
  clear(){
    //this.drawingBoard?.clearRect(0, 0, this.canvas.nativeElement.width, this.canvas.nativeElement.height)
    this.drawingBoard!.fillStyle = "white";
    this.drawingBoard?.fillRect(0, 0,this.canvas.nativeElement.width, this.canvas.nativeElement.height);
    this.isFilling=false;
    this.isDrawing=false;
  }
  createDrawing(){
    this.canvas.nativeElement.toBlob(blob=>this.imageService.sendImage(blob!,this.nameForm.value.name).subscribe());
    this.toastr.success("Image Created");
      console.log("Image created");
      this.router.navigate(["/"]);
  }
  loadImage(){
    let drawing_screen_width=this.drawing_screen_width;
    let drawing_screen_height=this.drawing_screen_height;
    let img1 = new Image();
    let drawingBoard=this.drawingBoard;
    img1.onload = function () {
      drawingBoard?.drawImage(img1,0,0,drawing_screen_width,drawing_screen_height);
    }
    img1.crossOrigin ='Anonymous';
    img1.src =this.loadImageUrl.nativeElement.value;
  }
}
