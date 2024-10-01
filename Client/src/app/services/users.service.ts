import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from '../models/User';
import { UserStatus } from '../enums/user-status';
import { AUTENTICATION_API } from '../environments/constants'

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  constructor(private http: HttpClient) { }

  public giveAdminRole(userId: string) : Observable<any> {
    return this.http.put(`${AUTENTICATION_API}users/admin-role?userId=${userId}`, {})
  }

  public denyAdminRole(userId: string) : Observable<any> {
    return this.http.delete(`${AUTENTICATION_API}users/admin-role?userId=${userId}`)
  }

  public changeUserStatus(userId: string, userStatus: UserStatus) : Observable<any> {
    return this.http.put(`${AUTENTICATION_API}users/status`, {
      "userId": userId,
      "status": userStatus
    })
  }

  public getUsers(page: number, count: number) : Observable<IUser[]> {
    return this.http.get<IUser[]>(`${AUTENTICATION_API}users?page=${page}&count=${count}`)
  }

  public getUserByUsername(username: string) : Observable<IUser> {
    return this.http.get<IUser>(`${AUTENTICATION_API}users/${username}`)
  }
}
