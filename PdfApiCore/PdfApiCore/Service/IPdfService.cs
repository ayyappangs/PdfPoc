namespace PdfApiCore.Service
{
    public interface IPdfService
    {
        Task CreatePdf();

        Task MergedPdf();
    }
}
