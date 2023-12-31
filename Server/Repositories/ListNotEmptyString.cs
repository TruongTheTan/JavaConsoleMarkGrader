using System.ComponentModel.DataAnnotations;

namespace Repositories
{

	public class ListNotEmptyString : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			if (value is List<string> list)
			{
				return !list.Any(s => string.IsNullOrEmpty(s));
			}
			return false;
		}
	}

}
