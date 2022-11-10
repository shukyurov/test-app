import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Contact } from 'src/models/contact';
import { ContactsService } from 'src/services/contacts.service';

@Component({
  selector: 'app-contact-add-edit-item-modal',
  templateUrl: './contact-add-edit-item-modal.component.html',
  styleUrls: ['./contact-add-edit-item-modal.component.css'],
})
export class ContactAddEditItemModalComponent implements OnInit, OnDestroy {
  private subs = new Subscription();
  @Input() contact: Contact;
  public pageTitle: string;
  public form: FormGroup;
  private isEdit: boolean;

  constructor(
    public readonly activeModal: NgbActiveModal,
    private readonly contactService: ContactsService,
    private readonly fb: FormBuilder
  ) {}

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  ngOnInit(): void {
    if (this.contact) {
      this.pageTitle = 'Edit New Contact';
      this.isEdit = true;
    } else {
      this.pageTitle = 'Add New Contact';
      this.isEdit = false;
      this.contact = {} as Contact;
    }

    this.form = this.fb.group({
      firstName: [this.isEdit ? this.contact.firstName : ''],
      lastName: [this.isEdit ? this.contact.lastName : ''],
      email: [this.isEdit ? this.contact.email : ''],
    });
  }

  save(): void {
    this.contact.firstName = this.form.get('firstName').value;
    this.contact.lastName = this.form.get('lastName').value;
    this.contact.email = this.form.get('email').value;

    if (this.isEdit) {
      this.subs.add(this.contactService.editContact(this.contact).subscribe());
    } else {
      this.subs.add(
        this.contactService.addNewContact(this.contact).subscribe()
      );
    }
    this.activeModal.close();
  }
}
