import { Component, OnInit } from '@angular/core';
import { FeedsService } from '../services/feeds.service';
import { FeedModel } from '../model/feed-model';

@Component({
  selector: 'app-feeds-list',
  templateUrl: './feeds-list.component.html',
  styleUrls: ['./feeds-list.component.scss']
})
export class FeedsListComponent implements OnInit {

  private data:FeedModel[];
  constructor(private svc:FeedsService) {
    
  }


  ngOnInit() {
    this.svc.getFeeds().subscribe(data => this.data = data);
  }

}
