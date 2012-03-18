

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Libmpc;



public delegate void MpcConnectionEventDelegate(MpcConnection connection);

public class MpcConnection
{


    private static readonly string FIRST_LINE_PREFIX = "OK MPD ";

    private static readonly string OK = "OK";
    private static readonly string ACK = "ACK";

    private static readonly Regex ACK_REGEX = new Regex("^ACK \\[(?<code>[0-9]*)@(?<nr>[0-9]*)] \\{(?<command>[a-z]*)} (?<message>.*)$");
    Socket _socket = null;
    static ManualResetEvent _clientDone = new ManualResetEvent(false);
    const int TIMEOUT_MILLISECONDS = 5000;
    const int MAX_BUFFER_SIZE = 32768;
    public MpcConnection() { }

    /// <summary>
    /// Connects to the MPD server who's dnsendpoint was set in the Server property.
    /// </summary>
    /// <exception cref="InvalidOperationException">If no dnsendpoint was set to the Server property.</exception>
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
    /// <summary>
    /// Disconnects from the current MPD server.
    /// </summary>
    public void Disconnect()
    {
        if (_socket != null)
        {
            _socket.Close();
        }
    }
    /// <summary>
    /// Executes a simple command without arguments on the MPD server and returns the response.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <returns>The MPD server response parsed into a basic object.</returns>
    /// <exception cref="ArgumentException">If the command contains a space of a newline charakter.</exception>
    public MpdResponse Exec(string command)
    {
       
        if (_socket != null)
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
            socketEventArg.UserToken = null;
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                _clientDone.Set();
            });
            byte[] payload = Encoding.UTF8.GetBytes(command);
            socketEventArg.SetBuffer(payload, 0, payload.Length);
            _clientDone.Reset();
            _socket.SendAsync(socketEventArg);
            _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
        }
        return this.readResponse();
    }

    public MpdResponse Exec(string command, string[] argument)
    {
        
       

        if (_socket != null)
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
            socketEventArg.UserToken = null;
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                _clientDone.Set();
            });
            foreach (string arg in argument)
            {
                command = command + ' ' + arg;
            }
            byte[] payload = Encoding.UTF8.GetBytes(command);
            socketEventArg.SetBuffer(payload, 0, payload.Length);
            _clientDone.Reset();
            _socket.SendAsync(socketEventArg);
            _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
        }
        return this.readResponse();
   
    }

    public MpdResponse readResponse()
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
        StringReader reader = new StringReader(response);
        List<string> ret = new List<string>();
        string line = reader.ReadLine();
        while (!(line.Equals(OK) || line.Equals(FIRST_LINE_PREFIX) || line.StartsWith(ACK))) 
        {
            ret.Add(line);
            line = reader.ReadLine();
            if (String.IsNullOrEmpty(line))
            {
                line = "OK";
            }
        }
        if (line.Equals(OK) || line.Equals(FIRST_LINE_PREFIX))
        {
            ret.Add(line);
            return new MpdResponse(new ReadOnlyCollection<string>(ret));
        }
        else
        {
            Match match = ACK_REGEX.Match(line);


            return new MpdResponse(
                int.Parse(match.Result("${code}")),
                int.Parse(match.Result("${nr}")),
                match.Result("${command}"),
                match.Result("${message}"),
                new ReadOnlyCollection<string>(ret)
                );
        }
        throw new NotImplementedException();
    }

}






