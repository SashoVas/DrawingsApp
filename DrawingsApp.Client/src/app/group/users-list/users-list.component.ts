import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUser } from 'src/app/core/interfaces/IUser';
import { UserService } from 'src/app/group/services/user.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  users!:Array<IUser>;
  role!:number;
  groupId!:number;
  constructor(private activatedRoute:ActivatedRoute,private userService:UserService) { }

  ngOnInit(): void {
    this.users=this.activatedRoute.snapshot.data['data'];
    this.groupId=this.activatedRoute.snapshot.queryParams["id"];
    this.userService.getRole(this.groupId).subscribe(data=>this.role=data);
  }
  acceptUser(userId:string){
    this.userService.acceptUser(this.groupId,userId).subscribe(()=>this.users=this.users.filter(u=>u.id!=userId));
  }
}
