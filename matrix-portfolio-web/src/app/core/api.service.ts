import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

export interface Project {
  id: number;
  title: string;
  description: string;
  imageUrl?: string;
  repoUrl?: string;
  liveUrl?: string;
  tagsCsv: string;
  isPublished: boolean;
  displayOrder: number;
}

export interface Skill {
  id: number;
  name: string;
  level: number;
  category: string;
  displayOrder: number;
}

export interface Message {
  id: number;
  name: string;
  email: string;
  body: string;
  isRead: boolean;
  createdAt: string;
}

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  // Set at build time for production; falls back to the dev API.
  private base = ''; // same-origin by default; override in environments or use a proxy

  projects(all = false): Observable<Project[]> {
    return this.http.get<Project[]>(`/api/projects${all ? '?all=true' : ''}`);
  }
  saveProject(p: Partial<Project>): Observable<Project> {
    return p.id ? this.http.put<Project>(`/api/projects/${p.id}`, p) : this.http.post<Project>('/api/projects', p);
  }
  deleteProject(id: number) { return this.http.delete(`/api/projects/${id}`); }

  skills(): Observable<Skill[]> { return this.http.get<Skill[]>('/api/skills'); }
  saveSkill(s: Partial<Skill>): Observable<Skill> {
    return s.id ? this.http.put<Skill>(`/api/skills/${s.id}`, s) : this.http.post<Skill>('/api/skills', s);
  }
  deleteSkill(id: number) { return this.http.delete(`/api/skills/${id}`); }

  profile(): Observable<Record<string, string>> { return this.http.get<Record<string, string>>('/api/profile'); }
  setProfile(key: string, value: string) { return this.http.put(`/api/profile/${key}`, { value }); }

  sendMessage(m: { name: string; email: string; body: string }) { return this.http.post('/api/messages', m); }
  messages(): Observable<Message[]> { return this.http.get<Message[]>('/api/messages'); }
  markRead(id: number) { return this.http.put(`/api/messages/${id}/read`, {}); }
  deleteMessage(id: number) { return this.http.delete(`/api/messages/${id}`); }

  login(username: string, password: string): Observable<{ token: string }> {
    return this.http.post<{ token: string }>('/api/auth/login', { username, password })
      .pipe(tap(r => localStorage.setItem('matrix_token', r.token)));
  }
  logout() { localStorage.removeItem('matrix_token'); }
}
