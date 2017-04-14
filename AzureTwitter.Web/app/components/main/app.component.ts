import { Component } from '@angular/core';

import TweetsService from '../../services/tweet.service';
import Tweet from '../../models/tweet.model'

@Component({
	selector: 'app',
	templateUrl: './app.component.html'
})
export default class AppComponent {

	tweets: Tweet[];

	constructor(private tweetsService: TweetsService) { }

	ngOnInit() {
		this.tweetsService
			.getAll()
			.subscribe((data: Tweet[]) => {
				this.tweets = data;
			});
	}
}
