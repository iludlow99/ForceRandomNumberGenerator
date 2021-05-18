import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `<div>{{randomNumber}}</div>`,
})
export class AppComponent {
  randomNumber: number;

  constructor(private http: HttpClient) {
    this.http.get('/api/GenerateRandomNumber')
      .subscribe((res: any) => this.randomNumber = res);
  }
}