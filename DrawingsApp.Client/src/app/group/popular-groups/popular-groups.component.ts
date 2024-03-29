import { Component, OnInit } from '@angular/core';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { environment } from 'src/environments/environment';
import { GroupService } from '../services/group.service';

@Component({
  selector: 'app-popular-groups',
  templateUrl: './popular-groups.component.html',
  styleUrls: ['./popular-groups.component.css']
})
export class PopularGroupsComponent implements OnInit {
  topGroups:Array<IGroup>=[];
  yourGroups:Array<IGroup>=[]
  imagesUrl=environment.imageApi;
  constructor(private groupService:GroupService) { }
  
  ngOnInit(): void {
    this.groupService.getTopGroups(true).subscribe(data=>this.topGroups=data);
    this.groupService.getGroupsByUser(true).subscribe(data=>this.yourGroups=data)
  }

}
