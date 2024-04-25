import {Component, inject, OnInit} from '@angular/core';
import {TweetDto} from "../../DTOs/Tweet.dto";
import {TweetService} from "../../services/tweet.service";
import {CommentService} from "../../services/comment.service";
import {Time} from "@angular/common";
import {retry, Timestamp} from "rxjs";
import {Tweet} from "../../interfaces/tweet.interface";
import {Comment} from "../../interfaces/comment.interface";
import {User} from "../../interfaces/user.interface";
import {CommentDto} from "../../DTOs/Comment.dto";

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
  private _commentService: CommentService = inject(CommentService);

  ngOnInit(): void {
    this.getTweets();
  }

  async getTweets() {
    await this._tweetService.getAllTweets().pipe(retry(3)).subscribe(tweets => {
      this.tweets = tweets.map(tweet => ({ ...tweet, comments: [] })).reverse();
    });
  }

  async getComments(tweetId: number) {
    await this._commentService.getAllComments(tweetId).subscribe(comments => {
      const index = this.tweets.findIndex(t => t.id === tweetId);
      if (index > -1) {
        this.tweets[index].comments = [...comments];  // Create a new array reference
      }
    }, error => {
      console.error('Failed to load comments:', error);
    });
  }



  toggleCommentBox(tweetId: number) {
    if (this.activeCommentBox === tweetId) {
      this.activeCommentBox = null;
    } else {
      this.activeCommentBox = tweetId;
      this.getComments(tweetId);
    }
  }

  likeTweet(tweetId: number) { }

  async postTweet() {
    if(this.body != '') {
      await this.addTweet(1, this.body);
      this.body = '';
    }
    else this.body = 'Write something interesting here!';
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

  async postComment(tweetId: number) {
    await this.addComment(tweetId);
    this.commentBody = '';
  }

  async addComment(tweetId: number) {
    if (this.commentBody.trim() === '') {
      console.error('Comment body is empty.');
      return;
    }

    const commentDto: CommentDto = {
      userId: 1, // assuming you have a way to get the current user's ID
      body: this.commentBody,
      tweetId: tweetId,
    };

    this._commentService.postComment(commentDto).subscribe({
      next: (comment) => {
        console.log('Comment posted:', comment);
        this.getComments(tweetId);
      },
      error: (error) => {
        console.error('Error posting comment:', error);
        this.getComments(tweetId);
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
