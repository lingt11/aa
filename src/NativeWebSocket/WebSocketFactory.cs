using System;

namespace NativeWebSocket
{
	// Token: 0x020004A8 RID: 1192
	public static class WebSocketFactory
	{
		// Token: 0x06001A86 RID: 6790 RVA: 0x000A32DA File Offset: 0x000A14DA
		public static WebSocket CreateInstance(string url)
		{
			return new WebSocket(url, null);
		}
	}
}
