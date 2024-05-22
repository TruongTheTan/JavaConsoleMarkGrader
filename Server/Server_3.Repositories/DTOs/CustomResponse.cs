namespace Repositories.DTOs;

public sealed class CustomResponse<T>
{
	public T? Data { get; set; }
	public int? StatusCode { get; set; }
	public string? Message { get; set; } = string.Empty;
	public bool IsSuccess { get => StatusCode >= 200 && StatusCode <= 299; }
}
