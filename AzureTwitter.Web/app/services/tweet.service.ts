import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';

import Tweet from '../models/tweet.model';
import * as def from '../definitions/const';

@Injectable()
export default class TweetService {

	private api: string = 'http://localhost:63200/api/tweets';

	constructor(private http: Http) { }

	getAll(): Observable<Tweet[]> {
		return this.http
			.get(this.api)
			.map((data) => <Tweet[]>data.json());
	}

	get(id: string): Observable<Tweet> {
		return this.http.get(`${this.api}/${id}`)
			.map(response => <Tweet>response.json());
	}
}
