import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BaseEntity } from 'src/app/Models/classes/base-entity';

@Component({
  selector: 'app-select-entity',
  templateUrl: './select-entity.component.html',
  styleUrls: ['./select-entity.component.css']
})
export class SelectEntityComponent implements OnInit {

  @Input() entityId: number;
  @Input() entities: BaseEntity[];
  @Input() title: string;
  @Output() onEntityChanged = new EventEmitter<number>();  
  constructor() { }

  ngOnInit(): void {
  }

  onChange(selectedValue): void{
    console.log("changed to " + selectedValue);
    this.onEntityChanged.emit(selectedValue);
  }

}
