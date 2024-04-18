const tweetUrl = 'http://localhost:9090/api';
const other1Url = 'http://localhost:8083/api';
const other2Url = 'http://localhost:8085/api';

export const apiEndpoint = {
  TweetEndPoint: {
    getTweets: `${tweetUrl}/Tweet/GetTweets`,
    postTweet: `${tweetUrl}/Tweet/PostTweet`,
    deleteTweet: `${tweetUrl}/Tweet/DeleteTweet`,
  },
  Other1EndPoint: {
    subtraction: `${other1Url}/Other`
  },
  Other2EndPoint: {
    getOther1: `${other2Url}/Other`,
    getOther2: `${other2Url}/`,
    addOther: `${other2Url}/Other/AddOther`,
  }
}
