namespace OrderManagementSystem.API.Models;

public class ApiResponse<T>
{
    /// <summary>
    /// İşlemin başarılı olup olmadığı
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// HTTP benzeri durum kodu (opsiyonel, FE için bilgi)
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// FE tarafında anlamlı mesaj göstermek için string mesaj
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// FE tarafında lokalize edilebilecek kod
    /// </summary>
    public string? MessageCode { get; set; }

    /// <summary>
    /// Dönen veri
    /// </summary>
    public T? Data { get; set; }

    public ApiResponse() { }

    public ApiResponse(T? data, bool success = true, int statusCode = 200, string? message = null, string? messageCode = null)
    {
        Data = data;
        Success = success;
        StatusCode = statusCode;
        Message = message;
        MessageCode = messageCode;
    }

    // Kolay kullanım için static helper metodlar
    public static ApiResponse<T> Ok(T? data, string? message = null, string? messageCode = null)
        => new(data, true, 200, message, messageCode);

    public static ApiResponse<T> NotFound(string? message = null, string? messageCode = null)
        => new(default, false, 404, message, messageCode);

    public static ApiResponse<T> BadRequest(string? message = null, string? messageCode = null)
        => new(default, false, 400, message, messageCode);

    public static ApiResponse<T> InternalError(string? message = null, string? messageCode = null)
        => new(default, false, 500, message, messageCode);
}
