namespace p48_lessons_asp.Services.StorageService
{
    public interface IStorageService
    {
        void LoadFile(string filename);
        string GetFile(string filename);
        bool DeleteFile(string filename);
    }
}
