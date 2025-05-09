import { Component } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-contact',
  imports: [],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {

  details: any;
  constructor(private apiService: ApiService) { }

  async ngOnInit() {
    const response = await firstValueFrom(this.apiService.getContactUs())
    if (response != null) {
      this.details = response;
    }
  }

  async deleteContact(id:string){
    const response = await firstValueFrom(this.apiService.deleteContactUs(id));
    if(response == null){
      this.ngOnInit();
    }
  }
}

