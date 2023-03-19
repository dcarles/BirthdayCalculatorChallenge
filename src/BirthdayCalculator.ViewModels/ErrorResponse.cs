using System.Collections.Generic;
using System.Text.Json;

namespace BirthdayCalculator.ViewModels;

public class ErrorResponse
{
    public string? Message { get; set; }

    public List<ErrorResponseItem>? Errors { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}

