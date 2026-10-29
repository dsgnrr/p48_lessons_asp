namespace p48_lessons_asp.Services.StorageService
{
    public class AzureStorageAccount:IStorageService
    {
        //public AzureStorageAccount(string API_KEY)
        //{
        //    Console.WriteLine("S3 succesfully conected");
        //}

        public void LoadFile(string filename)
        {
            Console.WriteLine("File succesffuly loaded on AZURE");
        }

        public string GetFile(string fileID)
        {
            return $"File: {Guid.NewGuid()}";
        }

        public bool DeleteFile(string fileID)
        {
            Console.WriteLine("File succesffuly deleted");
            return true;
        }
    }
}
