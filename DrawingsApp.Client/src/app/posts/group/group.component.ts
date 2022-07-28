import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { IPost } from 'src/app/core/interfaces/IPost';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  constructor(private activatedRoute:ActivatedRoute) { }
  group!:IGroup;
  posts:Array<IPost>=[];
  ngOnInit(): void {
    console.log();
    this.group=this.activatedRoute.snapshot.data['data'][0];
    this.posts=this.activatedRoute.snapshot.data['data'][1];
  }

}
