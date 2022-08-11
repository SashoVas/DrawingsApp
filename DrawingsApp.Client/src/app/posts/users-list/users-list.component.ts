import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUser } from 'src/app/core/interfaces/IUser';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  users!:Array<IUser>;
  constructor(private userService:UserService,private activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.userService.getUsers(this.activatedRoute.snapshot.params["id"],1).subscribe(data=>this.users=data);
  }

}
