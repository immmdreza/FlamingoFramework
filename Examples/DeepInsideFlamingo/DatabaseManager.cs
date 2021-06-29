using System;

namespace DeepInsideFlamingo
{
    public class DatabaseManager: IDisposable
    {
        public DatabaseManager()
        {
            // Generate a random id for db instance
            InstanceId = Guid.NewGuid().ToString("N");
        }

        private bool isConnected = false;

        public bool IsConnected => isConnected;

        public string InstanceId { get; }

        public void Connect()
        {
            isConnected = true;
            Console.WriteLine($"Connected to database service ({InstanceId})");
        }

        public string FetchData()
        {
            if(!isConnected)
            {
                throw new Exception("Not Connected!");
            }

            return "Your lovely data is here";
        }

        public void Disonnect()
        {
            isConnected = false;
            Console.WriteLine($"Closed connection to database service! ({InstanceId})");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
