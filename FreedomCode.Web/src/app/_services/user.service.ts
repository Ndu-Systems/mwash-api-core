import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models';
import { API_URL } from '../_config/config';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  API = API_URL;
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<User[]>(`${this.API}/users`);
  }

  getById(id: number) {
    return this.http.get<User>(`${this.API}/users/${id}`);
  }
}
