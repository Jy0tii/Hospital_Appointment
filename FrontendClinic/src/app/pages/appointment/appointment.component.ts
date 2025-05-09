import { Component } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { appointmentInterface } from '../../interfaces/appointmentInterface';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-appointment',
  imports: [FormsModule, CommonModule],
  templateUrl: './appointment.component.html',
  styleUrl: './appointment.component.css'
})
export class AppointmentComponent {

  appointmentList: appointmentInterface[] = [];
  date: string = new Date().toISOString().split('T')[0];
  isLoggedIn: boolean = false;

  constructor(private apiservice: ApiService, private router: Router) {
  }

  ngOnInit(){
    if(localStorage.getItem('token')){ this.isLoggedIn = true;}
  }

  async addAppoint(appointment: appointmentInterface) {
    appointment.userId = '';
    appointment.name = '';
    appointment.mobileNo = '';
    console.log(appointment)
    // const response = await firstValueFrom(this.apiservice.addAppointment(appointment));
    // if (response) {
    //   alert("your appointment is booked successfully");
    // }
    // this.router.navigate(['/home']);
  }
}
