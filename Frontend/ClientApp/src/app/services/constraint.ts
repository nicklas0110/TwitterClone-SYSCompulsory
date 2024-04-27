const tweetUrl = 'http://localhost:9090/api';
const commentUrl = 'http://localhost:9092/api';
const userUrl = 'http://localhost:9090/api';

export const apiEndpoint = {
  TweetEndPoint: {
    getTweets: `${tweetUrl}/Tweet/`,
    postTweet: `${tweetUrl}/Tweet/`,
    deleteTweet: `${tweetUrl}/Tweet/DeleteTweet`,
  },
  CommentEndPoint: {
    getComments: `${commentUrl}/Comment`,
    postComment: `${commentUrl}/Comment/PostComment`,
  },
  UserEndPoint: {
    getOther1: `${userUrl}/User`,
    getOther2: `${userUrl}/User`,
  }
}
