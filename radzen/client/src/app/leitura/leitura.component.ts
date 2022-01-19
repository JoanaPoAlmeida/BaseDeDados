import { Component, Injector } from '@angular/core';
import { LeituraGenerated } from './leitura-generated.component';

@Component({
  selector: 'page-leitura',
  templateUrl: './leitura.component.html'
})
export class LeituraComponent extends LeituraGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
