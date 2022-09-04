import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { ImageService } from '../services/image.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit,AfterViewInit {
 constructor(private activatedRoute:ActivatedRoute,private imageService:ImageService,private fb:UntypedFormBuilder,private router:Router){
  }
  ngOnInit(): void {
    this.nameForm=this.fb.group({
      name:["",[Validators.required]]
    });
  }
  nameForm!:UntypedFormGroup;
  @ViewChild('loadImageUrl') private loadImageUrl!:ElementRef;
  @ViewChild('drawingBoard')
  private canvas!: ElementRef<HTMLCanvasElement>;
  private isDrawing:boolean=false;
  private drawingBoard: CanvasRenderingContext2D|null=null;
  private isFilling:boolean=false; 
  private filling:boolean=false;
  private paddingSize=15;
  private nav_height=60;
  private drawing_screen_size=700;
  colorR=255;
  colorG=1;
  colorB=1;
  lineWidth=10
  
  ngAfterViewInit(): void {
    this.drawingBoard=this.canvas!.nativeElement.getContext('2d');
    this.canvas.nativeElement.height=this.drawing_screen_size;
    this.canvas.nativeElement.width=this.drawing_screen_size;
    this.clear();
    this.activatedRoute.queryParamMap.subscribe(data=>{
      var img1 = new Image();
      let drawingBoard=this.drawingBoard;
      img1.onload = function () {
        drawingBoard?.drawImage(img1, 0, 0);
      }
      img1.src =environment.imageApi+'Images/'+data.get("image")+'.jpg';
      console.log(environment.imageApi+'Images/'+data.get("image")+'.jpg');
    });
  }
  startLine(event:MouseEvent){
    if (this.isFilling)
    {
      let sectionLength=window.innerWidth*0.25;
      let centerGaps=sectionLength*2 -this.drawing_screen_size;
      if(centerGaps<0)
      {
        centerGaps=0
      }
      this.fillAlgorihm(event.clientX-(sectionLength+(centerGaps/2)),event.clientY-this.nav_height-this.paddingSize-this.paddingSize);
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
      let sectionLength=window.innerWidth*0.25;
      let centerGaps=sectionLength*2 -this.drawing_screen_size;
      if(centerGaps<0)
      {
        centerGaps=0
      }
      //mousePos-(leftSection+(paddingBoard/2)),mousePos-nav-paddingBoard-paddingAll  
      this.drawingBoard?.lineTo(event.clientX-(sectionLength+(centerGaps/2)),event.clientY-this.nav_height-this.paddingSize-this.paddingSize);

      this.drawingBoard?.stroke();
    }
  }
  fillAlgorihm(x:number,y:number){
    
    this.filling=true;
    let stack=[[x,y]]
    let width=this.canvas.nativeElement.width;
    let height=this.canvas.nativeElement.height;
    let image=this.drawingBoard?.getImageData(0,0,width,height);
    let startPos=(y*width+x)*4;
    if(image?.data[startPos]===this.colorR && image?.data[startPos+1]===this.colorG && image?.data[startPos+2]==this.colorB){
      this.filling=false;
      return;
    }
    let colors=[image?.data[startPos],image?.data[startPos+1],image?.data[startPos+2],image?.data[startPos+3]]
    while(stack.length>0)
    {
      let posArr=stack.pop();
      let pos=(width*posArr![1]+posArr![0])*4
      if(image?.data[pos]===colors[0] &&image?.data[pos+1]===colors[1] &&image?.data[pos+2]===colors[2] &&image?.data[pos+3]===colors[3])
      {
        image!.data[pos]=this.colorR;
        image!.data[pos+1]=this.colorG;
        image!.data[pos+2]=this.colorB;
        image!.data[pos+3]=255;
        if(posArr![0]<width)
        {
          stack.push([posArr![0]+1,posArr![1]])
        }
        if(posArr![0]>0)
        {
          stack.push([posArr![0]-1,posArr![1]])
        }
        if(posArr![1]<height)
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
    this.canvas.nativeElement.toBlob(blob=>this.imageService.sendImage(blob!,this.nameForm.value.name).subscribe(()=>this.router.navigate(["/"])));
  }
  loadImage(){
    let drawing_screen_size=this.drawing_screen_size;
    let img1 = new Image();
    let drawingBoard=this.drawingBoard;
    img1.onload = function () {
      drawingBoard?.drawImage(img1,0,0,drawing_screen_size,drawing_screen_size);
    }
    img1.src =this.loadImageUrl.nativeElement.value;
  }
}
