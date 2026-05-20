using System;

namespace NativeWebSocket
{
	// Token: 0x0200049A RID: 1178
	public interface IWebSocket
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001A4B RID: 6731
		// (remove) Token: 0x06001A4C RID: 6732
		event WebSocketOpenEventHandler OnOpen;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001A4D RID: 6733
		// (remove) Token: 0x06001A4E RID: 6734
		event WebSocketMessageEventHandler OnMessage;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001A4F RID: 6735
		// (remove) Token: 0x06001A50 RID: 6736
		event WebSocketErrorEventHandler OnError;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06001A51 RID: 6737
		// (remove) Token: 0x06001A52 RID: 6738
		event WebSocketCloseEventHandler OnClose;

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06001A53 RID: 6739
		WebSocketState State { get; }
	}
}
