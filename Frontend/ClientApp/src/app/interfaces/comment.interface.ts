import {User} from "./user.interface";

export interface Comment {
  tweetId: number;
  userId: number;
  body: string;
}
