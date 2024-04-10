﻿namespace Repositories.DTOs
{
	public class CustomResponse<T>
	{
		public int? StatusCode { get; set; } = 200;
		public string? Message { get; set; }
		public T? Data { get; set; }
	}
}
