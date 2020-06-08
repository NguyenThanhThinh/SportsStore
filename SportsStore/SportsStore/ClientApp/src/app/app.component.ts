import { Component } from '@angular/core';
import { Repository } from './models/repository';
import { Product } from './models/product.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  constructor(private repo: Repository) {

  }

  getProduct(): Product {

    return this.repo.product;
  }
}
