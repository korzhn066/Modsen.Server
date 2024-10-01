import { CommentType } from "../enums/comment-type";

export interface IAddComment {
    carId: string,
    message: string,
    createdAt: Date,
    commentType: CommentType
}