import { RouterLink } from '@angular/router';
import { Component, OnInit, inject, signal } from '@angular/core';
import { ApiService, Project } from '../../core/api.service';

@Component({
  selector: 'app-projects',
  imports: [RouterLink],
  templateUrl: './projects.html',
  styleUrl: './projects.css',
})
export class Projects implements OnInit {
  private api = inject(ApiService);
  projects = signal<Project[]>([]);

  ngOnInit() {
    this.api.projects().subscribe(p => this.projects.set(p));
  }
}
