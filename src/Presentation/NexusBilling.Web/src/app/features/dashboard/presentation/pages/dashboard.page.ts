import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: '<h2>Bienvenido al ERP NexusBilling</h2><p>Selecciona un módulo en el menú lateral para comenzar.</p>'
})
export class DashboardPage {}
