import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '../_models/user';
import { environment } from '../../../src/environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    // getAll() {
    //     return this.http.get<User[]>(`${environment.host}/users`);
    // }

    getById(id: number) {
        return this.http.get(`${environment.host}/users/${id}`);
    }

    register(user: User) {
        return this.http.post(`${environment.host}api/account/Register`, user);
    }

    // update(user: User) {
    //     return this.http.put(`${environment.host}/users/${user.id}`, user);
    // }

    // delete(id: number) {
    //     return this.http.delete(`${environment.host}/users/${id}`);
    // }
}