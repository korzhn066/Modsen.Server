import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { NzTableModule } from 'ng-zorro-antd/table';
import { IUser } from '../../models/User';
import { PageTitleComponent } from "../../components/page-title/page-title.component";
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule } from '@angular/forms';
import { UserStatus } from '../../enums/user-status';

@Component({
  selector: 'app-users-page',
  standalone: true,
  imports: [
    NzTableModule,
    PageTitleComponent,
    NzButtonModule,
    NzInputModule,
    FormsModule
],
  templateUrl: './users-page.component.html',
  styleUrl: './users-page.component.css'
})
export class UsersPageComponent implements OnInit {
  constructor (private usersService: UsersService) { }

  username: string = ''
  userId: string = ''
  users: IUser[] = []
  user?: IUser

  denyAdminRole(): void {
    this.usersService.denyAdminRole(this.userId).subscribe({
      next: () => this.getUsers(1, 10),
      error: error => console.log(error)
    })
  }

  giveAdminRole(): void {
    this.usersService.giveAdminRole(this.userId).subscribe({
      next: () => this.getUsers(1, 10),
      error: error => console.log(error)
    })
  }

  banUser(): void {
    this.usersService.changeUserStatus(this.userId, UserStatus.Ban).subscribe({
      next: () => this.getUsers(1, 10),
      error: error => console.log(error)
    })
  }

  unbanUser(): void {
    this.usersService.changeUserStatus(this.userId, UserStatus.Unban).subscribe({
      next: () => this.getUsers(1, 10),
      error: error => console.log(error)
    })
  }

  getUserByUsername(): void {
    if (this.username == '') {
      return
    }

    this.usersService.getUserByUsername(this.username).subscribe({
      next: (user: IUser) => {
        this.user = user
      }
    })
  }

  getUsers(page: number, count: number) {
    this.usersService.getUsers(page, count).subscribe({
      next: (users: IUser[]) => this.users = users,
      error: (error) => console.log(error) 
    })
  }

  ngOnInit() {    
    this.getUsers(1, 10)
  }
}
