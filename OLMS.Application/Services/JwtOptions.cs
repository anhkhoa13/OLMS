﻿namespace OLMS.Application.Services;

public sealed class JwtOptions
{
    public string Secret { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public int ExpirationAsMinutes { get; init; }

}
