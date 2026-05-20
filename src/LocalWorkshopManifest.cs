using System;

// Token: 0x020003CD RID: 973
[Serializable]
public class LocalWorkshopManifest
{
	// Token: 0x040014F1 RID: 5361
	public int formatVersion;

	// Token: 0x040014F2 RID: 5362
	public string contentType;

	// Token: 0x040014F3 RID: 5363
	public string title;

	// Token: 0x040014F4 RID: 5364
	public string description;

	// Token: 0x040014F5 RID: 5365
	public string author;

	// Token: 0x040014F6 RID: 5366
	public int heroId;

	// Token: 0x040014F7 RID: 5367
	public string heroName;

	// Token: 0x040014F8 RID: 5368
	public string heroIcon;

	// Token: 0x040014F9 RID: 5369
	public bool localOnly;

	// Token: 0x040014FA RID: 5370
	public string gameVersion;

	// Token: 0x040014FB RID: 5371
	public string unityVersion;

	// Token: 0x040014FC RID: 5372
	public string buildTarget;

	// Token: 0x040014FD RID: 5373
	public string bundleRelativePath;

	// Token: 0x040014FE RID: 5374
	public string bundleName;

	// Token: 0x040014FF RID: 5375
	public string bundleAssetName;

	// Token: 0x04001500 RID: 5376
	public bool preserveSourceMaterials;

	// Token: 0x04001501 RID: 5377
	public string previewImage;

	// Token: 0x04001502 RID: 5378
	public string mainTextureOverride;
}
