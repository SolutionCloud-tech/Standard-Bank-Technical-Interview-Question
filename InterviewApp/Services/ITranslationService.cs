namespace InterviewApp.Services
{
    public interface ITranslationService
    {
        string Translate(string text, string targetLanguage);
    }
}
