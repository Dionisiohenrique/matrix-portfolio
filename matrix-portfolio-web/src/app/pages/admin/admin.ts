import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe, NgIf } from '@angular/common';
import { ApiService, Message, Project } from '../../core/api.service';
import { getToken, setToken } from '../../core/auth.interceptor';

@Component({
  selector: 'app-admin',
  imports: [FormsModule, DatePipe, NgIf],
  templateUrl: './admin.html',
  styleUrl: './admin.css',
})
export class Admin {
  private api = inject(ApiService);

  loggedIn = signal(!!getToken());
  loginError = signal('');
  username = '';
  password = '';

  tab = signal<'projects' | 'messages'>('projects');

  projects = signal<Project[]>([]);
  messages = signal<Message[]>([]);

  // form model
  editing = signal<Partial<Project> | null>(null);

  login() {
    this.api.login(this.username, this.password).subscribe({
      next: r => { setToken(r.token); this.loggedIn.set(true); this.load(); },
      error: () => this.loginError.set('Wrong credentials. The door stays locked.'),
    });
  }

  logout() { setToken(null); this.loggedIn.set(false); }

  load() {
    if (!this.loggedIn()) return;
    this.api.projects(true).subscribe(p => this.projects.set(p));
    this.api.messages().subscribe(m => this.messages.set(m));
  }

  ngOnInit() { this.load(); }

  startNew() { this.editing.set({ title: '', description: '', tagsCsv: '', isPublished: true, displayOrder: (this.projects().length + 1) * 10 }); }
  edit(p: Project) { this.editing.set({ ...p }); }
  cancel() { this.editing.set(null); }

  save() {
    const p = this.editing();
    if (!p?.title) return;
    this.api.saveProject(p).subscribe(() => { this.editing.set(null); this.load(); });
  }

  remove(id: number) {
    this.api.deleteProject(id).subscribe(() => this.load());
  }

  toggleRead(m: Message) { this.api.markRead(m.id).subscribe(() => this.load()); }
  deleteMessage(id: number) { this.api.deleteMessage(id).subscribe(() => this.load()); }
}
