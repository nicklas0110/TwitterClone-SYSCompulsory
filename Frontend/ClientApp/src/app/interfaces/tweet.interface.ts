import {User} from "./user.interface";
import {Comment} from "./comment.interface";

export interface Tweet {
  id: number;
  userId: number;
  body: string;
  comments: Comment[];
  createdAt: string;
}

