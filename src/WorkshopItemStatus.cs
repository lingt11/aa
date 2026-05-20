using System;

// Token: 0x020003D6 RID: 982
[Serializable]
public class WorkshopItemStatus
{
	// Token: 0x0400150E RID: 5390
	public ulong publishedFileId;

	// Token: 0x0400150F RID: 5391
	public string title = string.Empty;

	// Token: 0x04001510 RID: 5392
	public int heroId;

	// Token: 0x04001511 RID: 5393
	public bool subscribed;

	// Token: 0x04001512 RID: 5394
	public bool installed;

	// Token: 0x04001513 RID: 5395
	public bool needsUpdate;

	// Token: 0x04001514 RID: 5396
	public bool downloading;

	// Token: 0x04001515 RID: 5397
	public bool downloadPending;

	// Token: 0x04001516 RID: 5398
	public bool enabledInGame;

	// Token: 0x04001517 RID: 5399
	public string installFolder = string.Empty;

	// Token: 0x04001518 RID: 5400
	public ulong sizeOnDisk;

	// Token: 0x04001519 RID: 5401
	public uint timestamp;

	// Token: 0x0400151A RID: 5402
	public uint remoteTimestamp;

	// Token: 0x0400151B RID: 5403
	public bool updateAvailable;
}
