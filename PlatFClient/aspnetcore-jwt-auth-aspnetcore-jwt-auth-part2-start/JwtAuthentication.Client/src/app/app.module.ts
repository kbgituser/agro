import { AuthGuard } from './guards/auth.guard';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { CustomersComponent } from './customers/customers.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {FormsModule} from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';
import { CityIndexComponent } from './components/cities/index/city-index.component';
import { CityUpdateComponent } from './components/cities/update/city-update.component';
import { CategoryIndexComponent } from './components/categories/category-index/category-index.component';
import { CategoryUpdateComponent } from './components/categories/category-update/category-update.component';
import { CategorySelectComponent } from './components/categories/category-select/category-select.component';
import { SelectEntityComponent } from './components/select-entity/select-entity.component';
import { RequestIndexComponent } from './components/request/request-index/request-index.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { PricecomComponent } from './components/pricecom/pricecom.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { AdvertisementIndexComponent } from './components/advertisements/advertisement-index/advertisement-index.component';
import { AdvertisementUpdateComponent } from './components/advertisements/advertisement-update/advertisement-update.component'; 

export function tokenGetter(){
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CustomersComponent,
    HomeComponent,
    CityUpdateComponent,
    CategoryIndexComponent,
    CategoryUpdateComponent,
    CategorySelectComponent,
    SelectEntityComponent,
    CityIndexComponent,
    RequestIndexComponent,
    HeaderComponent,
    FooterComponent,
    PricecomComponent,
    RegistrationComponent,
    AdvertisementIndexComponent,
    AdvertisementUpdateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    //MatSelect,
    // RouterModule.forRoot([
    //   {path: '', component: HomeComponent},
    //   {path: 'login', component: LoginComponent},
    //   {path: 'customer', component: CustomersComponent, canActivate: [AuthGuard]}
    // ]),
    JwtModule.forRoot(
      {
        config:{
          tokenGetter: tokenGetter,
          allowedDomains: ["localhost:7272"],
          disallowedRoutes: []
        }
      }
    )
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
