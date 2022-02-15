using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using IronPdf;

namespace PdfApiCore.Repos
{
    public class PdfRepo: IPdfRepo
    {
        public async Task<List<string>> GetFiles(string containerName)
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=krishkothapallipoc;AccountKey=wF9d4o/KmAp/Ms1Mdwxqb6nwwAFY30uxr6ihd45WmVbeFs/Wlx0/vU2nasjhBUf624dlsS3QyJ+2+ASt2r7Sog==;EndpointSuffix=core.windows.net");
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("1234566778");
                
                var blobs =  containerClient.GetBlobs();
                foreach (BlobItem blobItem in blobs)
                {
                    if (!blobItem.Name.Contains(".pdf"))
                    {
                        BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);
                        var stream = new MemoryStream();
                        await blobClient.DownloadToAsync(stream);
                        byte[] bytes = stream.ToArray();

                        string base64 = Convert.ToBase64String(bytes); 
                        var ImageTag = $"<img src=\"data:image/jpeg;base64, {base64}\"/><br/>";
                        var Renderer = new IronPdf.ChromePdfRenderer();
                        BlobClient blobClient1 = containerClient.GetBlobClient("NewBlob.pdf");
                        blobClient1.Upload(Renderer.RenderHtmlAsPdf(ImageTag).Stream);
                        Dictionary<string, string> tags = new Dictionary<string, string>();
                        tags.Add("Status", "In Progress");
                        blobClient.SetTags(tags);
                        ;
                    }
                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return new List<string>();
        }

        public async Task<List<string>> MergeFiles(string containerName)
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=krishkothapallipoc;AccountKey=wF9d4o/KmAp/Ms1Mdwxqb6nwwAFY30uxr6ihd45WmVbeFs/Wlx0/vU2nasjhBUf624dlsS3QyJ+2+ASt2r7Sog==;EndpointSuffix=core.windows.net");
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("1234566778");

                var blobs = containerClient.GetBlobs();
                List<PdfDocument> lstPdfs = new List<PdfDocument>();
                foreach (BlobItem blobItem in blobs)
                {
                    if (blobItem.Name.Contains(".pdf"))
                    {
                        BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);
                        var stream = new MemoryStream();
                        await blobClient.DownloadToAsync(stream);
                       
                        lstPdfs.Add(new PdfDocument(stream));

                    }

                }
                BlobClient blobClient1 = containerClient.GetBlobClient("Merged.pdf");
                blobClient1.Upload(IronPdf.PdfDocument.Merge(lstPdfs).Stream);
            }
            catch (Exception ex)
            {

                throw;
            }
            
           
            return new List<string>();
        }
    }
}
