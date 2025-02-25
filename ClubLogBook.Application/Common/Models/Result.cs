﻿using System.Collections.Generic;
using System.Linq;

namespace ClubLogBook.Application.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}


//using System.Collections.Generic;
//using System.Linq;

//namespace ClubLogBook.Application.Common.Models
//{
//    public class Result
//    {
//        public Result()
//        {
//            Succeeded = true;
//            Errors = new List<string>();
//        }

//        public Result(bool succeeded, List<string> errors )
//        {
//            Succeeded = succeeded;
//            Errors = errors;
//        }

//        public bool Succeeded { get; private set; }

//        public List<string> Errors { get; private set; }

//        public static Result Success()
//        {
//            return new Result(true,new List<string>());
//        }

//        public static Result Failure(List<string> errors)
//        {
//            return new Result(false, errors);
//        }
//        public void AddError(string error)
//        {
//            Succeeded = false;
//            Errors.Add(error);
//        }
//    }
//}
