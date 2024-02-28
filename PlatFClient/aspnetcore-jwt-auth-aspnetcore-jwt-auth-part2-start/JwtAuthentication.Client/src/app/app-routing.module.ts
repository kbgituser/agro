import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CityIndexComponent } from './components/cities/index/city-index.component';
import { CategoryIndexComponent } from './components/categories/category-index/category-index.component';
import { CategoryUpdateComponent } from './components/categories/category-update/category-update.component';
import { CityUpdateComponent } from './components/cities/update/city-update.component';
import { RequestIndexComponent } from './components/request/request-index/request-index.component';
import { CustomersComponent } from './customers/customers.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';

const routes: Routes = [
  { path: '', component: HomeComponent },

  { path: 'login', component: LoginComponent },
  //{ path: 'customers', component: CustomersComponent, canActivate: [AuthGuard] },
  { path: 'customers', component: CustomersComponent, canActivate: [AuthGuard] },
  //cities

  { path: 'cities', component: CityIndexComponent, canActivate: [AuthGuard] },
  { path: 'cities/edit/:id', component: CityUpdateComponent , canActivate: [AuthGuard]},
  { path: 'cities/create', component: CityUpdateComponent , canActivate: [AuthGuard]},

  
  { path: 'categories', component: CategoryIndexComponent, canActivate: [AuthGuard] },
  { path: 'categories/:id', component: CategoryUpdateComponent , canActivate: [AuthGuard]},
  { path: 'requests', component: RequestIndexComponent , canActivate: [AuthGuard]},

  { path: 'users/registration', component: RegistrationComponent},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
