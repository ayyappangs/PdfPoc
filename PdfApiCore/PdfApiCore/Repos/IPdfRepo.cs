namespace PdfApiCore.Repos
{
    public interface IPdfRepo
    {
         Task<List<string>> GetFiles(string containerName);

        Task<List<string>> MergeFiles(string containerName);
    }
}
