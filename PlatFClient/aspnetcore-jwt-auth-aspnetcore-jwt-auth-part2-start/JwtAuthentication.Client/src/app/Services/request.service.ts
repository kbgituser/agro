import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestDto } from '../Models/Request/request-dto';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(public httpClient: HttpClient) { }
  public url="https://localhost:7272/api/Requests/ByPage";
  public changeStatusUrl="https://localhost:7272/api/Requests/ChangeStatus";
  public getRequests() : Observable<RequestDto[]> {
    return this.httpClient.get<RequestDto[]>(this.url);
  }
  public create(requestDto: RequestDto)
  {
    return this.httpClient.post(this.url,requestDto);
  }

  public getRequestsByPage(page: number) : Observable<RequestDto[]> {
    let queryParams = new HttpParams().append("page",page);
    //queryParams.append("pageSize",pageSize);
    //return this.httpClient.get<RequestDto[]>(this.url, {params: queryParams});
    return this.httpClient.get<RequestDto[]>(this.url);

  }
  public getRequestById(id: number) : Observable<RequestDto> {
    return this.httpClient.get<RequestDto>(this.url+"/"+id);
  }

  public update(requestDto: RequestDto) : Observable<string> {
    return this.httpClient.put<string>(this.url, requestDto);
  }

  public deleteById(id: string) {
    return this.httpClient.delete(this.url+"/"+id);
  }

  public changeStatus(id: number, status:number) 
  {    
    const body =  { id: id, status: status };
    return this.httpClient.put(this.changeStatusUrl, body );
  }
  
}
