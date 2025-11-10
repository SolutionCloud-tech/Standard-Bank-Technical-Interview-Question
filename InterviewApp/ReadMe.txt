============================================================
                 INTERVIEW APP - .NET 8 CONSOLE
============================================================

Candidate : Jason Pierre Renaud
Project   : InterviewApp (.NET 8 Console Application)
Purpose   : Technical assessment implementation demonstrating
            clean architecture, dependency injection, logging,
            configuration, translation, and extensibility.

============================================================
                    1. OVERVIEW
============================================================

This solution implements all required interview tasks in a 
structured .NET 8 Console Application. 

It demonstrates configuration-driven design, dependency 
injection (DI), structured logging, runtime translation using 
DeepL API, and time-based dynamic greetings.

============================================================
                2. IMPLEMENTED FEATURES
============================================================

[1] Configuration via appsettings.json
    -----------------------------------
    • Greeting message and language code come entirely from:
        {
          "Greeting": {
            "Message": "Welcome to the interview app!",
            "Language": "EN"
          }
        }
    • Both values are validated at startup.

[2] Dependency Injection (DI)
    ---------------------------
    • Registered using Host.CreateDefaultBuilder():
        services.AddTransient<IGreetingService, GreetingService>();
        services.AddTransient<ITimeGreetingService, TimeGreetingService>();
        services.AddHttpClient<ITranslationService, DeepLTranslationService>();
    • Promotes separation of concerns and testability.

[3] Logging
    --------
    • ILogger<T> used across all services.
    • Logs configuration load, translation events, time-based logic,
      and all fallback behaviors.

[4] GreetingService
    ----------------
    • Reads Message and Language from configuration.
    • Combines a time-based greeting and translates the full string.
    • Displays the translated text to the console.
    • Fully validated and logged at each step.

[5] TimeGreetingService
    --------------------
    • Generates “Good morning”, “Good afternoon”, or “Good evening”
      based on system time.
    • Uses the same configured language for automatic translation.
    • Logged and translatable independently.

[6] DeepLTranslationService
    ------------------------
    • Integrates with DeepL Free API:
        https://api-free.deepl.com/v2/translate
    • Reads API key from environment variable:
        DEEPL_API_KEY=your-key-here
    • Sends JSON body with:
        - source_lang = "EN"
        - target_lang = code from configuration
    • Deserializes DeepL response (case-insensitive).
    • Handles HTTP errors and unsupported languages gracefully.

[7] Error Handling & Fallbacks
    ----------------------------
    • Missing config values → Logs error, exits safely.
    • Missing API key → Logs warning, skips translation.
    • Unsupported language → Warns, shows original English text.
    • No hardcoded strings for message or language.

============================================================
               3. OPTIONAL ENHANCEMENTS
============================================================

    • DeepL translation fully optional (runs without API key).
    • Dynamic time-based greetings with translation.
    • Clear, extensible service layout (SOLID principles).
    • Ready for MediatR integration and unit testing.

============================================================
               4. NEXT STEPS (PLANNED)
============================================================

[1] MediatR Integration
    - Implement GreetUserCommand + handler.
    - Trigger greetings via mediator.Send() for decoupled execution.

[2] Unit Testing
    - Add xUnit/NUnit tests for each service.
    - Mock ITranslationService, ILogger, and IConfiguration.

[3] Extend Translation Providers
    - Add support for LibreTranslate or Microsoft Translator.

============================================================
              5. TESTING INSTRUCTIONS
============================================================

Step 1: Set the DeepL API Key
    Windows PowerShell:
        setx DEEPL_API_KEY "your-api-key-here"

    macOS/Linux:
        export DEEPL_API_KEY="your-api-key-here"

Step 2: Run the Application
    dotnet run

Step 3: Example Output
    ----------------------------------------
    info: GreetingService[0]
          GreetingService started.
    info: TimeGreetingService[0]
          Time-based greeting translated: Bonjour
    info: DeepLTranslationService[0]
          Translated text to FR:
          Bonjour! Bienvenue dans l'application d'entretien !
    Bonjour! Bienvenue dans l'application d'entretien !
    ----------------------------------------

============================================================
              6. ASSUMPTIONS AND NOTES
============================================================

• DeepL Free API supports:
  EN, DE, FR, ES, IT, NL, PT, RU, JA, ZH,
  SV, FI, KO, NO, DA, CS, EL, HU, ID, TR,
  UK, BG, RO, SK, SL, ET, LV, LT

• Unsupported codes (e.g., AF, ZU) revert to English.
• Default source language: EN.
• Config-only logic: all text and language from appsettings.json.
• Architecture follows clean layering:
  - GreetingService → Orchestrates execution
  - TimeGreetingService → Provides time-based greeting
  - DeepLTranslationService → Handles translation and API comms
• Application never crashes from missing API key or network issues.
• Built and tested with .NET 8 SDK.

============================================================
                     END OF DOCUMENT
============================================================
