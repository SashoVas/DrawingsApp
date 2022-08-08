import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { GroupService } from '../services/group.service';

@Component({
  selector: 'app-popular-groups',
  templateUrl: './popular-groups.component.html',
  styleUrls: ['./popular-groups.component.css']
})
export class PopularGroupsComponent implements OnInit {

  constructor(private groupService:GroupService) { }
  topGroups:Array<IGroup>=[];
  yourGroups:Array<IGroup>=[]
  ngOnInit(): void {
    this.groupService.getTopGroups().subscribe(data=>this.topGroups=data);
    this.groupService.getGroupsByUser().subscribe(data=>this.yourGroups=data)
  }

}
