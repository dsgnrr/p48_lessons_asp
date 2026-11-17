namespace p48_lessons_asp.Services.StorageService;

public class ServerFileStorageService : IStorageService
{
    

    public Task<bool> DeleteFileAsync(string url)
    {
        throw new NotImplementedException();
    }

    public Task<string> SaveFileAsync(string entityType, string entityName, IFormFile file, string fileName = null)
    {
        throw new NotImplementedException();
    }
}