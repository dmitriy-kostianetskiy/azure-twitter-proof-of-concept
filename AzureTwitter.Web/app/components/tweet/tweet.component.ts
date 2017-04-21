import {
	Component,
	Input
} from '@angular/core';
import Tweet from '../../models/tweet.model';

@Component({
	selector: 'tweet',
	templateUrl: './tweet.component.html',
	styleUrls: ['tweet.component.scss']
})
export default class TweetComponent {
	@Input()
	tweet: Tweet;
}
