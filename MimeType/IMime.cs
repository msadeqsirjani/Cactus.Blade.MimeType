namespace Cactus.Blade.MimeType
{
    public interface IMime
    {
        string this[string extension] { get; }
    }
}
