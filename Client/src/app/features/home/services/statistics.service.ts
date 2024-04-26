import { Injectable } from '@angular/core';
import { environment } from "../../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Statistics } from "../../../shared/interfaces/Statistics";

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  private statisticsPath: string = environment.statisticsApiUrl + 'Statistics/';

  constructor(private http: HttpClient) { }

  getStatistics(): Observable<Statistics> {
    return this.http.get<Statistics>(this.statisticsPath);
  }
}
