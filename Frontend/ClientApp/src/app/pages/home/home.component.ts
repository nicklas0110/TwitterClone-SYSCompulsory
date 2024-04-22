import {Component, inject, OnInit} from '@angular/core';
import {TweetDto} from "../../DTOs/Tweet.dto";
import {TweetService} from "../../services/tweet.service";
import {Time} from "@angular/common";
import {retry, Timestamp} from "rxjs";
import {Tweet} from "../../interfaces/tweet.interface";
import {Comment} from "../../interfaces/comment.interface";
import {User} from "../../interfaces/user.interface";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  body: string = '';
  commentBody: string = '';
  activeCommentBox: number | null = null;
  tweets = <Tweet[]>([]);
  suggestedUsers: User[] = [
    {
      id: 1,
      name: "Jane Doe",
      username: "janedoe",
      profilePicture: "./assets/user-icon.webp",
      email: "janedoe@gmail.com",
      description: "I'm a cool person"
    },
    {
      id: 2,
      name: "John Doe",
      username: "johndoe",
      profilePicture: "./assets/user-icon.webp",
      email: "johndoe@gmail.com",
      description: "I'm a cool person too"
    },

  ];

  private _tweetService: TweetService = inject(TweetService);

  ngOnInit(): void {
    this.getTweets();
  }

  async getTweets() {
    await this._tweetService.getAllTweets().pipe(retry(3)).subscribe(tweets => {
      this.tweets = tweets.map(tweet => ({ ...tweet, comments: [] })).reverse();
    });
  }

  toggleCommentBox(tweetId: number) {
    this.activeCommentBox = this.activeCommentBox === tweetId ? null : tweetId;
  }

  async postComment(tweetId: number) {
    const comment: Comment = {
      tweetId: tweetId,
      userId: 1,
      body: this.commentBody,
    };

    // Placeholder for backend integration
    console.log('Posting comment:', comment);
    this.commentBody = '';

    const index = this.tweets.findIndex(t => t.id === tweetId);
    if (index > -1) {
      this.tweets[index].comments.push(comment);
    }
  }

  likeTweet(tweetId: number) { }

  async postTweet() {

    await this.addTweet(1, this.body);
    this.body = '';
  }

  async addTweet(userId: number, body: string) {
    const tweetDto: TweetDto = {
      userId: userId,
      body: body,
    };

    await this._tweetService.postTweet(tweetDto).subscribe({
      next: (tweet) => {
        console.log('Tweet tweeted:', tweet);
        this.getTweets();
      },
      error: (error) => {
        console.error('Error posting tweet:', error);
      }
    });
  }

  getSuggestedUsers() {
    //this._userService.getSuggestedUsers()
      //.subscribe(users => this.suggestedUsers = users);
  }

  followUser(userId: number) {
    console.log('Following user:', userId);
    //TODO follow logic here
  }

  formatDate(date: string) {
    const formatedDate = new Date(date);
    return formatedDate.toString().slice(0, -41);
  }

}
