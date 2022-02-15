using Azure.Storage.Blobs;
using PdfApiCore.Repos;

namespace PdfApiCore.Service
{
    public class PdfService: IPdfService
    {
        private readonly IPdfRepo pdfRepo;

        public PdfService(IPdfRepo pdfRepo)
        {
            this.pdfRepo = pdfRepo;
        }

        public async Task CreatePdf()
        {            
            await pdfRepo.GetFiles("test");
        }

        public async Task MergedPdf()
        {
            await pdfRepo.MergeFiles("");
        }
    }
}
