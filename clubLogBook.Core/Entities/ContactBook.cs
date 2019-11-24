using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public class ContactBook : BaseEntity, IAggregateRoot
	{

		public string Name { get; set; } = "Global";
		public ContactBook() { }
		public void  AddContact(Contact contact)
		{
			if (!_contacts.Any(i => i.Id == contact.Id) || contact.Id == 0)
				_contacts.Add(contact);
			else
			{
				var c = _contacts.Where(i => i.Id == contact.Id).SingleOrDefault();
				c = contact;
			}
		}
		public Contact GetById(int id)
		{
			return _contacts.Where(i => i.Id == Id).SingleOrDefault() ?? null;
		}
		private List<Contact> _contacts { get; set; } = new List<Contact>();
		public virtual ICollection<Contact> Contacts => _contacts;
	}
}
