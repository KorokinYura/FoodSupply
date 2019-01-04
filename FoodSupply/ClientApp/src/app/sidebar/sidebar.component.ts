import { Component, OnInit } from '@angular/core';
import { Permission } from '../models/permission.model';
import { AccountService } from "../services/account.service";
import { ConfigurationService } from '../services/configuration.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  constructor(public configurations: ConfigurationService, private accountService: AccountService, private http: HttpClient) { }

  ngOnInit() {
  }

  makeAnOrder() {
    document.getElementById("makeAnOrder").hidden = false;
    document.getElementById("createSet").hidden = true;
    document.getElementById("addGoods").hidden = true;
  }

  createSet() {
    document.getElementById("makeAnOrder").hidden = true;
    document.getElementById("createSet").hidden = false;
    document.getElementById("addGoods").hidden = true;
  }

  addGoods() {
    document.getElementById("makeAnOrder").hidden = true;
    document.getElementById("createSet").hidden = true;
    document.getElementById("addGoods").hidden = false;
  }

  get canAddGoods() {
    return this.accountService.userHasPermission(Permission.viewRolesPermission);
  }
}
