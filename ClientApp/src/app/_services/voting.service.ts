import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Candidate } from '../_models/candidate';
import { environment } from '../../../src/environments/environment';

@Injectable({ providedIn: 'root' })
export class VotingService {
    constructor(private http: HttpClient) { }

    getAllCandidates() {
        return this.http.get<Candidate[]>(`${environment.host}/api/voting/getCandidates`);
    }

    // update(user: User) {
    //     return this.http.put(`${environment.host}/users/${user.id}`, user);
    // }

    // delete(id: number) {
    //     return this.http.delete(`${environment.host}/users/${id}`);
    // }
}