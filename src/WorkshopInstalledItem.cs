using System;

// Token: 0x020003D7 RID: 983
[Serializable]
public class WorkshopInstalledItem
{
	// Token: 0x0400151C RID: 5404
	public ulong publishedFileId;

	// Token: 0x0400151D RID: 5405
	public string installFolder = string.Empty;

	// Token: 0x0400151E RID: 5406
	public string manifestPath = string.Empty;

	// Token: 0x0400151F RID: 5407
	public ulong sizeOnDisk;

	// Token: 0x04001520 RID: 5408
	public uint timestamp;

	// Token: 0x04001521 RID: 5409
	public LocalWorkshopManifest manifest;
}
