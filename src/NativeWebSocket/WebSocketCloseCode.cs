using System;

namespace NativeWebSocket
{
	// Token: 0x02000498 RID: 1176
	public enum WebSocketCloseCode
	{
		// Token: 0x0400196E RID: 6510
		NotSet,
		// Token: 0x0400196F RID: 6511
		Normal = 1000,
		// Token: 0x04001970 RID: 6512
		Away,
		// Token: 0x04001971 RID: 6513
		ProtocolError,
		// Token: 0x04001972 RID: 6514
		UnsupportedData,
		// Token: 0x04001973 RID: 6515
		Undefined,
		// Token: 0x04001974 RID: 6516
		NoStatus,
		// Token: 0x04001975 RID: 6517
		Abnormal,
		// Token: 0x04001976 RID: 6518
		InvalidData,
		// Token: 0x04001977 RID: 6519
		PolicyViolation,
		// Token: 0x04001978 RID: 6520
		TooBig,
		// Token: 0x04001979 RID: 6521
		MandatoryExtension,
		// Token: 0x0400197A RID: 6522
		ServerError,
		// Token: 0x0400197B RID: 6523
		TlsHandshakeFailure = 1015
	}
}
