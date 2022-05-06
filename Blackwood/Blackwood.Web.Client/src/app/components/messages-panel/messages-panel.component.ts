import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'bw-messages-panel',
  host: {'class': 'bw-messages-panel'},
  templateUrl: './messages-panel.component.html'
})
export class MessagesPanelComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
