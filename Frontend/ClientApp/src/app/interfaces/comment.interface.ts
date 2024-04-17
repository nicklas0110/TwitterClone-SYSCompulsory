import {User} from "./user.interface";

export interface Comment {
  user: User;
  body: string;
}
