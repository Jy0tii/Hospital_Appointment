import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../../services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contactus',
  imports: [FormsModule],
  templateUrl: './contactus.component.html',
  styleUrl: './contactus.component.css'
})
export class ContactusComponent {

  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit() { }
  async contactData(data: any) {
    console.log(data)
    const response = await firstValueFrom(this.apiService.addContactUs(data))
    if (response != null) {
      alert(`your message is send successfully.`);
      window.location.reload();
    }
  }
}
