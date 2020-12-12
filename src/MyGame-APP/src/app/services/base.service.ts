import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { map, filter, tap } from 'rxjs/operators'
import { environment } from '../../environments/environment';


export class ResultData<T>{
    data: T;
    success: boolean
    mensagem: any
}

@Injectable({
    providedIn: 'root'
})
export abstract class BaseService<TRoot> {
    protected domain: string;
    protected headers: HttpHeaders;

    constructor(private http: HttpClient) {
        this.headers = this.setHeaders();
        this.domain = environment.apiUrl
    }


    protected setHeaders(): HttpHeaders {
        let headers = new HttpHeaders();
        headers = headers.set("Access-Control-Allow-Credentials", "*");
        headers = headers.set("Access-Control-Allow-Origin", "*");
        headers = headers.set(
            "Access-Control-Allow-Methods",
            "GET, POST, PATCH, PUT, DELETE, OPTIONS"
        );
        return headers
    }

    private methodUrl(method: string) {
        return this.domain.concat(method);
    }

    private getFormData(data: any): FormData {
        const formaData = new FormData();
        for (var attr in data) {
            const value = data[attr] !== undefined ? data[attr] : null;
            formaData.append(attr, value);
        }
        return formaData;
    }
    protected postFromUploadFile(methodName: string, data: any) {
        return this.http.post<ResultData<any>>(this.methodUrl(methodName), this.getFormData(data)).pipe(
            filter(result => result.success),
            map(result => result.data)
        );
    }

    protected putFromUploadFile(methodName: string, id: string, data: any) {
        return this.http.put<ResultData<any>>(`${this.methodUrl(methodName)}/${id}`, this.getFormData(data)).pipe(
            filter(result => result.success),
            map(result => result.data)
        );
    }

    protected postRootAsync(methodName: string, data: any = null, headers: any = null): Promise<TRoot> {
        return this.http.post<ResultData<TRoot>>(this.methodUrl(methodName), data, { headers: headers ?? this.headers })
            .pipe(
                filter(result => result.success),
                map(result => result.data)
            ).toPromise();
    }
    protected getRootAsync(methodName: string, id: string = null, query: any = null, headers: any = null): Promise<TRoot> {
        return this.http.get<ResultData<TRoot>>(`${this.methodUrl(methodName)}/${id ? id : ""}`, { params: query, headers: headers ?? this.headers })
            .pipe(
                filter(result => result.success),
                map(result => result.data)
            ).toPromise();
    }
    protected putRootAsync(methodName: string, id: string = null, data: any = null, headers: any = null): Promise<TRoot> {
        return this.http.put<ResultData<TRoot>>(`${this.methodUrl(methodName)}${id ? "/" + id : ""}`, data, { headers: headers ?? this.headers })
            .pipe(
                filter(result => result.success),
                map(result => result.data)
            ).toPromise();;

    }
    protected deleteRootAsync(methodName: string, id: string, headers: any = null): Promise<TRoot> {
        return this.http.delete<ResultData<TRoot>>(`${this.methodUrl(methodName)}/${id}`, { headers: headers ?? this.headers })
            .pipe(
                filter(result => result.success),
                map(result => result.data)
            ).toPromise();;
    }

    protected searchRootAsync(methodName: string, headers: any = null): Promise<TRoot[]> {
        return this.http.get<ResultData<TRoot[]>>(`${this.methodUrl(methodName)}`, { headers: headers ?? this.headers })
            .pipe(
                filter(result => result.success),
                map(result => result.data)
            ).toPromise();
    }

}
