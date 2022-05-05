import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HeroesService {

  private url:string = "/api/heroes/";

  constructor(private http:HttpClient) { }

  //Método que obtiene el JWT token
  getHttpHeader(): HttpHeaders {
    var reqHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + sessionStorage.getItem('auth-token')
    });

    return reqHeader;
  }

  //Busca un héroe de una casa concreta
  buscarHeroeDeCasaConcreta(bbdd:string, heroeName:string):Observable<any> {
    var httpHeader = this.getHttpHeader();
    return this.http.get<any>(this.url+bbdd+"/"+heroeName, {headers: httpHeader});
  }

  //Busca un héroe específico
  buscarHeroe(heroeId:number):Observable<any> {
    var httpHeader = this.getHttpHeader();
    return this.http.get<any>(this.url+heroeId, {headers: httpHeader});
  }

  //Muestra los héroes de una casa concreta
  mostrarHeroesDeCasaConcreta(bbdd:string):Observable<any> {
    var httpHeader = this.getHttpHeader();
    return this.http.get<any>(this.url+bbdd, {headers: httpHeader});
  }

  //Muestra todos los héroes inclusive la casa a la que pertenecen
  mostrarTodosHeroes():Observable<any> {
    var httpHeader = this.getHttpHeader();
    return this.http.get<any>(this.url, {headers: httpHeader});
  }


}
