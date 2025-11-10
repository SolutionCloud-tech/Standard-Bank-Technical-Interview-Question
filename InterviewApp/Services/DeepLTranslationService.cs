using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace InterviewApp.Services
{
    public class DeepLTranslationService : ITranslationService
    {
        private readonly HttpClient _http;
        private readonly ILogger<DeepLTranslationService> _logger;
        private readonly string _apiKey;

        public DeepLTranslationService(HttpClient http, ILogger<DeepLTranslationService> logger)
        {
            _http = http;
            _logger = logger;
            _apiKey = Environment.GetEnvironmentVariable("DEEPL_API_KEY") ?? string.Empty;

            if (string.IsNullOrEmpty(_apiKey))
            {
                _logger.LogWarning("DeepL API key not found. Translation will be skipped.");
            }
            else
            {
                _logger.LogInformation("DeepL translation service initialized successfully.");
            }
        }

        public string Translate(string text, string targetLanguage)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(targetLanguage))
            {
                _logger.LogWarning("Text or target language missing for translation.");
                return text;
            }

            if (string.IsNullOrEmpty(_apiKey))
            {
                _logger.LogWarning("DeepL API key missing. Returning original text.");
                return text;
            }

            try
            {
                var requestBody = new
                {
                    text = new[] { text },
                    source_lang = "EN",                    // Explicitly specify English
                    target_lang = targetLanguage.ToUpper() // For example "DE"
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Sending translation request to DeepL API...");

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api-free.deepl.com/v2/translate");
                request.Headers.Add("Authorization", $"DeepL-Auth-Key {_apiKey}");
                request.Content = content;

                var response = _http.Send(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("DeepL returned {StatusCode}.", response.StatusCode);
                    return text;
                }

                var result = response.Content.ReadAsStringAsync().Result;
                _logger.LogInformation("Raw DeepL response: {Response}", result);

                var translationResponse = JsonSerializer.Deserialize<DeepLResponse>(
                    result,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
                var translated = translationResponse?.Translations?.FirstOrDefault()?.Text ?? text;

                _logger.LogInformation("Translated text to {Lang}: {Translated}", targetLanguage, translated);
                return translated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeepL translation error.");
                return text;
            }
        }


        private class DeepLResponse
        {
            public List<Translation>? Translations { get; set; }
        }

        private class Translation
        {
            public string? Text { get; set; }
        }
    }
}
