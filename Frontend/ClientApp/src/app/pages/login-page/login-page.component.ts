import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  email: string = '';
  password: string = '';
  error: string = '';
  loggedIn: boolean = false;

  constructor( private router: Router) {}

  navigateToPage(page: string) {
    this.router.navigate(['/' + page]);
  }

  async handleLogin(email: string, password: string) {
    try {

      //TODO: Implement login logic here

      window.scrollTo(0, 0);

      // Successful login
      this.navigateToPage('admin-panel/admin');
    } catch (error) {
      // Handle login errors
      console.error('Login error:', error);

      if ((error as any).code != null) {
        this.error = 'Wrong Email or password';
      }
    }
  }
}
