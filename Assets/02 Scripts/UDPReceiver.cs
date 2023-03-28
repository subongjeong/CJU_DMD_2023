using System;
using System.Net;
using System.Net.Sockets; //소켓통신을 사용하기 위해서
using System.Threading; //쓰레드를 쓰기 위해
using UnityEngine;

public class UDPReceiver : MonoBehaviour
{
    Thread receiveThread; //쓰레드
    UdpClient client = null; //UDP 클라이언트 제작
    public int port = 11999; //포트는 그대로 사용해도 되고 바꿔도 됩니다.

    //public SimpleSampleCharacterControl control;

    void Start()
    {
        try
        {
            //ReceiveData()라는 함수를 쓰레드에 등록하는 부분
            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        catch (Exception err)
        {
            //에러가 났을 경우 내용 출력
            Debug.Log(err.ToString());
        }
    }

    //데이터를 받는 이 함수는 쓰레드에서 계속 작동함
    private void ReceiveData()
    {
        //UDP 클라이언트 셋팅
        client = new UdpClient(port);
        while (receiveThread.IsAlive)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP); //데이터를 data라는 byte배열에 저장

                //Debug.Log((char)data[0] + "/" + (int)data[0]); //byte배열 첫번째 값을 Char, Int 두가지 변수 형태로 출력함
                Debug.Log(System.Text.Encoding.UTF8.GetString(data)); //받은 데이터를 string으로 변환해서 출력함

                //첫번째 값이 x면 작동
                if(data[0] == 'x')
                {                    
                    
                }
            }
            catch (Exception err)
            {
                Debug.Log(err.ToString());
            }
        }
    }

    public void OnApplicationQuit()
    {
        //쓰레드를 먼저 중지시키고
        receiveThread.Abort();
        receiveThread = null;
        Debug.Log("receiveThread.Abort()");

        //클라이언트를 닫음
        client.Close();
        client = null;
        Debug.Log("UDPReceive.Close()");
    }
}
