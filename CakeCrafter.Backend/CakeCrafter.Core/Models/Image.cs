namespace CakeCrafter.Core.Models
{
    public class Image : IDisposable, IAsyncDisposable
    {
        public Image(Stream content, string extension = ".jpg")
        {
            Content = content;
            Extension = extension;
        }
        public Guid Id { get; } = Guid.NewGuid();
        public string Extension { get; }

        public Stream Content { get; }

        public string Name => $"{Id}{Extension}";

        public void Dispose()
        {
            Content?.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (Content != null) await Content.DisposeAsync();
        }
    }
}
