using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200032C RID: 812
[CreateAssetMenu(menuName = "DungeonBrawl/Hero Guide Poster Preset", fileName = "HeroGuidePosterPreset")]
public class HeroGuidePosterPreset : ScriptableObject
{
	// Token: 0x06001296 RID: 4758 RVA: 0x0006E938 File Offset: 0x0006CB38
	public void Normalize()
	{
		HeroGuidePosterPreset.EnsureEntry(ref this.hero);
		HeroGuidePosterPreset.EnsureLayout(ref this.heroLayout);
		HeroGuidePosterPreset.EnsureLayout(ref this.recommendedRelicsLayout);
		HeroGuidePosterPreset.EnsureLayout(ref this.openingBuildLayout);
		HeroGuidePosterPreset.EnsureLayout(ref this.midBuildLayout);
		HeroGuidePosterPreset.EnsureLayout(ref this.lateBuildLayout);
		if (this.heroLayout.labelWidth <= 0f)
		{
			this.heroLayout.labelWidth = 360f;
		}
		if (this.recommendedRelics == null)
		{
			this.recommendedRelics = HeroGuidePosterPreset.CreateEntries(3);
		}
		if (this.openingBuild == null)
		{
			this.openingBuild = HeroGuidePosterPreset.CreateEntries(2);
		}
		if (this.midBuild == null)
		{
			this.midBuild = HeroGuidePosterPreset.CreateEntries(4);
		}
		if (this.lateBuild == null)
		{
			this.lateBuild = HeroGuidePosterPreset.CreateEntries(4);
		}
		HeroGuidePosterPreset.EnsureList(this.recommendedRelics);
		HeroGuidePosterPreset.EnsureList(this.openingBuild);
		HeroGuidePosterPreset.EnsureList(this.midBuild);
		HeroGuidePosterPreset.EnsureList(this.lateBuild);
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x0006EA25 File Offset: 0x0006CC25
	private static void EnsureEntry(ref HeroGuidePosterEntry entry)
	{
		if (entry == null)
		{
			entry = new HeroGuidePosterEntry();
		}
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x0006EA34 File Offset: 0x0006CC34
	private static void EnsureLayout(ref HeroGuidePosterRowLayout layout)
	{
		if (layout == null)
		{
			layout = new HeroGuidePosterRowLayout();
		}
		if (layout.slotSize <= 0f)
		{
			layout.slotSize = 100f;
		}
		if (layout.spacing <= 0f)
		{
			layout.spacing = layout.slotSize + 35f;
		}
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x0006EA88 File Offset: 0x0006CC88
	private static List<HeroGuidePosterEntry> CreateEntries(int count)
	{
		List<HeroGuidePosterEntry> list = new List<HeroGuidePosterEntry>(count);
		for (int i = 0; i < count; i++)
		{
			list.Add(new HeroGuidePosterEntry());
		}
		return list;
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x0006EAB4 File Offset: 0x0006CCB4
	private static void EnsureList(List<HeroGuidePosterEntry> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == null)
			{
				list[i] = new HeroGuidePosterEntry();
			}
		}
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x0006EAE7 File Offset: 0x0006CCE7
	private void OnValidate()
	{
		this.Normalize();
	}

	// Token: 0x040010E4 RID: 4324
	public string posterName = "英雄一图流攻略";

	// Token: 0x040010E5 RID: 4325
	public string heroNameSuffix = string.Empty;

	// Token: 0x040010E6 RID: 4326
	public Font font;

	// Token: 0x040010E7 RID: 4327
	public Color backgroundColor = Color.black;

	// Token: 0x040010E8 RID: 4328
	public Color textColor = Color.white;

	// Token: 0x040010E9 RID: 4329
	public Color borderColor = Color.white;

	// Token: 0x040010EA RID: 4330
	public Color emptyIconColor = Color.white;

	// Token: 0x040010EB RID: 4331
	public HeroGuidePosterEntry hero = new HeroGuidePosterEntry();

	// Token: 0x040010EC RID: 4332
	public HeroGuidePosterRowLayout heroLayout = new HeroGuidePosterRowLayout
	{
		slotX = 178f,
		slotY = 120f,
		slotSize = 176f,
		spacing = 176f,
		labelOffsetY = 0f,
		labelWidth = 360f,
		labelFontSize = 34
	};

	// Token: 0x040010ED RID: 4333
	public HeroGuidePosterRowLayout recommendedRelicsLayout = new HeroGuidePosterRowLayout
	{
		slotX = 675f,
		slotY = 172f,
		slotSize = 118f,
		spacing = 205f,
		labelOffsetY = 22f,
		labelFontSize = 28,
		useStarWhenEmpty = true
	};

	// Token: 0x040010EE RID: 4334
	public HeroGuidePosterRowLayout openingBuildLayout = new HeroGuidePosterRowLayout
	{
		slotX = 328f,
		slotY = 386f,
		slotSize = 150f,
		spacing = 210f,
		labelOffsetY = 22f,
		labelFontSize = 24
	};

	// Token: 0x040010EF RID: 4335
	public HeroGuidePosterRowLayout midBuildLayout = new HeroGuidePosterRowLayout
	{
		slotX = 346f,
		slotY = 644f,
		slotSize = 102f,
		spacing = 180f,
		labelOffsetY = 24f,
		labelFontSize = 23
	};

	// Token: 0x040010F0 RID: 4336
	public HeroGuidePosterRowLayout lateBuildLayout = new HeroGuidePosterRowLayout
	{
		slotX = 360f,
		slotY = 892f,
		slotSize = 102f,
		spacing = 185f,
		labelOffsetY = 24f,
		labelFontSize = 23
	};

	// Token: 0x040010F1 RID: 4337
	public List<HeroGuidePosterEntry> recommendedRelics = HeroGuidePosterPreset.CreateEntries(3);

	// Token: 0x040010F2 RID: 4338
	public List<HeroGuidePosterEntry> openingBuild = HeroGuidePosterPreset.CreateEntries(2);

	// Token: 0x040010F3 RID: 4339
	public List<HeroGuidePosterEntry> midBuild = HeroGuidePosterPreset.CreateEntries(4);

	// Token: 0x040010F4 RID: 4340
	public List<HeroGuidePosterEntry> lateBuild = HeroGuidePosterPreset.CreateEntries(4);
}
