import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { formatDate } from '@angular/common';


import { AlertService, MessageSeverity } from '../../services/alert.service';
import { AccountService } from "../../services/account.service";
import { Utilities } from '../../services/utilities';
import { User } from '../../models/user.model';
import { Role } from '../../models/role.model';
import { setTimeout } from 'timers';


@Component({
  selector: 'orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent {

  orders: Order[];

  constructor(private http: HttpClient, private alertService: AlertService, private accountService: AccountService) { }

  ngOnInit() {
    this.loadCurrentUserData();
  }

  sleep(milliseconds: number) {
    let e = new Date().getTime() + milliseconds;
    while (new Date().getTime() <= e) { }
  }

  getUserOrders(path: string) {
    this.http.get(location.origin + "/api/" + path + "/" + this.user.id).subscribe(
      obj => {
        this.orders = <Order[]>obj;
        console.log("Orders: \n " + obj);

        for (let i = 0; i < this.orders.length; i++) {
          this.orders[i].formatedDate = formatDate(this.orders[i].date, "yyyy-MM-dd", "en-US")
        }
      }, error => console.error(error));
  }

  deleteOrder(id: string) {
    this.http.delete(location.origin + "/api/SetOrders/" + id).subscribe(
      obj => {
        if (obj !== null)
          console.log(obj);
        location.reload();
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

    console.log(this.user);
    this.getUserOrders("SetOrders");
  }

  private onCurrentUserDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);

    this.user = new User();
  }
}


class Order {
  id: number;
  idUser: string;
  idSet: number;
  setName: string;
  date: Date;
  formatedDate: string;
  //goodsSet: Set;
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
