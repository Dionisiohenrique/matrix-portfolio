import { Component, OnInit, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../core/api.service';

interface SkillRow { name: string; level: number; category: string; }

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  private api = inject(ApiService);
  profile: Record<string, string> = {};
  skills: SkillRow[] = [];

  ngOnInit() {
    this.api.profile().subscribe(p => (this.profile = p));
    this.api.skills().subscribe(s => (this.skills = s));
  }
}
