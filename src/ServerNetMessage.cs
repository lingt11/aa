using System;
using Mirror;

// Token: 0x020003E2 RID: 994
public struct ServerNetMessage : NetworkMessage
{
	// Token: 0x04001579 RID: 5497
	public ServerNetOperation serverNetOperation;

	// Token: 0x0400157A RID: 5498
	public int[] datas;

	// Token: 0x0400157B RID: 5499
	public string strData;
}
