import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'

import { jwtDecode } from "jwt-decode"
import { Observable } from 'rxjs'

import { AUTENTICATION_API } from '../environments/constants'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public isAuthenticated: boolean = false
  public isAdmin: boolean = false
  public username: string = ''

  constructor(private http: HttpClient) {
    if (typeof window !== 'undefined') {
      this.setAuthParams()
    }
    
  }

  public refreshToken(): Observable<string> {
    return this.http.get(`${AUTENTICATION_API}tokens`, {
      responseType: 'text',
      withCredentials: true
    })
  }

  public login(username: string, password: string) {
    this.http.post(`${AUTENTICATION_API}authentication/login`, {
      password: 'Admin1_admin',
      userName: 'admin'
    }, {
      responseType: 'text',
      withCredentials: true
    }).subscribe({
      next:(token: any) => {
        localStorage.setItem('token', token)

        this.setAuthParams()
      },
      error: error => console.log(error)
    });
  }

  public register() {
    
  }

  public logOut() {
    localStorage.removeItem('token')
    this.isAuthenticated = false
    this.isAdmin = false
    this.username = ''
  }

  private setAuthParams() {
    const token = localStorage.getItem('token')

    if (!token){
      return
    }

    const jwtJson = JSON.stringify(jwtDecode(token))
    const roles = JSON.parse(jwtJson)['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    
    this.username = JSON.parse(jwtJson)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']

    for (let role of roles) {
      if (role == 'Admin') {
        this.isAdmin = true
        break
      }
    }

    this.isAuthenticated = true
  }
}
