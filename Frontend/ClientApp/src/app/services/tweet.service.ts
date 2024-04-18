import {inject, Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {apiEndpoint} from "./constraint";
import {TweetDto} from "../DTOs/Tweet.dto";
import {Tweet} from "../interfaces/tweet.interface";

@Injectable({
  providedIn: 'root'
})
export class TweetService {

  private _httpClient: HttpClient = inject(HttpClient);

  constructor() {

  }
  postTweet(tweetDto: TweetDto): Observable<any> {
    return this._httpClient.post(`${apiEndpoint.TweetEndPoint.postTweet}/`, tweetDto, { responseType: 'text' });
  }

  getAllTweets(): Observable<Tweet[]> {
    return this._httpClient.get<Tweet[]>(`${apiEndpoint.TweetEndPoint.getTweets}`);
  }

}
