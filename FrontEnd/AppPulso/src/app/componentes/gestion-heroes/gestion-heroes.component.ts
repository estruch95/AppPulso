import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HeroesService } from 'src/app/servicios/heroes.service';

@Component({
  selector: 'app-gestion-heroes',
  templateUrl: './gestion-heroes.component.html',
  styleUrls: ['./gestion-heroes.component.css']
})
export class GestionHeroesComponent implements OnInit {

  //Resultado del API que mostramos en tabla
  result:any = [];
  querySelection!:number;
  heroeId!:number;
  heroeBBDD!:string;
  heroeName!:string;

  constructor(
    private readonly router:Router,
    private readonly heroesService:HeroesService
  ) 
  { }

  ngOnInit(): void {
    this.querySelection = 0; //Inicialización del selector de consulta | 0 -> Ninguna opción, por tanto, no se muestran filtros
  }

  onSearch(searchCode:number): void {
    console.log('onSearch with search code = ', searchCode);

    //Condicionamos código de búsqueda
    switch(searchCode) { 
      case 1: { 
         //Search code = 1 -> Buscar héroe de casa concreta; 
         console.log('switch case -> 1');

         //actualizamos querySelector para mostrar el campo Id para filtrar la consulta
         this.querySelection = 1;

         break; 
      } 
      case 2: { 
         //Search code = 2 -> Buscar héroe específico;
         console.log('switch case -> 2');

         //actualizamos querySelector para mostrar el campo Id para filtrar la consulta
         this.querySelection = 2;

         break; 
      }
      case 3: { 
        //Search code = 3 -> Mostrar héroes de casa concreta;
        console.log('switch case -> 3');

        //actualizamos querySelector para mostrar el campo Id para filtrar la consulta
        this.querySelection = 3;

        break; 
      } 
      case 4: { 
        //Search code = 4 -> Mostrar todos los héroes;
        console.log('switch case -> 4');

        //actualizamos querySelector para mostrar el campo Id para filtrar la consulta
        this.querySelection = 4;

        //llamada a servicio
        this.heroesService.mostrarTodosHeroes().subscribe( response => {
          console.log("Todos los heroes -> ", response);
          this.result = response;

        });
        break; 
      } 
      default: { 
         //Consulta por defecto -> Mostrar todos los héroes; 
         break; 
      } 
   } 

  }

  buscarHeroe(heroeId:number): void {
    this.heroesService.buscarHeroe(heroeId).subscribe( response => {
      console.log('Heroe by id -> ', response);

      if(response.length > 0){
        this.result = response;
      }
      else {
        this.result = response;
        alert('No hay resultados para esta búsqueda');
      }
    });
  }

  buscarHeroesDeCasa(casa:string): void {
    this.heroesService.mostrarHeroesDeCasaConcreta(casa).subscribe( response => {
      console.log('Heroes de la casa '+casa+' -> ', response);
      if(response.length > 0){
        this.result = response;
      }
      else {
        this.result = response;
        alert('No hay resultados para esta búsqueda');
      }
      
    });
  }

  buscarHeroeDeCasa(heroeBBDD:string, heroeName:string): void {
    this.heroesService.buscarHeroeDeCasaConcreta(heroeBBDD, heroeName).subscribe( response => {
      console.log('Heroe: '+heroeName+' de la casa: '+heroeBBDD+' -> ', response);
      if(response[0] != null){
        this.result = response;
      }
      else {
        alert('No hay resultados para esta búsqueda');
      }
      
    });
  }

}
