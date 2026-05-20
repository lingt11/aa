using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NativeWebSocket
{
	// Token: 0x020004A2 RID: 1186
	public class WebSocket : IWebSocket
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001A67 RID: 6759 RVA: 0x000A1FC8 File Offset: 0x000A01C8
		// (remove) Token: 0x06001A68 RID: 6760 RVA: 0x000A2000 File Offset: 0x000A0200
		public event WebSocketOpenEventHandler OnOpen;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001A69 RID: 6761 RVA: 0x000A2038 File Offset: 0x000A0238
		// (remove) Token: 0x06001A6A RID: 6762 RVA: 0x000A2070 File Offset: 0x000A0270
		public event WebSocketMessageEventHandler OnMessage;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001A6B RID: 6763 RVA: 0x000A20A8 File Offset: 0x000A02A8
		// (remove) Token: 0x06001A6C RID: 6764 RVA: 0x000A20E0 File Offset: 0x000A02E0
		public event WebSocketErrorEventHandler OnError;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06001A6D RID: 6765 RVA: 0x000A2118 File Offset: 0x000A0318
		// (remove) Token: 0x06001A6E RID: 6766 RVA: 0x000A2150 File Offset: 0x000A0350
		public event WebSocketCloseEventHandler OnClose;

		// Token: 0x06001A6F RID: 6767 RVA: 0x000A2188 File Offset: 0x000A0388
		public WebSocket(string url, Dictionary<string, string> headers = null)
		{
			this.uri = new Uri(url);
			if (headers == null)
			{
				this.headers = new Dictionary<string, string>();
			}
			else
			{
				this.headers = headers;
			}
			this.subprotocols = new List<string>();
			string scheme = this.uri.Scheme;
			if (!scheme.Equals("ws") && !scheme.Equals("wss"))
			{
				throw new ArgumentException("Unsupported protocol: " + scheme);
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000A2244 File Offset: 0x000A0444
		public WebSocket(string url, string subprotocol, Dictionary<string, string> headers = null)
		{
			this.uri = new Uri(url);
			if (headers == null)
			{
				this.headers = new Dictionary<string, string>();
			}
			else
			{
				this.headers = headers;
			}
			this.subprotocols = new List<string>
			{
				subprotocol
			};
			string scheme = this.uri.Scheme;
			if (!scheme.Equals("ws") && !scheme.Equals("wss"))
			{
				throw new ArgumentException("Unsupported protocol: " + scheme);
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000A2308 File Offset: 0x000A0508
		public WebSocket(string url, List<string> subprotocols, Dictionary<string, string> headers = null)
		{
			this.uri = new Uri(url);
			if (headers == null)
			{
				this.headers = new Dictionary<string, string>();
			}
			else
			{
				this.headers = headers;
			}
			this.subprotocols = subprotocols;
			string scheme = this.uri.Scheme;
			if (!scheme.Equals("ws") && !scheme.Equals("wss"))
			{
				throw new ArgumentException("Unsupported protocol: " + scheme);
			}
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000A23BE File Offset: 0x000A05BE
		public void CancelConnection()
		{
			CancellationTokenSource tokenSource = this.m_TokenSource;
			if (tokenSource == null)
			{
				return;
			}
			tokenSource.Cancel();
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x000A23D0 File Offset: 0x000A05D0
		public Task Connect()
		{
			WebSocket.<Connect>d__27 <Connect>d__;
			<Connect>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Connect>d__.<>4__this = this;
			<Connect>d__.<>1__state = -1;
			<Connect>d__.<>t__builder.Start<WebSocket.<Connect>d__27>(ref <Connect>d__);
			return <Connect>d__.<>t__builder.Task;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x000A2414 File Offset: 0x000A0614
		public WebSocketState State
		{
			get
			{
				switch (this.m_Socket.State)
				{
				case WebSocketState.Connecting:
					return WebSocketState.Connecting;
				case WebSocketState.Open:
					return WebSocketState.Open;
				case WebSocketState.CloseSent:
				case WebSocketState.CloseReceived:
					return WebSocketState.Closing;
				case WebSocketState.Closed:
					return WebSocketState.Closed;
				default:
					return WebSocketState.Closed;
				}
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x000A2454 File Offset: 0x000A0654
		public Task Send(byte[] bytes)
		{
			return this.SendMessage(this.sendBytesQueue, WebSocketMessageType.Binary, new ArraySegment<byte>(bytes));
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x000A246C File Offset: 0x000A066C
		public Task SendText(string message)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			return this.SendMessage(this.sendTextQueue, WebSocketMessageType.Text, new ArraySegment<byte>(bytes, 0, bytes.Length));
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000A249C File Offset: 0x000A069C
		private Task SendMessage(List<ArraySegment<byte>> queue, WebSocketMessageType messageType, ArraySegment<byte> buffer)
		{
			WebSocket.<SendMessage>d__32 <SendMessage>d__;
			<SendMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendMessage>d__.<>4__this = this;
			<SendMessage>d__.queue = queue;
			<SendMessage>d__.messageType = messageType;
			<SendMessage>d__.buffer = buffer;
			<SendMessage>d__.<>1__state = -1;
			<SendMessage>d__.<>t__builder.Start<WebSocket.<SendMessage>d__32>(ref <SendMessage>d__);
			return <SendMessage>d__.<>t__builder.Task;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000A24F8 File Offset: 0x000A06F8
		private Task HandleQueue(List<ArraySegment<byte>> queue, WebSocketMessageType messageType)
		{
			WebSocket.<HandleQueue>d__33 <HandleQueue>d__;
			<HandleQueue>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<HandleQueue>d__.<>4__this = this;
			<HandleQueue>d__.queue = queue;
			<HandleQueue>d__.messageType = messageType;
			<HandleQueue>d__.<>1__state = -1;
			<HandleQueue>d__.<>t__builder.Start<WebSocket.<HandleQueue>d__33>(ref <HandleQueue>d__);
			return <HandleQueue>d__.<>t__builder.Task;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000A254C File Offset: 0x000A074C
		public void DispatchMessageQueue()
		{
			if (this.m_MessageList.Count == 0)
			{
				return;
			}
			object incomingMessageLock = this.IncomingMessageLock;
			List<byte[]> list;
			lock (incomingMessageLock)
			{
				list = new List<byte[]>(this.m_MessageList);
				this.m_MessageList.Clear();
			}
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				WebSocketMessageEventHandler onMessage = this.OnMessage;
				if (onMessage != null)
				{
					onMessage(list[i]);
				}
			}
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000A25DC File Offset: 0x000A07DC
		public Task Receive()
		{
			WebSocket.<Receive>d__36 <Receive>d__;
			<Receive>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Receive>d__.<>4__this = this;
			<Receive>d__.<>1__state = -1;
			<Receive>d__.<>t__builder.Start<WebSocket.<Receive>d__36>(ref <Receive>d__);
			return <Receive>d__.<>t__builder.Task;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000A2620 File Offset: 0x000A0820
		public Task Close()
		{
			WebSocket.<Close>d__37 <Close>d__;
			<Close>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Close>d__.<>4__this = this;
			<Close>d__.<>1__state = -1;
			<Close>d__.<>t__builder.Start<WebSocket.<Close>d__37>(ref <Close>d__);
			return <Close>d__.<>t__builder.Task;
		}

		// Token: 0x04001987 RID: 6535
		private Uri uri;

		// Token: 0x04001988 RID: 6536
		private Dictionary<string, string> headers;

		// Token: 0x04001989 RID: 6537
		private List<string> subprotocols;

		// Token: 0x0400198A RID: 6538
		private ClientWebSocket m_Socket = new ClientWebSocket();

		// Token: 0x0400198B RID: 6539
		private CancellationTokenSource m_TokenSource;

		// Token: 0x0400198C RID: 6540
		private CancellationToken m_CancellationToken;

		// Token: 0x0400198D RID: 6541
		private readonly object OutgoingMessageLock = new object();

		// Token: 0x0400198E RID: 6542
		private readonly object IncomingMessageLock = new object();

		// Token: 0x0400198F RID: 6543
		private bool isSending;

		// Token: 0x04001990 RID: 6544
		private List<ArraySegment<byte>> sendBytesQueue = new List<ArraySegment<byte>>();

		// Token: 0x04001991 RID: 6545
		private List<ArraySegment<byte>> sendTextQueue = new List<ArraySegment<byte>>();

		// Token: 0x04001992 RID: 6546
		private List<byte[]> m_MessageList = new List<byte[]>();
	}
}
