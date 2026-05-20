using System;
using Mirror;

// Token: 0x020003E3 RID: 995
public struct ClientNetMessage : NetworkMessage
{
	// Token: 0x0400157C RID: 5500
	public ClientNetOperation clientNetOperation;

	// Token: 0x0400157D RID: 5501
	public int[] datas;

	// Token: 0x0400157E RID: 5502
	public int data;

	// Token: 0x0400157F RID: 5503
	public string[] strs;
}
