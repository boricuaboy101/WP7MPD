
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

public class MpcConnection
{
    Socket _socket = null;
    static ManualResetEvent _clientDone = new ManualResetEvent(false);
    const int TIMEOUT_MILLISECONDS = 5000;
    const int MAX_BUFFER_SIZE = 2048;

    public string Connect(string hostName, int portNumber)
    {
        string result = string.Empty;
        DnsEndPoint host = new DnsEndPoint(hostName, portNumber);
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
        socketEventArg.RemoteEndPoint = host;
        socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
        {
            result = e.SocketError.ToString();
            _clientDone.Set();
        });
        _clientDone.Reset();
        _socket.ConnectAsync(socketEventArg);
        _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
        return result;
    }

    public string Receive()
    {
        string response = "Operation Timeout";
        if (_socket != null)
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
            socketEventArg.SetBuffer(new Byte[MAX_BUFFER_SIZE], 0, MAX_BUFFER_SIZE);
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                if (e.SocketError == SocketError.Success)
                {
                    response = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                    response = response.Trim('\0');
                }
                else
                {
                    response = e.SocketError.ToString();
                }

                _clientDone.Set();
            });
            _clientDone.Reset();

            _socket.ReceiveAsync(socketEventArg);
            _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
        }
        else
        {
            response = "Socket is not initialized";
        }

        return response;
    }


    public string Send(string data)
    {
        string response = "Operation Timeout";
        if (_socket != null)
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
            socketEventArg.UserToken = null;
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                response = e.SocketError.ToString();
                _clientDone.Set();
            });
            byte[] payload = Encoding.UTF8.GetBytes(data);
            socketEventArg.SetBuffer(payload, 0, payload.Length);
            _clientDone.Reset();
            _socket.SendAsync(socketEventArg);
            _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
        }
        else
        {
            response = "Socket is not initialized";
        }

        return response;
    }


    public void Close()
    {
        if (_socket != null)
        {
            _socket.Close();
        }
    }
}