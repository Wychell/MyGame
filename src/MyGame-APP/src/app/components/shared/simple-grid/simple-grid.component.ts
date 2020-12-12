import { EventEmitter, SimpleChanges } from '@angular/core';
import { Output } from '@angular/core';
import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table'


export interface IGridAction {
  edit?: boolean;
  delete?: boolean;
}

export interface IGRidColunm {
  id: string,
  isKey?: boolean
  label: string,
  html?: boolean,
  action?: IGridAction,
  format?: (data: any) => string
}


@Component({
  selector: 'app-simple-grid',
  templateUrl: './simple-grid.component.html',
  styleUrls: ['./simple-grid.component.css']
})
export class SimpleGridComponent implements OnInit {
  @Input() data: any;
  @Input() displayedColumns: IGRidColunm[];

  @Output() onEdit: EventEmitter<any> = new EventEmitter();
  @Output() onDelete: EventEmitter<any> = new EventEmitter();
  @Output() onAdd: EventEmitter<any> = new EventEmitter();
  dataSource: MatTableDataSource<any>;

  constructor() {
  }
  ngOnChanges(_: SimpleChanges): void {
    this.dataSource = new MatTableDataSource<any>(this.data);
  }
  ngOnInit(): void {
  }

  get displayedColumnsFilter() {
    return this.displayedColumns.filter(x => !x.isKey);
  }
  onClickEdit(row: any) {
    this.onEdit.emit(row);
  }
  onClickAdd() {
    this.onAdd.emit();
  }
  onClickDelete(row: any) {
    this.onDelete.emit(row);
  }

  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource<any>(this.data);
  }
  public bindLabel(): string[] {
    return this.displayedColumnsFilter.map(x => x.id)
  }

}
