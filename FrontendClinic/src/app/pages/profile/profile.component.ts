import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ApiService } from '../../services/api.service';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  // standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

  patientData: any = {};
  patientAppointment: any;

  constructor(private authService: AuthService, private apiservice: ApiService, private route: ActivatedRoute,) { }

  async ngOnInit() {
    var id = this.route.snapshot.paramMap.get('id');
    if (id) {
      console.log("id in profile is", id)
      const response = await firstValueFrom(this.authService.getByIdPatient(id));
      this.patientData = response;
      console.log("id res",response)

      const nextResponse = await firstValueFrom(this.apiservice.getUserApptAdmin(id));

      console.log("appoi2", nextResponse);
      this.patientAppointment = nextResponse;

    }
    else {
      const data = this.authService.getRole();
      if (data) {
        console.log("hi")
        const response = await firstValueFrom(this.authService.getByIdPatient(data[1]));
        this.patientData = response;
        console.log(response)

        const nextResponse = await firstValueFrom(this.apiservice.getUserAppointment());
        this.patientAppointment = nextResponse;
        console.log("appoi1", nextResponse);
      }
    }
  }
}
