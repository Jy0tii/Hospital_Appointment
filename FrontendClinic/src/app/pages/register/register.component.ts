import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule, NgIf,],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  today: string = new Date().toISOString().split('T')[0];
  pass: string = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+\\-=\\[\\]{};:'\\\"\\\\|,.<>\\/?]).{6,}$"

  passwordVisible = false;
  userData: any;
  passwordData: any;

  constructor(private authService: AuthService, private router: Router) { }

  //get data from patient forms
  async formData(user: any) {
    
    const response = await firstValueFrom(this.authService.checkUserExist(user.Email));
    if(response.message == "email already exits"){
      alert(response.message)
    }
    else{
      this.userData = user;
      this.userData.DateOfBirth = this.userData.DateOfBirth.split('T')[0];
      this.passwordVisible = true;
    }
    // if(response.)
  }
  // add patient
  async formPassword(password: any) {
    this.userData.Password = password.Password;
    await firstValueFrom(this.authService.addPatient(this.userData));

    alert(`${this.userData.Name} yor are registered successfully!! redirect to login...`);
    this.router.navigate(['/login']);
    // setTimeout(()=>{
    // },2000);
  }
}
