﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace ClubLogBook.Application.Common.Exceptions
{
    public class MyValidationException : Exception
    {
        public MyValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public MyValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
        public StringBuilder FailuresMessage { 
            get {
                StringBuilder message = new StringBuilder();
                foreach (var failure in Failures)
                {
                    foreach (var s in failure.Value)
                    {
                        message.AppendJoin("/n", $"{failure.Key}: {s}");
                    }
                }
                return message;

            }
        }
    }
}
