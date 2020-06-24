
import { environment } from './../../environments/environment';
import { Product } from "./product.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Filter } from './configClasses.repository';
import { Supplier } from './supplier.model';

@Injectable()
export class Repository {

  product: Product;
  products: Product[];
  suppliers: Supplier[];
  filter: Filter = new Filter();
  constructor(private http: HttpClient) {
    //this.filter.category = "soccer";
    this.filter.related = true;
    this.getProducts();
  }

  getProduct(id: number) {


    this.http.get<Product>(`${environment.baseUrL}/api/products/${id}`)
      .subscribe(p => this.product = p);
  }
  getProducts() {
    let url = `${environment.baseUrL}/api/products?related=${this.filter.related}`;
    console.log(url);
    if (this.filter.category) {
      url += `&category=${this.filter.category}`;
    }
    if (this.filter.search) {
      url += `&search=${this.filter.search}`;
    }
    this.http.get<Product[]>(url).subscribe(prods => this.products = prods);
  }

  getSupplier() {
    this.http.get<Supplier[]>(`${environment.baseUrL}/api/suppliers`)
      .subscribe(p => this.suppliers = p);
  }

  createProduct(product: Product) {
    let data = {
      name: product.name, category: product.category,
      description: product.description, price: product.price,
      supplier: product.supplier ? product.supplier.supplierId : 0
    };

    this.http.post<number>(`${environment.baseUrL}/api/products`, data).
      subscribe(id => {
        product.productId = id;
        this.products.push(product);
      });

  }
  createProductAndSupplier(product: Product, supplier: Supplier) {
    let data = {
      name: supplier.name, city: supplier.city, state: supplier.state
    }

    this.http.post<number>(`${environment.baseUrL}/api/suppliers`, data).
      subscribe(id => {
        supplier.supplierId = id;
        this.suppliers.push(supplier);

        if (product != null) this.createProduct(product);
      });

  }
  replaceProduct(product: Product) {
    let data = {
      name: product.name, category: product.category,
      description: product.description, price: product.price,
      supplier: product.supplier ? product.supplier.supplierId : 0
    };

    this.http.put(`${environment.baseUrL}/api/products/${product.productId}`, data).
   subscribe(() => this.getProducts());

  }
  replaceSupplier(supplier: Supplier) {
    let data = {
      name: supplier.name, city: supplier.city, state: supplier.state
    }

     this.http.put(`${environment.baseUrL}/api/suppliers/${supplier.supplierId}`, data).
       subscribe(() => this.getProducts());

  }
}
