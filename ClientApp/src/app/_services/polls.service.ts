import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';


@Injectable({ providedIn: 'root' })
export class PollsService {

  constructor(private http: HttpClient) { }

  // list() {
  //   return this.http.get(`${environment.host}/api/polls/candidates`);
  // }

  get() {
    return this.http.get(`${environment.host}/api/polls/candidates`);
  }

  // create(poll) {
  //   return this.http.post(`${environment.host}/api/polls`, poll);
  // }

  delete(id) {
    return this.http.delete(`${environment.host}/api/polls/${id}`);
  }

  vote(pollId, option) {
    return this.http.post(`${environment.host}/api/polls/vote/${pollId}`, option);
  }

}