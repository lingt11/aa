using System;

namespace NativeWebSocket
{
	// Token: 0x0200049D RID: 1181
	public class WebSocketUnexpectedException : WebSocketException
	{
		// Token: 0x06001A59 RID: 6745 RVA: 0x000A1F5E File Offset: 0x000A015E
		public WebSocketUnexpectedException()
		{
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000A1F66 File Offset: 0x000A0166
		public WebSocketUnexpectedException(string message) : base(message)
		{
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000A1F6F File Offset: 0x000A016F
		public WebSocketUnexpectedException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
