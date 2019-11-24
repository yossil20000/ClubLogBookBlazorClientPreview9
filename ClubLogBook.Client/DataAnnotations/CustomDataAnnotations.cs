using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClubLogBook.Client.DataAnnotations
{
	public class DateGreaterThanAttribute : ValidationAttribute
	{
		public string otherPropertyName;
		

		public DateGreaterThanAttribute(string otherPropertyName, string ErrorMessage)
		{
			this.otherPropertyName = otherPropertyName;
			this.ErrorMessage = ErrorMessage;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			ValidationResult validationResult = ValidationResult.Success;
			try
			{
				var containerType = validationContext.ObjectInstance.GetType();
				var field = containerType.GetProperty(this.otherPropertyName);
				var extensionValue = field.GetValue(validationContext.ObjectInstance, null);
				var datatype = extensionValue.GetType();

				//var otherPropertyInfo = validationContext.ObjectInstance.GetType().GetProperty(this.otherPropertyName);
				if (field == null)
					return new ValidationResult(String.Format("Unknown property: {0}.", otherPropertyName));
				// Let's check that otherProperty is of type DateTime as we expect it to be
				if ((field.PropertyType == typeof(DateTime) || (field.PropertyType.IsGenericType && field.PropertyType == typeof(Nullable<DateTime>))))
				{
					DateTime toValidate = (DateTime)value;
					DateTime referenceProperty = (DateTime)field.GetValue(validationContext.ObjectInstance, null);
					// if the end date is lower than the start date, than the validationResult will be set to false and return
					// a properly formatted error message
					if (toValidate.CompareTo(referenceProperty) < 1)
					{
						validationResult = new ValidationResult(ErrorMessageString);
					}
				}
				else
				{
					validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTime");
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return validationResult;
		}
	}
}
