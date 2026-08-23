import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit, OnDestroy {
  private timer: ReturnType<typeof setInterval> | null = null;

  // Digital rain rendered as text columns (cheap, no canvas needed for SSR-safety).
  rainColumns = signal<string[][]>([]);

  ngOnInit() {
    const glyphs = 'アイウエオカキクケコサシスセソ01<>{}[]#$%&*+=/\\'.split('');
    const build = () => {
      const cols = Math.max(10, Math.floor(window.innerWidth / 28));
      this.rainColumns.set(
        Array.from({ length: cols }, () =>
          Array.from({ length: 30 }, () => glyphs[Math.floor(Math.random() * glyphs.length)])
        )
      );
    };
    build();
    this.timer = setInterval(build, 180);
  }
  ngOnDestroy() { if (this.timer) clearInterval(this.timer); }
}
