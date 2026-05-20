using System;

namespace NativeWebSocket
{
	// Token: 0x0200049F RID: 1183
	public class WebSocketInvalidStateException : WebSocketException
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x000A1F5E File Offset: 0x000A015E
		public WebSocketInvalidStateException()
		{
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000A1F66 File Offset: 0x000A0166
		public WebSocketInvalidStateException(string message) : base(message)
		{
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000A1F6F File Offset: 0x000A016F
		public WebSocketInvalidStateException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
