import { Component } from '@angular/core';
import { Router } from "@angular/router";

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
  username: string = '';
  email: string = '';
  password: string = '';
  firstName: string = '';
  lastName: string = '';
  error: string = '';
  loggedIn: boolean = false;

  constructor(private router: Router) {}

  navigateToPage(page: string) {
    this.router.navigate(['/' + page]);
  }

  async handleRegister(username: string, email: string, password: string, firstName: string, lastName: string) {
    try {
      //TODO: Implement registration logic here

      window.scrollTo(0, 0);

      // Successful registration
      this.navigateToPage('');
    } catch (error) {
      // Handle registration errors
      console.error('Registration error:', error);

      if ((error as any).code === !null) {
        this.error = 'Failed to register';
      }
    }
  }
}
