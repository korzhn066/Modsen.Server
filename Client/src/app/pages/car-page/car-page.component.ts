import { Component, Input, OnInit } from '@angular/core';
import { FullCardComponent } from '../../components/full-card/full-card.component';
import { AdminEditPanelComponent } from "../../components/admin-edit-panel/admin-edit-panel.component";
import { PageTitleComponent } from '../../components/page-title/page-title.component';
import { AuthService } from '../../services/auth.service';
import { CarType } from '../../enums/car-type';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CarFactoryService } from '../../services/car-factory.service';
import { ICar } from '../../models/Car';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';

import { formatDistance } from 'date-fns';
import { CommentsService } from '../../services/comments.service';
import { ICommentResponse } from '../../models/CommentResponse';
import { IComment } from '../../models/Comment';
import { CommentType } from '../../enums/comment-type';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-car-page',
  standalone: true,
  imports: [
    FullCardComponent,
    AdminEditPanelComponent,
    PageTitleComponent,
    NzCommentModule,
    NzButtonModule,
    NzFormModule,
    FormsModule
  ],
  templateUrl: './car-page.component.html',
  styleUrl: './car-page.component.css'
})
export class CarPageComponent implements OnInit{
  car?: ICar
  carType: CarType = CarType.Rent
  myComment?: IComment
  comments: IComment[] = []

  constructor(
    public authService: AuthService,
    private route: ActivatedRoute,
    private carFactoryService: CarFactoryService,
    private commentService: CommentsService
  ) {
    this.carType = route.snapshot.data['carType']
  }

  addComment() {
    this.commentService.addComment({
      carId: '66f43cc4854362ae98582f36',
      message: "message",
      createdAt: new Date(),
      commentType: CommentType.Positive
    })
  }

  updateComment() {
    if (this.myComment == null) {
      return
    }

    this.commentService.updateComment({
      commentId: this.myComment?.id,
      message: this.myComment.message
    })
  }

  deleteComment(id: number) {
    this.commentService.deleteComment(id)
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.carFactoryService.getCarService(this.carType).getCarById(params['id']).subscribe((car: ICar) => {
        this.car = car
      })

      this.commentService.getComments(params['id'], 0, 10).subscribe((comments: IComment[]) => {
        this.comments = comments
      })
      
      this.commentService.getComment(params['id']).subscribe((comment: IComment) => {
        this.myComment = comment
      })
    })

    this.commentService.startConnection().subscribe(() => {
      this.commentService.receiveComment().subscribe((comment: IComment) => {
        console.log(comment)
        this.comments.push(comment)
      });
    });
  }
}
