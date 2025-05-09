import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginCheck: boolean = false;
  constructor(private authService: AuthService, private router: Router) { }

  async logIn(login: any) {
    try {
      const response: any = await firstValueFrom(this.authService.userLogin(login));
      this.authService.getRole();
      this.router.navigate(['/home']);
    }
    catch (error: any) {
      if (error.error && error.error.message) {
        console.log(error.error.message)
        // this.errorMessage = error.error.message;
      } else {
        console.log("Server error")
        this.loginCheck = true;
        // this.errorMessage = 'Server error';
      }
    }
  }
}
