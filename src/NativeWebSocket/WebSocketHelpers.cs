using System;

namespace NativeWebSocket
{
	// Token: 0x0200049B RID: 1179
	public static class WebSocketHelpers
	{
		// Token: 0x06001A54 RID: 6740 RVA: 0x000A1E8D File Offset: 0x000A008D
		public static WebSocketCloseCode ParseCloseCodeEnum(int closeCode)
		{
			if (Enum.IsDefined(typeof(WebSocketCloseCode), closeCode))
			{
				return (WebSocketCloseCode)closeCode;
			}
			return WebSocketCloseCode.Undefined;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000A1EB0 File Offset: 0x000A00B0
		public static WebSocketException GetErrorMessageFromCode(int errorCode, Exception inner)
		{
			switch (errorCode)
			{
			case -7:
				return new WebSocketInvalidArgumentException("Cannot close WebSocket. An invalid code was specified or reason is too long.", inner);
			case -6:
				return new WebSocketInvalidStateException("WebSocket is not in open state.", inner);
			case -5:
				return new WebSocketInvalidStateException("WebSocket is already closed.", inner);
			case -4:
				return new WebSocketInvalidStateException("WebSocket is already closing.", inner);
			case -3:
				return new WebSocketInvalidStateException("WebSocket is not connected.", inner);
			case -2:
				return new WebSocketInvalidStateException("WebSocket is already connected or in connecting state.", inner);
			case -1:
				return new WebSocketUnexpectedException("WebSocket instance not found.", inner);
			default:
				return new WebSocketUnexpectedException("Unknown error.", inner);
			}
		}
	}
}
