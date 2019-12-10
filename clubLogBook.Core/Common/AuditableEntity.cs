using System;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Common
{
    public class AuditableEntity :  IAuditableEntity,IBasicEntity
    {
        public string CreatedBy { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.Now;

        public string LastModifiedBy { get; set; } = "";

        public DateTime? LastModified { get; set; } = DateTime.Now;
        public int Id { get; set; }
    }
}
