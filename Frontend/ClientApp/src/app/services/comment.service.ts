import {inject, Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {apiEndpoint} from "./constraint";
import {CommentDto} from "../DTOs/Comment.dto";
import {Comment} from "../interfaces/comment.interface";

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private _httpClient: HttpClient = inject(HttpClient);

  constructor() {

  }
  postComment(commentDto: CommentDto): Observable<Comment> {
    return this._httpClient.post<Comment>(`${apiEndpoint.CommentEndPoint.postComment}`, commentDto);
  }

  getAllComments(tweetId: number): Observable<Comment[]> {
    return this._httpClient.get<Comment[]>(`${apiEndpoint.CommentEndPoint.getComments}/${tweetId}`);
  }


}
