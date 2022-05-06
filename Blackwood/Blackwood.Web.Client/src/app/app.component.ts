import { Component } from '@angular/core';

@Component({
  selector: 'bw-root',
  host: {'class': 'bw-app'},
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'Blackwood';
}
