import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AudioComponent } from './audio/audio';
import { IncluirAudioComponent } from './audio/incluiraudio/incluiraudio';
import { AlterarAudioComponent } from './audio/alteraraudio/alteraraudio';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AudioComponent,
    IncluirAudioComponent,
    AlterarAudioComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'audio', component: AudioComponent },
      { path: 'audio/incluiraudio', component: IncluirAudioComponent },
      { path: 'audio/alteraraudio', component: AlterarAudioComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
