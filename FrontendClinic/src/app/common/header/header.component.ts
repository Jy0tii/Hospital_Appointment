import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  isLoggedIn: boolean = false;
  isRole: string | null = null;
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.router.events.subscribe((response: any) => {
      if (response.url && this.isRole == null) {
        const data: any = this.authService.getRole();
        if (data != null) {
          this.isRole = data[0];
          this.isLoggedIn = true;
        }
      }
    })
  }

  logout() {
    this.isLoggedIn = false;
    this.isRole = null;
    localStorage.removeItem('token');
    this.router.navigate(['/login'])
  }
}
