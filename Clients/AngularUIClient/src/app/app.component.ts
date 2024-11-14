import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './Layout/header/header.component';
import { SideNavComponent } from './Layout/side-nav/side-nav.component';
import { SideMainComponent } from './Layout/side-main/side-main.component';
import { MainComponent } from './Layout/main/main.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, 
    HeaderComponent,
    SideNavComponent,
    SideMainComponent, 
    MainComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  baseUrl = 'http:localhost:9000/'
  title = 'AngularUIClient';
  private http = inject(HttpClient);
}
