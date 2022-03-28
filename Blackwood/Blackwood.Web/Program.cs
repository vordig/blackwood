var app = WebApplication.Create();

app.MapGet("helloworld", () => "Hello, World!");

app.Run();