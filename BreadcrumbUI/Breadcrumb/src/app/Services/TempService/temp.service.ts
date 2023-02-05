import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from '../../Core/Services/ConfigService/config.service';

@Injectable({
  providedIn: 'root'
})
export class TempService {
  private baseURL:string = "";

  constructor(private ConfigService:ConfigService, private http: HttpClient) { 
    this.baseURL = this.ConfigService.getApiUrl();
  }

  get(){
    let apiLink = this.baseURL+"api/Shows/Get?page=1&itemsPerPage=100&Type=Movie";
    console.log(apiLink);
    const headers= new HttpHeaders()
      .set("Authorization", 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJEYXRhYmFzZVR5cGUiOiJTUUxTZXJ2ZXIiLCJDb25uZWN0aW9uU3RyaW5nIjoiU2VydmVyPXRjcDpNU0ksNDkxNzI7RGF0YWJhc2U9QnJlYWRjcnVtYjtVc2VyIElkPXNhO1Bhc3N3b3JkPTE4MTU4MTE0OyIsIlNoZWV0SWQiOiIiLCJleHAiOjE3MDcxNTU4NTcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0Mzc2LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0Mzc2LyJ9.5VUlcN1-RRRDjKZuwDYfNT3XSTuyZDHyxI07pNleAZ8');
    return this.http.get(apiLink, { headers: headers});
  }
}
