```csharp
builder.Services.AddFmgLibHttpClient(); // OR
builder.Services
    .AddFmgLibHttpClient(() =>
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            return options;
        });
```

```csharp
User user = await HttpClientHelper.SendAsync<User>("REUQEST_URL", HttpMethod.Post, jsonRequestContent, ClientContentType.Json);
```
