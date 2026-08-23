import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api.service';

@Component({
  selector: 'app-contact',
  imports: [FormsModule],
  templateUrl: './contact.html',
  styleUrl: './contact.css',
})
export class Contact {
  private api = inject(ApiService);
  name = '';
  email = '';
  body = '';
  sent = signal(false);
  error = signal('');

  send() {
    this.error.set('');
    this.api.sendMessage({ name: this.name, email: this.email, body: this.body }).subscribe({
      next: () => this.sent.set(true),
      error: () => this.error.set('Transmission failed. Try again.'),
    });
  }
}
