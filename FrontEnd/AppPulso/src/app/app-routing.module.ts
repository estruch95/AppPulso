import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GestionHeroesComponent } from './componentes/gestion-heroes/gestion-heroes.component';
import { LoginComponent } from './componentes/login/login.component';

const routes: Routes = [
  {path:'', redirectTo:'login', pathMatch:'full'}, // localhost:4200 -> Redirección a login
  {path:'login', component:LoginComponent}, //Redirección al componente login
  {path:'gestion-heroes', component:GestionHeroesComponent} //Redirección al componente gestión heroes
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
