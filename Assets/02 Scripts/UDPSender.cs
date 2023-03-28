using UnityEngine;

using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDPSender : MonoBehaviour
{
    IPEndPoint remoteEndPoint;
    UdpClient client;
   
    public string ip = "127.0.0.1"; //상대편 IP. 자기자신은 127.0.0.1
    public int port = 11999; //상대편 포트

    private void Start()
    {   //UDP 셋팅
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        client = new UdpClient();
    }

    private void Update()
    {

    }

    public void sendString(string message)
    {
        try
        {
            //값 보내기
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch
        {

        }
    }
}

