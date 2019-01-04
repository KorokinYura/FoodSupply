import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-create-set',
  templateUrl: './create-set.component.html',
  styleUrls: ['./create-set.component.css']
})
export class CreateSetComponent implements OnInit {
  name: string;
  goods: Goods[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getGoods("Goods");
  }

  createSet() {
    let set: Goods[] = [];

    for (let i = 0; i < this.goods.length; i++) {
      if (this.goods[i].quantity > 0) {
        set.push(this.goods[i]);
      }
    }

    this.postSet("GoodsSets", { goods: set, name: this.name });
  }

  getGoods(path: string) {
    this.http.get(location.origin + "/api/" + path).subscribe(
      obj => {
        this.goods = <Goods[]>obj;
        //console.log(this.goods);

        for (let i = 0; i < this.goods.length; i++) {
          this.goods[i].quantity = 0;
        }

      }, error => console.error(error)
    );
  }

  postSet(path: string, obj: object) {
    this.http.post<Object>(location.origin + "/api/" + path, obj).subscribe(
      obj => {
        console.log(obj);
        location.reload();
      }, error => console.error(error));
  }

}

class Goods {
  id: number;
  name: string;
  description: string;
  price: number;
  supplier: string;
  type: string;

  quantity: number;
}
