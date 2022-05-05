import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private url:string = "/api/usuarios/";

  constructor(private http:HttpClient) { }

  loginUser(user:string, password:string):Observable<any> {
    console.log('loginService -> ',this.url+user+"/"+password);
    return this.http.get<any>(this.url+user+"/"+password);
  }

}
