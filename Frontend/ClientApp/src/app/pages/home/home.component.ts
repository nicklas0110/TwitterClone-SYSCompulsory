import {Component, inject} from '@angular/core';
import {TweetDto} from "../../DTOs/Tweet.dto";
import {TweetService} from "../../services/tweet.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  private _tweetService: TweetService = inject(TweetService);

  async postTweet(userId: number, body: string, createdAt: string) {
    const tweetDto: TweetDto = {
      userId: 1,
      body: body,
      createdAt: "now",
    };

    this._tweetService.postTweet(tweetDto).subscribe({
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
