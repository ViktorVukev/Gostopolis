import { Table } from './Table';

export interface TableType {
  isSmokingAllowed: boolean,
    isOutdoor: boolean,
    capacity: number,
    tables: Array<Table>
}
