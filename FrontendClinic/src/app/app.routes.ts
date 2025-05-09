import { Routes } from '@angular/router';
import { AppointmentComponent } from './pages/appointment/appointment.component';
import { HomeComponent } from './pages/home/home.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { ContactusComponent } from './pages/contactus/contactus.component';
import { PatientComponent } from './adminpages/patient/patient.component';
import { DrappointmentComponent } from './adminpages/drappointment/drappointment.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { ContactComponent } from './adminpages/contact/contact.component';

export const routes: Routes = [
    //for user
    {path: 'home', component: HomeComponent},
    {path: 'appointment', component: AppointmentComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'login', component: LoginComponent},
    {path: 'contact', component: ContactusComponent},
    {path: 'profile', component: ProfileComponent},
    //for admin
    {path: 'profile/:id', component: ProfileComponent},
    {path: 'patient', component: PatientComponent},
    {path: 'drappointment', component: DrappointmentComponent},
    {path: 'contactDetails', component: ContactComponent},
    //for inappropriate links
    {path: '**', component: HomeComponent},
];
