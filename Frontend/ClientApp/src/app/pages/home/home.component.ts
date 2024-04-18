import {Component, inject} from '@angular/core';
import {TweetDto} from "../../DTOs/Tweet.dto";
import {TweetService} from "../../services/tweet.service";
import {Time} from "@angular/common";
import {Timestamp} from "rxjs";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  body: string = '';

  private _tweetService: TweetService = inject(TweetService);

  async postTweet() {

    await this.addTweet(1, this.body);
    this.body = '';
    //TODO this.getTweets();
  }

  async addTweet(userId: number, body: string) {
    const tweetDto: TweetDto = {
      userId: userId,
      body: body,
    };

    await this._tweetService.postTweet(tweetDto).subscribe({
      next: (tweet) => {
        console.log('Tweet tweeted:', tweet);
        //TODO this.getTweets();
      },
      error: (error) => {
        console.error('Error posting tweet:', error);
      }
    });
  }

}
