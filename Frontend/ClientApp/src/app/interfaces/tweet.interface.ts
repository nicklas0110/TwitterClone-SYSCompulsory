import {User} from "./user.interface";

export interface Tweet {
  id: number;
  body: string;
  timestamp: string;
  user: User;
  likes: number;
  comments: Comment[];
}
