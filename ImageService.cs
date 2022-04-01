using Azure.Identity;
using Azure.Storage.Blobs;

public class ImageService
{
    private readonly BlobContainerClient _client;
    private readonly Uri imageContainerUri;
    public ImageService(IConfiguration config)
    {
        imageContainerUri = new Uri(config["ImageContainerUrl"]);
        var cred = new VisualStudioCodeCredential();
        _client = new BlobContainerClient(imageContainerUri, cred);
    }
    public Task Upload(String path, Stream stream) =>
        _client.UploadBlobAsync(path, stream);

    public IAsyncEnumerable<string> List() =>
        _client.GetBlobsAsync().Select(blob => new Uri(imageContainerUri, "images/" + blob.Name).ToString());

}