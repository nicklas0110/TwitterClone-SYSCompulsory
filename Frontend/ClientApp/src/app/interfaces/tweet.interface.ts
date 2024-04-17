import {User} from "./user.interface";

export interface Tweet {
  id: number;
  user: number;
  body: string;
  timestamp: string;

  //Maybe remove these two when implementing comments interface in backend, because we use the ids instead
  likes: number;
  comments: Comment[];
}

