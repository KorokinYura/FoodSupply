import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { formatDate } from '@angular/common';


import { AlertService, MessageSeverity } from '../services/alert.service';
import { AccountService } from "../services/account.service";
import { Utilities } from '../services/utilities';
import { User } from '../models/user.model';
import { Role } from '../models/role.model';


@Component({
  selector: 'app-make-an-order',
  templateUrl: './make-an-order.component.html',
  styleUrls: ['./make-an-order.component.css']
})
export class MakeAnOrderComponent implements OnInit {

  sets: Set[] = [];
  prices: number[] = [];

  day = 86400000;
  idSet = 0;

  orderDate: string = formatDate(Date.now() + this.day, "yyyy-MM-dd", "en-US");

  constructor(private http: HttpClient, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.getSets("SetOrders");
    this.loadCurrentUserData();
  }

  makeOrder() {
    this.postSet("SetOrders/" + this.user.id, { idSet: this.idSet, date: this.orderDate });
    //alert(this.user.id);
  }


  postSet(path: string, obj: object) {
    if (this.idSet === null || this.idSet === undefined)
      return;

    this.http.post<Object>(location.origin + "/api/" + path, obj).subscribe(
      obj => {
        console.log(obj);
        alert("Success");
        location.reload();
      }, error => {
        console.error(error);
        //alert("Error: " + error);
        alert("Choose a set");
      });
  }


  getSets(path: string) {
    this.http.get(location.origin + "/api/" + path).subscribe(
      obj => {
        this.sets = <Set[]>obj;
        console.log("Sets: \n " + this.sets);

        for (let i = 0; i < this.sets.length; i++) {
          let price = 0;

          for (let j = 0; j < this.sets[i].goods.length; j++) {
            price += this.sets[i].goods[j].price * this.sets[i].goods[j].quantity;
          }
          this.prices.push(price);
        }

        this.idSet = this.sets[0].id;

      }, error => console.error(error));
  }







  user: User;

  private loadCurrentUserData() {
    this.alertService.startLoadingMessage();

    //this.accountService.getUserAndRoles().subscribe(results => this.onCurrentUserDataLoadSuccessful(results[0], results[1]), error => this.onCurrentUserDataLoadFailed(error));

    this.accountService.getUser().subscribe(user => this.onCurrentUserDataLoadSuccessful(user, user.roles.map(x => new Role(x))), error => this.onCurrentUserDataLoadFailed(error));
  }


  private onCurrentUserDataLoadSuccessful(user: User, roles: Role[]) {
    this.alertService.stopLoadingMessage();
    this.user = user;
  }

  private onCurrentUserDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);

    this.user = new User();
  }
}

class Set {
  goods: Goods[] = [];
  name: string;
  id: number;
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
