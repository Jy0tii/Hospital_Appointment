import { Component } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { appointmentInterface } from '../../interfaces/appointmentInterface';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-drappointment',
  imports: [],
  templateUrl: './drappointment.component.html',
  styleUrl: './drappointment.component.css'
})
export class DrappointmentComponent {
  appointmentList: appointmentInterface[]=[];
  constructor(private apiservice: ApiService) {
  }

  ngOnInit() {
    this.apiservice.getAllAppointment().subscribe((response: appointmentInterface[]) => {
      console.log(response);
      this.appointmentList = response;
    })
  }

  async deleteAppt(id: number){
    const response = await firstValueFrom(this.apiservice.deleteAppointment(id));
    if(response == null){
      alert("appointment deleted")
      this.ngOnInit();
    }
      
  }
}
