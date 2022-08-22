import { Component, Input, OnInit } from '@angular/core';
import { IComment } from 'src/app/core/interfaces/IComment';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {
  @Input()comments!:Array<IComment>;
  @Input()commentPath:Array<string>=[];
  constructor() { }

  ngOnInit(): void {
  }

}
