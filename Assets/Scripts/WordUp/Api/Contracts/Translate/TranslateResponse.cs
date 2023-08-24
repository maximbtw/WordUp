namespace WordUp.Api.Contracts.Translate
{
    public class TranslateResponse : IApiOperationResult
    {
        public string TranslatedText { get; set; }
        
        public bool IsSuccess { get; set; }
        
        public string Error { get; set; }
    }
}