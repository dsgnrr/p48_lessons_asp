namespace p48_lessons_asp.Services.StorageService
{
    public class S3Service:IStorageService
    {
        //public S3Service(string API_KEY)
        //{
        //    Console.WriteLine("S3 succesfully conected");
        //}

        public void LoadFile(string filename)
        {
            Console.WriteLine("File succesffuly loaded on S3");
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
