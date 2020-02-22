using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Core.Common;

namespace ClubLogBook.Core.Entities
{
	public class ContactBook : AuditableEntity, IAggregateRoot
	{

		public string Name { get; set; } = "Global";
		public ContactBook() { }
		public void  AddContact(Contact contact)
		{
			if (!Contacts.Any(i => i.Id == contact.Id) || contact.Id == 0)
				Contacts.Add(contact);
			else
			{
				var c = Contacts.Where(i => i.Id == contact.Id).SingleOrDefault();
				c = contact;
			}
		}
		public Contact GetById(int id)
		{
			return Contacts.Where(i => i.Id == Id).SingleOrDefault() ?? null;
		}
		
		public virtual ICollection<Contact> Contacts { get; set; }
	}
}
