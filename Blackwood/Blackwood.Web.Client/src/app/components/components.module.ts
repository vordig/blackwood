import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MaterialModule} from "../material-module";
import {MessagesPanelComponent} from './messages-panel/messages-panel.component';


@NgModule({
  declarations: [
    MessagesPanelComponent
  ],
  imports: [
    CommonModule,
    MaterialModule
  ],
  exports: [
    MessagesPanelComponent
  ]
})
export class ComponentsModule {
}
