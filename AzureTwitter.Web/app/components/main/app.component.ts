import { Component, NgZone } from '@angular/core';

import TweetService from '../../services/tweet.service';
import TweetHub from '../../hubs/tweet.hub'; 
import Tweet from '../../models/tweet.model'

@Component({
	selector: 'app',
	templateUrl: './app.component.html'
})
export default class AppComponent {
	
	private lastTweet: Tweet;
	private updated: boolean = false;

	constructor(private tweetService: TweetService, private tweetHub: TweetHub, private zone: NgZone) { }

	ngOnInit() {
		this.tweetHub.newTweet.subscribe((tweet: Tweet) => {
			this.zone.run(() => {
				this.lastTweet = tweet;

				this.updated = true;
				setTimeout(() => {
					this.updated = false;
				}, 100);
			});
		});
	}
}
