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
