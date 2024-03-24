using System.ComponentModel.DataAnnotations;

namespace Repositories
{

	public class ListNotContainEmptyString : ValidationAttribute
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



	public class ValidateInputAndOutput : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{

			if (value is bool)
			{


				return true;
			}
			return false;
		}
	}

}
