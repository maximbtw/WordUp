namespace WordUp.Api
{
    public interface IApiOperationResult
    {
        public bool IsSuccess { get; set; }
        
        public string Error { get; set; }
    }
}