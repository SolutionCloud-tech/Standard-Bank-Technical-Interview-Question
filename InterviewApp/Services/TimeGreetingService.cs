using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace InterviewApp.Services
{
    public class TimeGreetingService : ITimeGreetingService
    {
        private readonly ILogger<TimeGreetingService> _logger;
        private readonly ITranslationService _translator;
        private readonly IConfiguration _config;

        public TimeGreetingService(
            ILogger<TimeGreetingService> logger,
            ITranslationService translator,
            IConfiguration config)
        {
            _logger = logger;
            _translator = translator;
            _config = config;
        }

        public string GetTimeGreeting()
        {
            var languageCode = _config["Greeting:Language"] ?? "EN";
            var hour = DateTime.Now.Hour;

            string greeting = hour switch
            {
                < 12 => "Good morning",
                < 18 => "Good afternoon",
                _ => "Good evening"
            };

            var translated = _translator.Translate(greeting, languageCode);
            _logger.LogInformation("Time-based greeting translated: {Greeting}", translated);
            return translated;
        }
    }
}
