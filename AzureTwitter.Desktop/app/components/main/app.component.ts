import { Component, NgZone } from '@angular/core';

import TweetService from '../../services/tweet.service';
import TweetHub from '../../hubs/tweet.hub'; 
import Tweet from '../../models/tweet.model'

@Component({
	selector: 'app',
	templateUrl: './app.component.html'
})
export default class AppComponent {

	private tweetsByUser = new Map<string, Tweet>();
	private tweets: Tweet[] = [];

	constructor(private tweetService: TweetService, private tweetHub: TweetHub, private zone: NgZone) { }

	ngOnInit() {
		this.tweetHub.newTweet.subscribe((tweet: Tweet) => {

			let isUpdated = false;

			if (!this.tweetsByUser.has(tweet.user)) {
				isUpdated = true;
				this.tweetsByUser.set(tweet.user, tweet);
			} else {
				const oldTweet = this.tweetsByUser.get(tweet.user);

				const oldDate = new Date(oldTweet.created);
				const newDate = new Date(tweet.created);

				if (oldDate.getTime() < newDate.getTime()) {
					isUpdated = true;
					this.tweetsByUser.set(tweet.user, tweet);
				}
			}

			if (isUpdated) {
				this.zone.run(() => {
					this.tweets = Array.from(this.tweetsByUser.values());
				});
			}
		});
	}
}
