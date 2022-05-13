import {Directive, ElementRef, HostBinding, Renderer2} from '@angular/core';

@Directive({
  selector: '[bw-outlined-button]'
})
export class OutlinedButtonDirective {

  constructor(private renderer: Renderer2, hostElement: ElementRef) {
    renderer.addClass(hostElement.nativeElement, 'bw-button');
    renderer.addClass(hostElement.nativeElement, 'bw-button-outlined');
  }

}
