using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
[Serializable]
public class HeroGuidePosterEntry
{
	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06001291 RID: 4753 RVA: 0x0006E82D File Offset: 0x0006CA2D
	public bool HasData
	{
		get
		{
			return this.kind != HeroGuidePosterEntryKind.None && !string.IsNullOrEmpty(this.id);
		}
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x0006E848 File Offset: 0x0006CA48
	public void CopyFrom(HeroGuidePosterEntry other)
	{
		if (other == null)
		{
			this.Clear();
			return;
		}
		this.kind = other.kind;
		this.id = other.id;
		this.passiveSkill = other.passiveSkill;
		this.quality = other.quality;
		this.displayName = other.displayName;
		this.iconPath = other.iconPath;
		this.iconTint = other.iconTint;
	}

	// Token: 0x06001293 RID: 4755 RVA: 0x0006E8B4 File Offset: 0x0006CAB4
	public void Clear()
	{
		this.kind = HeroGuidePosterEntryKind.None;
		this.id = string.Empty;
		this.passiveSkill = false;
		this.quality = -1;
		this.displayName = string.Empty;
		this.iconPath = string.Empty;
		this.iconTint = Color.white;
	}

	// Token: 0x040010D5 RID: 4309
	public HeroGuidePosterEntryKind kind;

	// Token: 0x040010D6 RID: 4310
	public string id;

	// Token: 0x040010D7 RID: 4311
	public bool passiveSkill;

	// Token: 0x040010D8 RID: 4312
	public int quality = -1;

	// Token: 0x040010D9 RID: 4313
	public string displayName;

	// Token: 0x040010DA RID: 4314
	public string iconPath;

	// Token: 0x040010DB RID: 4315
	public Color iconTint = Color.white;
}
