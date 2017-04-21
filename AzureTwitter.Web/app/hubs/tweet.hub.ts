import Tweet from '../models/tweet.model';

import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs/Rx';

@Injectable()
export default class TweetHub {
	private hub: string = HUB + 'signalr';

	private newTweetSubject = new Subject<Tweet>();
	private timer;
	private counter: Number = 1;
	
	private tweetHub: any;
	
	constructor() {
		this.connect();
	}

	connect() {
		$.connection.hub.url = this.hub;

		const connection = $.hubConnection(this.hub);
		const tweetHub = connection.createHubProxy('tweetHub');

		tweetHub.on('newTweet', (tweet) => {
			this.newTweetSubject.next(<Tweet>tweet);
		});

		connection.start()
			.done(() => { console.log('Now connected, connection ID=' + connection.id); })
			.fail(() => { console.log('Could not connect'); });
	}

	get newTweet(): Subject<Tweet> {
		return this.newTweetSubject;
	};
}