using Newtonsoft.Json;
namespace ClubLogBook.Core.Extensions
{
	public static class EntityExtentionscs
	{
		public static string GetJason<T>(this T obj) where T : class
		{
			return JsonConvert.SerializeObject(obj);
		}
		public static T GetFromJason<T>(this string  obj) where T : class
		{
			
			return (T)JsonConvert.DeserializeObject<T>(obj);
		}
	}
}
