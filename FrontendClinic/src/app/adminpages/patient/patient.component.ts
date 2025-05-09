import { Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient',
  imports: [CommonModule],
  templateUrl: './patient.component.html',
  styleUrl: './patient.component.css'
})
export class PatientComponent {

  patientList: any;

  constructor(private authService: AuthService, private router: Router){}
  ngOnInit(){
    this.authService.getAllPatient().subscribe((response: any)=>{
      console.log(response);
      this.patientList = response});
  }
  
  viewPatient(id: string){
    console.log("hi")
    this.router.navigate(['/profile',id]);
  }

  async deletePatient(id: string){
    const response = await firstValueFrom(this.authService.deletePatient(id));
      console.log(response);
      this.ngOnInit();
    }
}
