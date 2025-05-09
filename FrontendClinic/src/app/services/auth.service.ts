import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom, Observable, tap } from 'rxjs';
import { appointmentInterface } from '../interfaces/appointmentInterface';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  url: string = "https://localhost:44333/api/auth";
  token = localStorage.getItem('token');
  headers = new HttpHeaders({
    'Authorization': `Bearer ${this.token}`
  });


  constructor(private http: HttpClient) { }

  //user 
  getAllPatient(): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    // console.log("header is", `${this.url}/auth`, { headers });
    return this.http.get(this.url, { headers });
  }
  getByIdPatient(id: string) {
    return this.http.get(`${this.url}/${id}`, { headers: this.headers });
  }

  checkUserExist(data: string): Observable<appointmentInterface> {
    return this.http.get<appointmentInterface>(`${this.url}/register/${data}`)
  }
  addPatient(data: appointmentInterface): Observable<appointmentInterface> {
    console.log(this.url, data);
    return this.http.post<appointmentInterface>(`${this.url}/register`, data);
  }
  updatePatient(id: number, data: appointmentInterface) {
    return this.http.put(`${this.url}/patient/${id}`, data);
  }

  deletePatient(id: string) {
    return this.http.delete(`${this.url}/${id}`, { headers: this.headers });
    // return this.http.delete(`${this.url}/auth/${id}`, { headers });
  }

  //login
  userLogin(data: any) {
    return this.http.post<{ token: string }>(`${this.url}/login`, data).pipe(tap(response => {
      localStorage.setItem('token', response.token);
    }));
  }

  getRole() {
    var isRole: string | null = null;

    const token = localStorage.getItem('token');
    if (token == null) return null;

    const decodeToken = jwtDecode<any>(token);
    isRole = decodeToken.Role;
    return ([isRole, decodeToken.Id]);
  }
}

// headers: object =
// {
//   headers: new HttpHeaders({
//   'Authorization': `Bearer ${localStorage.getItem('token')}`})
// };

// headers:any = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);