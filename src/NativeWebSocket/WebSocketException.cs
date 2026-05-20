using System;

namespace NativeWebSocket
{
	// Token: 0x0200049C RID: 1180
	public class WebSocketException : Exception
	{
		// Token: 0x06001A56 RID: 6742 RVA: 0x000A1F43 File Offset: 0x000A0143
		public WebSocketException()
		{
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000A1F4B File Offset: 0x000A014B
		public WebSocketException(string message) : base(message)
		{
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000A1F54 File Offset: 0x000A0154
		public WebSocketException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
