import {
	Component,
	Input
} from '@angular/core';
import {
	trigger,
	state,
	style,
	animate,
	transition,
	keyframes
} from '@angular/animations';

import Tweet from '../../models/tweet.model';


@Component({
	selector: 'tweet',
	templateUrl: './tweet.component.html',
	styleUrls: ['tweet.component.scss'],
	animations: [
		trigger('contentUpdated', [
			transition('* => *', [
				animate(300, keyframes([
					style({ opacity: 0, offset: 0 }),
					style({ opacity: 1, offset: 1.0 })
				]))
			])
		])
	]
})
export default class TweetComponent {
	@Input()
	tweet: Tweet;
}
