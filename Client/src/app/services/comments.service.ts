import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { ICommentResponse } from '../models/CommentResponse';
import { IAddComment } from '../models/AddComment';
import { isPlatformBrowser } from '@angular/common';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { IComment } from '../models/Comment';
import { CARS_ELECTIONS_API } from '../environments/constants';
import { IUpdateComment } from '../models/UpdateComment';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {
  private hubConnection?: signalR.HubConnection

  constructor(
    @Inject(PLATFORM_ID) private platformId: Object,
    private authService: AuthService,
    private http: HttpClient

  ) 
  {
    this.initHub()
  }

  private initHub() {
    if (isPlatformBrowser(this.platformId)) {
      this.authService.refreshToken().subscribe({
        next: token => {
          localStorage.setItem('token', token)
          
          this.hubConnection = new signalR.HubConnectionBuilder()
            .withAutomaticReconnect()
            .withUrl('https://localhost:7018/hubs/comments', {
              accessTokenFactory: () => {
                return token
              },  
            })
            .build();
        }
      })
    }
  }

  public startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
      this.hubConnection!
        .start()
        .then(() => {
          console.log('Connection established with SignalR hub');
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error()
        });
    });
  }

  public receiveComment(): Observable<IComment> {
    return new Observable<IComment>((observer) => {
      this.hubConnection!.on('CommentReceive', (comment: IComment) => {
        observer.next(comment);
      });
    });
  }

  public addComment(addComment: IAddComment): void {
    this.hubConnection!.invoke('AddComment', addComment);
  }

  public deleteComment(id: number): void {
    this.hubConnection!.invoke('DeleteComment', id);
  }

  public updateComment(updateComment: IUpdateComment): void {
    this.hubConnection!.invoke('UpdateComment', updateComment);
  }

  public getComment(carId: string): Observable<IComment> {
    return this.http.get<IComment>(`${CARS_ELECTIONS_API}comments?carId=${carId}`)
  }

  public getComments(carId: string, page: number, count: number): Observable<IComment[]> {
    return this.http.get<IComment[]>(`${CARS_ELECTIONS_API}cars/${carId}/comments?page=${page}&count=${count}`)
  }
}
