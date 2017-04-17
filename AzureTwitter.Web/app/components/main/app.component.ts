import { Component, NgZone } from '@angular/core';

import TweetService from '../../services/tweet.service';
import TweetHub from '../../hubs/tweet.hub'; 
import Tweet from '../../models/tweet.model'

@Component({
	selector: 'app',
	templateUrl: './app.component.html'
})
export default class AppComponent {

	tweets: Tweet[] = [];

	constructor(private tweetService: TweetService, private tweetHub: TweetHub, private zone: NgZone) { }

	add() {
		this.tweets.push(<Tweet>{ content: "asdasd" });
	}

	ngOnInit() {
		this.tweetService
			.getAll()
			.subscribe((data: Tweet[]) => {
				this.tweets = data;
			});

		this.tweetHub.newTweet.subscribe((tweet: Tweet) => {
			this.zone.run(() => {
				this.tweets = [...this.tweets, tweet];
			});
		});
	}
}
