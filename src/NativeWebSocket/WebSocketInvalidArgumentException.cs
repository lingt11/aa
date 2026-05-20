using System;

namespace NativeWebSocket
{
	// Token: 0x0200049E RID: 1182
	public class WebSocketInvalidArgumentException : WebSocketException
	{
		// Token: 0x06001A5C RID: 6748 RVA: 0x000A1F5E File Offset: 0x000A015E
		public WebSocketInvalidArgumentException()
		{
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000A1F66 File Offset: 0x000A0166
		public WebSocketInvalidArgumentException(string message) : base(message)
		{
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000A1F6F File Offset: 0x000A016F
		public WebSocketInvalidArgumentException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
