import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Contact } from 'src/models/contact';

@Injectable({
  providedIn: 'root',
})
export class ContactsService {
  public contactAdded: Subject<Contact> = new Subject();
  private readonly baseUrl = 'https://localhost:7282/';

  constructor(private readonly httpClient: HttpClient) {}

  public getContactsList(): Observable<Contact[]> {
    return this.httpClient.get<Contact[]>(this.baseUrl + 'Account');
  }

  public addNewContact(contact: Contact): Observable<any> {
    return this.httpClient.post(this.baseUrl + 'Account/create', contact).pipe(
      map((el) => {
        this.contactAdded.next(contact);
        return el;
      })
    );
  }

  public editContact(contact: Contact): Observable<any> {
    return this.httpClient.put(this.baseUrl + 'Account/edit', contact);
  }

  public deleteContact(id: number): Observable<any> {
    return this.httpClient.delete(this.baseUrl + `Account/delete/${id}`).pipe(
      map((el) => {
        this.contactAdded.next();
        return el;
      })
    );
  }
}
