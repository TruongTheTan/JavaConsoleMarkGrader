import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-table',
    templateUrl: './table.component.html',
    styleUrls: ['./table.component.css'],
})
export class TableComponent<T> {
    @Input() modalId = '';
    @Input() dataList = [] as T[] | null;
    @Input() tableHeadings = [] as string[];
    @Input() keys = [] as (keyof T)[] | null;
    @Output() buttonClick = new EventEmitter<{ data: T; eventName: string }>();

    //
    isBoolean(value: unknown): value is boolean {
        return typeof value === 'boolean';
    }

    isArray(value: unknown): value is unknown[] {
        return Array.isArray(value);
    }

    asArray(value: unknown): unknown[] {
        return value as unknown[];
    }

    onDetailButtonClick(data: T) {
        this.buttonClick.emit({ eventName: 'detail', data: data });
    }

    onDeleteButtonClick(data: T) {
        this.buttonClick.emit({ eventName: 'delete', data: data });
    }
}
