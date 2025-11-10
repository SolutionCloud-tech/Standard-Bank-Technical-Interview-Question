# InterviewApp – .NET 8 Console Application

**Candidate:** Jason Pierre Renaud  
**Project:** InterviewApp (.NET 8 Console Application)  
**Purpose:** Technical assessment implementation demonstrating clean architecture, dependency injection, configuration management, logging, translation, and extensibility.

---

## 🧩 Overview

This solution implements all core interview requirements in a structured .NET 8 console application.  
It demonstrates configuration-driven design, dependency injection (DI), structured logging, runtime translation using the DeepL API, and dynamic time-based greetings.

---

## ⚙️ Implemented Features

### 1. Configuration via `appsettings.json`

All user-facing text and target language codes come from configuration:

```json
{
  "Greeting": {
    "Message": "Welcome to the interview app!",
    "Language": "EN"
  }
}
