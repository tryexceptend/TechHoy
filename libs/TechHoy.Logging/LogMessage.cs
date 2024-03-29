using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace TechHoy.Logging;

/// <summary>
/// Сообщение сохраняемое в лог
/// </summary>
public record LogMessage
{
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public ulong Id { get; set; }
    [JsonPropertyName("message")]
    public required string Message { get; set; }
    [JsonPropertyName("source")]
    public string? Source { get; set; }
    [JsonPropertyName("app")]
    public string? Application { get; set; }
    [JsonPropertyName("level")]
    public LogLevel Level { get; set; }
    [JsonPropertyName("group")]
    public int Group { get; set; } = 0;
    [JsonPropertyName("val")]
    public decimal? Value { get; set; }
}
