# InterviewApp – .NET 8 Console Application

**Candidate:** Jason Pierre Renaud  
**Project:** InterviewApp (.NET 8 Console Application)  
**Purpose:** Technical assessment demonstrating clean architecture, dependency injection (DI), configuration-based design, MediatR orchestration, DeepL integration, and automated testing.

---

## ⚙️ Overview

The application reads configuration from `appsettings.json`, dynamically constructs a greeting message based on the current time, translates it into the configured target language using the **DeepL API**, and displays it to the console.  

This implementation demonstrates:
- Clean architecture principles  
- Configuration and dependency injection  
- Separation of concerns through services  
- MediatR command-based orchestration  
- Logging  
- Unit testing and mocking

---

## 🧩 Core Features

### 1. Configuration via `appsettings.json`
All greetings and language codes are read dynamically:
```json
{
  "Greeting": {
    "Message": "Welcome to the interview app!",
    "Language": "EN"
  }
}
```

### 2. Dependency Injection (DI)
Configured in `Program.cs` using `Host.CreateDefaultBuilder()`:
```csharp
services.AddTransient<IGreetingService, GreetingService>();
services.AddTransient<ITimeGreetingService, TimeGreetingService>();
services.AddHttpClient<ITranslationService, DeepLTranslationService>();
```

### 3. Logging
- Centralized logging with `ILogger<T>`
- Tracks configuration load, translation requests, and MediatR command execution
- Logs are printed to the console and can be extended to other sinks

### 4. GreetingService
- Combines a time-based greeting with a configurable message
- Handles language translation through DeepL
- Fully decoupled and unit testable

### 5. TimeGreetingService
- Determines the correct greeting based on local time
- Automatically translated to the configured language code

### 6. DeepLTranslationService
- Uses `https://api-free.deepl.com/v2/translate`
- API key stored in environment variable:  
  `DEEPL_API_KEY`
- Handles failures gracefully and logs response details

---

## ⚙️ MediatR Integration

The project implements **MediatR v13.1**, following modern .NET registration syntax.

### Command: `GreetUserCommand`
- Encapsulates the greeting execution request
- Decouples `Program.cs` from direct service calls

### Handler: `GreetUserHandler`
- Executes `GreetingService.Run()` through MediatR  
- Ensures clean CQRS-style separation between invocation and implementation

**Registration in Program.cs**
```csharp
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
```

**Invocation**
```csharp
var mediator = host.Services.GetRequiredService<IMediator>();
await mediator.Send(new GreetUserCommand());
```

---

## 🧪 Unit Testing

### Framework
- **xUnit 2.8.0**
- **Moq 4.20.69**
- **Microsoft.NET.Test.Sdk 17.10.0**

### Test Project: `InterviewApp.Tests`

| Test File | Description |
|------------|-------------|
| `GreetingServiceTests.cs` | Ensures `GreetingService` correctly combines messages and interacts with dependencies |
| `TimeGreetingServiceTests.cs` | Confirms that the correct time-based greeting is generated |

**Configuration:**  
Each test reads from `appsettings.test.json` to isolate test settings.

Example file:
```json
{
  "Greeting": {
    "Message": "Hello Test!",
    "Language": "EN"
  }
}
```

**Run Tests:**
```bash
dotnet test
```

**Expected Output:**
```
Passed!  - 2 passed, 0 failed, 0 skipped
```

---

## 🧱 Project Structure

```
InterviewApp/
 ├── InterviewApp/
 │   ├── Program.cs
 │   ├── Models/
 │   │   └── GreetingOptions.cs
 │   ├── Requests/
 │   │   ├── GreetUserCommand.cs
 │   │   └── GreetUserHandler.cs
 │   ├── Services/
 │   │   ├── DeepLTranslationService.cs
 │   │   ├── GreetingService.cs
 │   │   ├── IGreetingService.cs
 │   │   ├── ITimeGreetingService.cs
 │   │   ├── ITranslationService.cs
 │   │   └── TimeGreetingService.cs
 │   ├── appsettings.json
 │   └── InterviewApp.csproj
 ├── InterviewApp.Tests/
 │   ├── Services/
 │   │   ├── GreetingServiceTests.cs
 │   │   └── TimeGreetingServiceTests.cs
 │   ├── appsettings.test.json
 │   └── InterviewApp.Tests.csproj
 └── README.md
```

---

## 🧾 Assumptions & Notes

- DeepL free API key is provided via environment variable  
  (`setx DEEPL_API_KEY "your-key-here"`)
- Fallback logic reverts to English if language not supported
- Fully compatible with **.NET 8**
- Designed for extensibility (additional commands, queries, and MediatR pipelines can be added)

---

## ✅ Status Summary

| Feature | Status |
|----------|---------|
| Dependency Injection | ✅ |
| Configuration via appsettings.json | ✅ |
| Logging | ✅ |
| DeepL Translation | ✅ |
| Time-based Greeting | ✅ |
| MediatR Command Architecture | ✅ |
| Unit Testing | ✅ |
| Error Handling | ✅ |
| Code Extensibility | ✅ |

---

**End of README**
