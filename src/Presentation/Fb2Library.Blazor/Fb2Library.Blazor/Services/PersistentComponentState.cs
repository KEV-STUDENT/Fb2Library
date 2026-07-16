namespace Fb2Library.Blazor.Services
{
    public class AppState
    {
        public string? CurrentUser { get; set; }
        public bool IsLoading { get; set; }
        public Dictionary<string, object> Cache { get; set; } = new();

        public event Action? OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}
