import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Constants } from '../shared/constants'
import { Observable } from 'rxjs/Observable'
import { FeedModel } from '../model/feed-model';

@Injectable({
  providedIn: 'root'
})
export class FeedsService {
  private controller:string = '/feeds';

  constructor(private $http:HttpClient) { 

  }

  public getFeeds () : Observable<FeedModel[]> {
    return this.$http.get<FeedModel[]>(Constants.API_HOST + this.controller);
  }

  public getTopFeeds (quantity:number) : Observable<FeedModel[]> {
    return this.$http.get<FeedModel[]>(Constants.API_HOST + this.controller + '/top/' + quantity);
  }
}
