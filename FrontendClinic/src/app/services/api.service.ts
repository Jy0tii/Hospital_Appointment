import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { appointmentInterface } from '../interfaces/appointmentInterface';
import { Observable, tap } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  url: string = "https://localhost:44333/api";
  token = localStorage.getItem('token');

  constructor(private http: HttpClient) { }

  //Appointment
  getAllAppointment(): Observable<appointmentInterface[]> {
    return this.http.get<appointmentInterface[]>(`${this.url}/appointment`);
  }
  getByIdAppointment(id: number) {
    return this.http.get(`${this.url}/appointment/${id}`);
  }

  getUserAppointment(){
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get(`${this.url}/appointment/userAppointments`, {headers});
  }

  getUserApptAdmin(id: string){
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get(`${this.url}/admin/userAppointments`, {params: {id: id}, headers: headers});
  }
  addAppointment(data: appointmentInterface): Observable<appointmentInterface> {
    if(this.token != null) {
      var decodeToken = jwtDecode<any>(this.token);
    }
    data.userId = decodeToken.Id;
    data.name = decodeToken.Name;
    data.mobileNo = decodeToken.PhoneNumber;
    console.log("decd", decodeToken)
    console.log("id2 is",decodeToken.Name);
    return this.http.post<appointmentInterface>(`${this.url}/appointment`, data);
  }
  updateAppointment(id: number, data: appointmentInterface) {
    return this.http.put(`${this.url}/appointment/${id}`, data);
  }
  deleteAppointment(id: number) {
    return this.http.delete(`${this.url}/appointment/${id}`);
  }


  //contact Us
  addContactUs(data: any){
    console.log("ji")
    return this.http.post(`${this.url}/user`, data);
  }

  getContactUs(){
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get(`${this.url}/admin/userContactUs`, {headers});
  }

  deleteContactUs(id: string){
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.delete(`${this.url}/admin/userContactUs/${id}`, {headers});
  }
}
