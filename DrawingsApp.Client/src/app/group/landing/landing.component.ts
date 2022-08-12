import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPost } from 'src/app/core/interfaces/IPost';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {

  posts:Array<IPost>=[];
  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.posts=this.activatedRoute.snapshot.data['postsData'];
  }

}
