import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthState } from '../../../auth/presentation/state/auth.state';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './landing.page.html',
  styleUrls: ['./landing.page.css']
})
export class LandingPage implements OnInit {
  private readonly authState = inject(AuthState);
  private readonly router = inject(Router);

  ngOnInit(): void {
    if (this.authState.isAuthenticated()) {
      this.router.navigate(['/dashboard']);
    }
  }
}
