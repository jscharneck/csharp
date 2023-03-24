using System.Collections;
using System.Net;
using System.Net.Sockets;

public class Program
{
    // private ArrayList alSockets;
    private List<Socket> alSockets;

    public Program()
    {
        // alSockets = new ArrayList();
        alSockets = new List<Socket>();


        IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
        // lblStatus.Text = "My IP address is " +
        IPHost.AddressList[0].ToString();
        Thread thdListener = new Thread(new
        ThreadStart(listenerThread));
        thdListener.Start();
    }

    public void listenerThread()
    {
        // TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 9000);
        TcpListener tcpListener = new TcpListener(Dns.GetHostAddresses(Dns.GetHostName())[3], 9000);
        tcpListener.Start();
        while (true)
        {
            Console.WriteLine("Waiting for connection...");
            Socket handlerSocket = tcpListener.AcceptSocket();
            if (handlerSocket.Connected)
            {

                Console.WriteLine(" connected.");
                lock (this)
                {
                    alSockets.Add(handlerSocket);
                }
                ThreadStart thdstHandler = new ThreadStart(handlerThread);
                Thread thdHandler = new Thread(thdstHandler);
                thdHandler.Start();
            }
        }
    }

    public void handlerThread()
    {
        Socket handlerSocket = (Socket)alSockets[alSockets.Count - 1];
        NetworkStream networkStream = new NetworkStream(handlerSocket);
        int thisRead = 0;
        int blockSize = 1024;
        Byte[] dataByte = new Byte[blockSize];
        lock (this)
        {
            // Only one process can access
            // the same file at any given time
            // Stream fileStream = File.OpenWrite(@"dalio01_received" + DateTime.Now.Millisecond + ".jpg");
            Stream fileStream = File.OpenWrite(@"receivedfiles\file_" + DateTime.Now.Millisecond + ".jpg");

            while (true)
            {
                thisRead = networkStream.Read(dataByte, 0, blockSize);
                fileStream.Write(dataByte, 0, thisRead);
                if (thisRead == 0) break;
            }
            fileStream.Close();
        }
        Console.WriteLine("File Written");
        handlerSocket = null;
    }


    public static void Main(String[] args)
    {

        new Program();
    }

}