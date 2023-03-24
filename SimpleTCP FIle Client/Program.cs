using System.Net;
using System.Net.Sockets;

public class Program
{

    public static void Main(String[] args)
    {
        List<string> filesList = new List<string>();

        string path = @"files/";

        string[] fileEntries = Directory.GetFiles(path);

        foreach (string fileName in fileEntries)
        {
            filesList.Add(Path.GetFileName(fileName));
        }

        // string file01 = @"boo_and_baba.jpg";
        // string file02 = @"dalio01.jpg";

        //convert string to byte array to pass in a name of file

        // Console.WriteLine("Dns.GetHostAddresses :" + Dns.GetHostAddresses(Dns.GetHostName())[0]);
        // Console.WriteLine("Dns.GetHostAddresses :" + Dns.GetHostAddresses(Dns.GetHostName())[1]);
        // Console.WriteLine("Dns.GetHostAddresses :" + Dns.GetHostAddresses(Dns.GetHostName())[2]);
        Console.WriteLine("Dns.GetHostAddresses :" + Dns.GetHostAddresses(Dns.GetHostName())[3]);
        // Console.WriteLine("Dns.GetHostAddresses :" + Dns.GetHostAddresses(Dns.GetHostName())[4]);



        foreach (var file in filesList)
        {
            TcpClient clientSocket = new TcpClient(Dns.GetHostAddresses(Dns.GetHostName())[3].ToString(), 9000);

            Stream fileStream = File.OpenRead(path + file);
            // Console.WriteLine("My FileStream: " + fileStream.Length);

            byte[] fileBuffer = new byte[fileStream.Length];

            fileStream.Read(fileBuffer, 0, (int)fileStream.Length);
            // Stream.Read() reads/passes data into fileBuffer
            // An array of bytes. 
            // When this method returns, the buffer contains the specified byte array with the values...
            // between offset and (offset + count - 1) replaced by the bytes read from the current source.

            // Console.WriteLine("IPAddress.Loopback: " + IPAddress.Loopback);
            // Console.WriteLine("IPAddress.Broadcast: " + IPAddress.Broadcast);
            // Console.WriteLine("IPAddress.None: " + IPAddress.None);
            // Console.WriteLine("IPAddress.Any: " + IPAddress.Any);


            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Write(fileBuffer, 0, fileBuffer.Length);
            networkStream.Close();


        }

        // networkStream.Write(fileBuffer, 0, fileBuffer.GetLength(0));




    }

}