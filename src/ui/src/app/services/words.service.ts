import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs'
import { Constants } from '../shared/constants'
import { WordModel } from '../model/word-model';

@Injectable({
  providedIn: 'root'
})
export class WordsService {
  private controller:string = '/words';

  constructor(private $http:HttpClient) { 

  }

  public getWords () : Observable<WordModel[]> {
    return this.$http.get<WordModel[]>(Constants.API_HOST + this.controller);
  }

  public getTopWords (quantity:number) : Observable<WordModel[]> {
    return this.$http.get<WordModel[]>(Constants.API_HOST + this.controller + '/top/' + quantity);
  }
}
