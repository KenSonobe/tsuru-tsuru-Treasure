using UnityEngine;
using System.Collections;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

public class Server: MonoBehaviour {

	WebSocketServer server;

	void Start ()
	{
		server = new WebSocketServer(3000);

		server.AddWebSocketService<Echo>("/");
		server.Start();
	}

	void OnDestroy()
	{
		server.Stop();
		server = null;
	}

}

public class Echo : WebSocketBehavior
{
	public int send;
	protected override void OnMessage (MessageEventArgs e)
	{
		send++;
		Sessions.Broadcast(e.Data);
	}
}