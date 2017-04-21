import { Injectable } from '@angular/core';
import { Http, Response } from "@angular/http";
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';

import Tweet from '../models/tweet.model';


@Injectable()
export default class TweetService {

	private api: string = API + 'api/tweets';

	constructor(private http: Http) {
	}

	getAll(): Observable<Tweet[]> {
		return this.http
			.get(this.api)
			.map((data: Response) => <Tweet[]>data.json());
	}

	get(id: string): Observable<Tweet> {
		return this.http.get(`${this.api}/${id}`)
			.map((data: Response) => <Tweet>data.json());
	}
}
