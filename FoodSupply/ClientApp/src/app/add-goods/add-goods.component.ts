import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-goods',
  templateUrl: './add-goods.component.html',
  styleUrls: ['./add-goods.component.css']
})
export class AddGoodsComponent implements OnInit {
  name: string;
  description: string;
  price: string;
  supplier: string;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  createGoods() {
    this.post("Goods", { name: this.name, description: this.description, price: this.price, supplier: this.supplier });
  }

  post(path: string, obj: object) {
    if (this.name === "" || this.name === null || this.name === undefined)
      return;

    this.http.post<Object>(location.origin + "/api/" + path, obj).subscribe(
      obj => {
        console.log(obj);
        alert("Success");
        location.reload();
      }, error => {
        console.error(error);
        alert("Error");
      });
  }

}
