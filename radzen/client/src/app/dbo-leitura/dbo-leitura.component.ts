import { Component, Injector } from '@angular/core';
import { DboLeituraGenerated } from './dbo-leitura-generated.component';

@Component({
  selector: 'page-dbo-leitura',
  templateUrl: './dbo-leitura.component.html'
})
export class DboLeituraComponent extends DboLeituraGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
