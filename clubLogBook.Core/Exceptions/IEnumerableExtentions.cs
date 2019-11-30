using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Exceptions
{ 
	public static class IEnumerableExtentions
	{
		public static IEnumerable<T> GetTs<T,T1>(this IEnumerable<T1> list) where T : class ,IMember where T1 : class,IMember
		{
			foreach(var obj in list)
			{
				if (obj is T)
					yield return obj as T;
			}
		}

		public static IReadOnlyCollection<T> GetpILOT<T,T1>(this IReadOnlyCollection<T1> list) where T : class, IMember where T1 : class, IMember
		{

			List<T> a = new List<T>();
			foreach (var obj in list)
			{
				if (obj is T) { System.Diagnostics.Debug.WriteLine($"I am In {(obj as T)}"); a.Add(obj as T); }



			}
			return a.AsReadOnly();
		}

		public static IReadOnlyCollection<T> GetByType<T>(IReadOnlyCollection<IMember> list) where T : class , IMember
		{
			List<T> a = new List<T>();
			foreach (var obj in list)
			{
				if (obj is T) { System.Diagnostics.Debug.WriteLine($"I am In {(obj as T)}"); a.Add(obj as T); }
					


			}
			return a.AsReadOnly();
		}

		public static IEnumerable<T> GetpILOT<T, T1>(this IEnumerable<T1> list) where T : class, IMember where T1 : class, IMember
		{

			List<T> a = new List<T>();
			foreach (var obj in list)
			{
				if (obj is T) { System.Diagnostics.Debug.WriteLine($"I am In {(obj as T)}"); a.Add(obj as T); }



			}
			return a.AsReadOnly();
		}
		public static IEnumerable<T> GetByType<T>(IEnumerable<IMember> list) where T : class, IMember
		{
			List<T> a = new List<T>();
			foreach (var obj in list)
			{
				if (obj is T) { System.Diagnostics.Debug.WriteLine($"I am In {(obj as T)}"); a.Add(obj as T); }



			}
			return a.AsReadOnly();
		}
	}
}
