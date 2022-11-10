import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Contact } from 'src/models/contact';
import { ContactsService } from 'src/services/contacts.service';
import { ContactAddEditItemModalComponent } from '../modals/contact-add-edit-item-modal/contact-add-edit-item-modal.component';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css'],
})
export class ContactListComponent implements OnInit, OnDestroy {
  private subs = new Subscription();
  public contacts: Contact[] = [];

  constructor(
    public readonly contactService: ContactsService,
    private readonly modalService: NgbModal
  ) {}

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  ngOnInit(): void {
    this.subs.add(
      this.contactService.getContactsList().subscribe((res: Contact[]) => {
        this.contacts = res;
      })
    );

    this.subs.add(
      this.contactService.contactAdded.subscribe((el) => {
        this.contactService.getContactsList().subscribe((res: Contact[]) => {
          this.contacts = res;
        });
      })
    );
  }

  public addNewContact(): void {
    this.modalService.open(ContactAddEditItemModalComponent);
  }

  public editContact(contact: Contact): void {
    const modalRef = this.modalService.open(ContactAddEditItemModalComponent);
    modalRef.componentInstance.contact = contact;
  }

  public deleteContact(id: number): void {
    this.subs.add(this.contactService.deleteContact(id).subscribe());
  }
}
