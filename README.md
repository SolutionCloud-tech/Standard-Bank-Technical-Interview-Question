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
