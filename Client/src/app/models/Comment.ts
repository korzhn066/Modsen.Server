import { CommentType } from "../enums/comment-type";

export interface IComment {
    id: number,
    message: string,
    dateTime: Date,
    type: CommentType
    likeCount: number,
    dislikeCount: number
}