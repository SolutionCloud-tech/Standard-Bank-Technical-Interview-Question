# InterviewApp – .NET 8 Console Application

**Candidate:** Jason Pierre Renaud  
**Project:** InterviewApp (.NET 8 Console Application)  
**Purpose:** Technical assessment demonstrating clean architecture, dependency injection (DI), configuration management, logging, translation integration, and extensibility.

---

## 🧩 Overview

This solution implements all required interview tasks in a structured, configuration-driven .NET 8 console application.  
It demonstrates clean architecture principles, dependency injection (DI), runtime translation using the DeepL API, time-based dynamic greetings, and comprehensive logging and error handling.

---

## ⚙️ Implemented Features

### 1. Configuration via `appsettings.json`

All user-facing text and target language codes are configured in one place:

```json
{
  "Greeting": {
    "Message": "Welcome to the interview app!",
    "Language": "EN"
  }
}
```

- The application validates both values (`Message` and `Language`) before execution.
- No hardcoded text or language values exist in the codebase.

---

### 2. Dependency Injection (DI)

All services are registered via `Host.CreateDefaultBuilder()` in `Program.cs`:

```csharp
services.AddTransient<IGreetingService, GreetingService>();
services.AddTransient<ITimeGreetingService, TimeGreetingService>();
services.AddHttpClient<ITranslationService, DeepLTranslationService>();
```

- Promotes clean separation of concerns.  
- Makes the application highly testable and extensible.

---

### 3. Logging

- Uses `ILogger<T>` across all services for structured logging.  
- Logs:
  - Service startup and configuration load
  - Translation operations and time-based greetings
  - Any missing configuration or API failures  
- Provides full traceability without interrupting execution.

---

### 4. Greeting Service (`GreetingService`)

- Reads the greeting message and language from configuration.  
- Combines a time-based greeting with the configured message.  
- Translates the combined message through the `DeepLTranslationService`.  
- Displays the final translated message in the console.  
- Logs all steps for clarity and debugging.

---

### 5. Time-Based Greeting (`TimeGreetingService`)

- Generates:
  - “Good morning” (before 12 PM)
  - “Good afternoon” (12–6 PM)
  - “Good evening” (after 6 PM)
- Automatically translates the generated greeting based on the configured language.  
- Fully integrated with DI and translation service.

---

### 6. Translation (`DeepLTranslationService`)

- Integrates with **DeepL Free API**:  
  `https://api-free.deepl.com/v2/translate`
- Reads the API key from the environment variable:
  ```bash
  DEEPL_API_KEY=your-key-here
  ```
- Sends JSON requests with:
  - `source_lang = "EN"`
  - `target_lang` from configuration
- Deserializes DeepL responses case-insensitively:
  ```json
  {"translations":[{"detected_source_language":"EN","text":"Bienvenue dans l'application d'entretien !"}]}
  ```
- Logs raw JSON responses for debugging.
- Handles missing keys, network errors, and unsupported languages gracefully.

---

### 7. Error Handling and Fallbacks

- If configuration is missing → logs an error and exits gracefully.  
- If the API key is missing → logs a warning and displays the un-translated text.  
- If translation fails or a language is unsupported → logs a warning and returns the original message.  
- Prevents all unhandled exceptions during normal operation.

---

## 🧱 Optional Enhancements Implemented

- DeepL translation is fully optional (app runs normally without a key).  
- Dynamic time-based greeting fully translatable.  
- Configuration-driven logic — no hardcoded text.  
- Logging at all key execution points.  
- Extensible and SOLID-aligned architecture, ready for further tasks.

---

## 🚀 Next Steps (Planned)

### 1. MediatR Integration
- Add a `GreetUserCommand` and `GreetUserHandler`.
- Use `IMediator.Send()` to execute greetings through MediatR for decoupled logic.

### 2. Unit Testing
- Introduce xUnit or NUnit tests for:
  - `GreetingService`
  - `TimeGreetingService`
  - `DeepLTranslationService`
- Mock `ILogger`, `IConfiguration`, and `ITranslationService`.

### 3. Alternative Translation Providers
- Add support for LibreTranslate or Microsoft Translator as configurable providers.

---

## 🧪 Testing Instructions

### Step 1 – Set the DeepL API Key

**Windows PowerShell:**
```bash
setx DEEPL_API_KEY "your-api-key-here"
```

**macOS / Linux:**
```bash
export DEEPL_API_KEY="your-api-key-here"
```

---

### Step 2 – Run the Application
```bash
dotnet run
```

---

### Step 3 – Expected Output Example
```
info: GreetingService[0]
      GreetingService started.
info: TimeGreetingService[0]
      Time-based greeting translated: Bonjour
info: DeepLTranslationService[0]
      Translated text to FR: Bonjour! Bienvenue dans l'application d'entretien !
Bonjour! Bienvenue dans l'application d'entretien !
```

---

## 🧾 Assumptions and Notes

- **Supported DeepL languages:**
  ```
  EN, DE, FR, ES, IT, NL, PT, RU, JA, ZH,
  SV, FI, KO, NO, DA, CS, EL, HU, ID, TR,
  UK, BG, RO, SK, SL, ET, LV, LT
  ```
- Unsupported codes (e.g., `AF`, `ZU`) fall back to English.
- Default source language is `EN`.
- The application is fully configuration-driven (no hardcoded content).
- Architecture follows a clean service hierarchy:
  - `GreetingService` – orchestrates flow
  - `TimeGreetingService` – provides dynamic greetings
  - `DeepLTranslationService` – performs translations
- Logging ensures full observability for debugging.
- Application built and tested using **.NET 8 SDK**.

---

## ✅ Feature Summary

| Feature                     | Status |
|------------------------------|---------|
| Configuration-driven setup   | ✅ |
| Dependency Injection          | ✅ |
| Logging                       | ✅ |
| Configuration validation      | ✅ |
| Translation (DeepL API)       | ✅ |
| Time-based greeting           | ✅ |
| Graceful error handling       | ✅ |
| Ready for MediatR integration | ✅ |

---

## 📬 Submission Notes

This project is complete up to Step 6 (DeepL integration and time-based translation).  
The next phase will include MediatR integration and unit testing for full decoupled command handling.

---

**End of README**
