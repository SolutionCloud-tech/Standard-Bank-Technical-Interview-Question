using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace InterviewApp.Services
{
    public class GreetingService : IGreetingService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<GreetingService> _logger;
        private readonly ITranslationService _translator;
        private readonly ITimeGreetingService _timeGreeting;


        public GreetingService(
            IConfiguration config,
            ILogger<GreetingService> logger,
            ITranslationService translator,
            ITimeGreetingService timeGreeting)
        {
            _config = config;
            _logger = logger;
            _translator = translator;
            _timeGreeting = timeGreeting;
        }

        public void Run()
        {
            _logger.LogInformation("GreetingService started.");

            var message = _config["Greeting:Message"];
            var languageCode = _config["Greeting:Language"];

            if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(languageCode))
            {
                _logger.LogError("Invalid configuration. 'Greeting:Message' or 'Greeting:Language' is missing.");
                return;
            }

            _logger.LogInformation("Configuration loaded: Message='{Message}', Language='{Language}'", message, languageCode);

            var timeGreeting = _timeGreeting.GetTimeGreeting();
            var combinedMessage = $"{timeGreeting}! {message}";

            // Log pre-translation version
            _logger.LogInformation("Combining greeting and message before translation: {Combined}", combinedMessage);

            // Translate the entire combined text together
            var translatedMessage = _translator.Translate(combinedMessage, languageCode);

            // If translation didn’t change, try translating the time greeting separately
            if (translatedMessage == combinedMessage)
            {
                var translatedTimeGreeting = _translator.Translate(timeGreeting, languageCode);
                translatedMessage = $"{translatedTimeGreeting}! {message}";
            }

            _logger.LogInformation("Final translated message: {Translated}", translatedMessage);
            Console.WriteLine(translatedMessage);


            _logger.LogInformation("Final message displayed: {TranslatedMessage}", translatedMessage);
            Console.WriteLine(translatedMessage);
        }
    }
}
