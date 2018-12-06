import { Component, OnInit } from '@angular/core';
import { WordsService } from '../services/words.service';
import { WordModel } from '../model/word-model';

@Component({
  selector: 'app-words-table',
  templateUrl: './words-table.component.html',
  styleUrls: ['./words-table.component.scss']
})
export class WordsTableComponent implements OnInit {

  private data:WordModel[];
  constructor(private svc:WordsService) { }

  ngOnInit() {
    this.svc.getTopWords(10).subscribe(data => this.data = data);
  }
}
