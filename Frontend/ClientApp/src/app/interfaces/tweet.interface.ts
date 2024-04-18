import {User} from "./user.interface";

export interface Tweet {
  id: number;
  userId: number;
  body: string;
  createdAt: string;
}

