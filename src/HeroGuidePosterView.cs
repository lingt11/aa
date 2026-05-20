using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200032D RID: 813
[ExecuteAlways]
[DisallowMultipleComponent]
public class HeroGuidePosterView : MonoBehaviour
{
	// Token: 0x0600129D RID: 4765 RVA: 0x0006ED04 File Offset: 0x0006CF04
	public void Rebuild()
	{
		this.EnsureCanvas();
		this.ClearSpawnedObjects();
		if (this.preset != null)
		{
			this.preset.Normalize();
		}
		Font font = this.ResolveFont();
		Color color = (this.preset != null) ? this.preset.textColor : Color.white;
		Color borderColor = (this.preset != null) ? this.preset.borderColor : Color.white;
		Color color2 = (this.preset != null) ? this.preset.backgroundColor : Color.black;
		RectTransform rectTransform = this.CreateRect("PosterRoot", base.transform, 0f, 0f, 1920f, 1080f);
		rectTransform.gameObject.AddComponent<Image>().color = color2;
		HeroGuidePosterRowLayout layout = (this.preset != null) ? this.preset.heroLayout : null;
		this.AddEntryBox(rectTransform, "Hero", this.GetHeroEntry(), layout, font, color, borderColor, 0);
		this.AddText(rectTransform, "推荐遗物", 508f, 198f, 150f, 48f, 30, TextAnchor.MiddleLeft, font, color);
		this.AddEntryRow(rectTransform, (this.preset != null) ? this.preset.recommendedRelics : null, (this.preset != null) ? this.preset.recommendedRelicsLayout : null, font, color, borderColor);
		this.AddText(rectTransform, "出门装", 160f, 432f, 130f, 48f, 30, TextAnchor.MiddleLeft, font, color);
		this.AddEntryRow(rectTransform, (this.preset != null) ? this.preset.openingBuild : null, (this.preset != null) ? this.preset.openingBuildLayout : null, font, color, borderColor);
		this.AddText(rectTransform, "中期出装", 96f, 686f, 170f, 48f, 30, TextAnchor.MiddleLeft, font, color);
		this.AddEntryRow(rectTransform, (this.preset != null) ? this.preset.midBuild : null, (this.preset != null) ? this.preset.midBuildLayout : null, font, color, borderColor);
		this.AddText(rectTransform, "后期出装", 102f, 940f, 170f, 48f, 30, TextAnchor.MiddleLeft, font, color);
		this.AddEntryRow(rectTransform, (this.preset != null) ? this.preset.lateBuild : null, (this.preset != null) ? this.preset.lateBuildLayout : null, font, color, borderColor);
		Canvas.ForceUpdateCanvases();
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x0006EFBD File Offset: 0x0006D1BD
	private HeroGuidePosterEntry GetHeroEntry()
	{
		if (!(this.preset != null))
		{
			return null;
		}
		return this.preset.hero;
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x0006EFDC File Offset: 0x0006D1DC
	private void AddEntryRow(RectTransform root, List<HeroGuidePosterEntry> entries, HeroGuidePosterRowLayout layout, Font font, Color textColor, Color borderColor)
	{
		if (layout == null)
		{
			layout = new HeroGuidePosterRowLayout();
		}
		int num = (entries != null) ? entries.Count : 0;
		for (int i = 0; i < num; i++)
		{
			this.AddEntryBox(root, "Slot_" + i.ToString(), entries[i], layout, font, textColor, borderColor, i);
		}
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x0006F034 File Offset: 0x0006D234
	private void AddEntryBox(RectTransform root, string name, HeroGuidePosterEntry entry, HeroGuidePosterRowLayout layout, Font font, Color textColor, Color borderColor, int index = 0)
	{
		if (layout == null)
		{
			layout = new HeroGuidePosterRowLayout();
		}
		float num = layout.slotX + layout.spacing * (float)index;
		float slotY = layout.slotY;
		float num2 = Mathf.Max(8f, layout.slotSize);
		Color frameColor = HeroGuidePosterView.GetFrameColor(entry, borderColor);
		RectTransform parent = this.CreateRect(name, root, num, slotY, num2, num2);
		this.AddBorder(parent, frameColor, 4f);
		if (entry != null && entry.HasData)
		{
			Sprite sprite = HeroGuidePosterView.LoadSprite(entry.iconPath);
			if (sprite != null)
			{
				Image image = this.CreateRect("Icon", parent, 8f, 8f, num2 - 16f, num2 - 16f).gameObject.AddComponent<Image>();
				image.sprite = sprite;
				image.color = HeroGuidePosterView.GetEntryIconColor(entry);
				image.preserveAspect = true;
			}
			else
			{
				this.AddText(parent, HeroGuidePosterView.ShortKindName(entry.kind), 0f, 0f, num2, num2, 25, TextAnchor.MiddleCenter, font, textColor);
			}
		}
		else if (layout.useStarWhenEmpty)
		{
			this.AddText(parent, "☆", 0f, 0f, num2, num2, 74, TextAnchor.MiddleCenter, font, (this.preset != null) ? this.preset.emptyIconColor : Color.white);
		}
		string entryLabel = this.GetEntryLabel(entry);
		if (!string.IsNullOrEmpty(entryLabel))
		{
			float num3 = (layout.labelWidth > 0f) ? layout.labelWidth : (num2 + 36f);
			float x = num + (num2 - num3) * 0.5f;
			this.AddText(root, entryLabel, x, slotY + num2 + layout.labelOffsetY, num3, 44f, layout.labelFontSize, TextAnchor.UpperCenter, font, textColor);
		}
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x0006F1F0 File Offset: 0x0006D3F0
	private string GetEntryLabel(HeroGuidePosterEntry entry)
	{
		if (entry == null || string.IsNullOrEmpty(entry.displayName))
		{
			return string.Empty;
		}
		if (entry.kind == HeroGuidePosterEntryKind.Hero && this.preset != null && !string.IsNullOrEmpty(this.preset.heroNameSuffix))
		{
			return entry.displayName + this.preset.heroNameSuffix;
		}
		return entry.displayName;
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x0006F259 File Offset: 0x0006D459
	private static Color GetFrameColor(HeroGuidePosterEntry entry, Color borderColor)
	{
		if (entry != null && entry.kind == HeroGuidePosterEntryKind.Relic)
		{
			return HeroGuidePosterView.GetEntryIconColor(entry);
		}
		return borderColor;
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x0006F270 File Offset: 0x0006D470
	private static Color GetEntryIconColor(HeroGuidePosterEntry entry)
	{
		if (entry != null && entry.kind == HeroGuidePosterEntryKind.Relic)
		{
			int relicQuality = HeroGuidePosterView.GetRelicQuality(entry);
			if (relicQuality >= 0 && relicQuality < ColorDefine.QuaUIColor.Length)
			{
				return ColorDefine.QuaUIColor[relicQuality];
			}
		}
		if (entry == null)
		{
			return Color.white;
		}
		return entry.iconTint;
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x0006F2BC File Offset: 0x0006D4BC
	private static int GetRelicQuality(HeroGuidePosterEntry entry)
	{
		if (entry == null)
		{
			return -1;
		}
		object obj;
		if (!string.IsNullOrEmpty(entry.id) && ExcelManager.allExcelData != null && ExcelManager.allExcelData.TryGetValue("remains", out obj))
		{
			Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
			object obj2;
			if (dictionary != null && dictionary.TryGetValue(entry.id, out obj2))
			{
				Dictionary<string, object> dictionary2 = obj2 as Dictionary<string, object>;
				object obj3;
				if (dictionary2 != null && dictionary2.TryGetValue("grade", out obj3) && obj3 != null)
				{
					string text = obj3.ToString();
					int result;
					if (int.TryParse(text, out result))
					{
						return result;
					}
					int num = DropDefine.QualityAry.IndexOf(text);
					if (num >= 0)
					{
						return num;
					}
				}
			}
		}
		return entry.quality;
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x0006F35F File Offset: 0x0006D55F
	private static string ShortKindName(HeroGuidePosterEntryKind kind)
	{
		switch (kind)
		{
		case HeroGuidePosterEntryKind.Hero:
			return "英雄";
		case HeroGuidePosterEntryKind.Relic:
			return "遗物";
		case HeroGuidePosterEntryKind.Skill:
			return "技能";
		case HeroGuidePosterEntryKind.Equip:
			return "装备";
		default:
			return string.Empty;
		}
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x0006F398 File Offset: 0x0006D598
	private void AddBorder(RectTransform parent, Color color, float thickness)
	{
		Vector2 sizeDelta = parent.sizeDelta;
		this.AddSolid(parent, "BorderTop", 0f, 0f, sizeDelta.x, thickness, color);
		this.AddSolid(parent, "BorderBottom", 0f, sizeDelta.y - thickness, sizeDelta.x, thickness, color);
		this.AddSolid(parent, "BorderLeft", 0f, 0f, thickness, sizeDelta.y, color);
		this.AddSolid(parent, "BorderRight", sizeDelta.x - thickness, 0f, thickness, sizeDelta.y, color);
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x0006F42E File Offset: 0x0006D62E
	private RectTransform AddSolid(Transform parent, string name, float x, float y, float width, float height, Color color)
	{
		RectTransform rectTransform = this.CreateRect(name, parent, x, y, width, height);
		rectTransform.gameObject.AddComponent<Image>().color = color;
		return rectTransform;
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x0006F454 File Offset: 0x0006D654
	private RectTransform AddText(Transform parent, string text, float x, float y, float width, float height, int size, TextAnchor alignment, Font font, Color color)
	{
		RectTransform rectTransform = this.CreateRect("Text", parent, x, y, width, height);
		Text text2 = rectTransform.gameObject.AddComponent<Text>();
		text2.font = font;
		text2.fontSize = size;
		text2.alignment = alignment;
		text2.color = color;
		text2.text = text;
		text2.horizontalOverflow = HorizontalWrapMode.Wrap;
		text2.verticalOverflow = VerticalWrapMode.Truncate;
		text2.raycastTarget = false;
		return rectTransform;
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x0006F4BC File Offset: 0x0006D6BC
	private RectTransform CreateRect(string name, Transform parent, float x, float y, float width, float height)
	{
		GameObject gameObject = new GameObject(name, new Type[]
		{
			typeof(RectTransform)
		});
		gameObject.layer = base.gameObject.layer;
		gameObject.transform.SetParent(parent, false);
		this.spawnedObjects.Add(gameObject);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		RectTransform rectTransform = parent as RectTransform;
		Vector2 vector = (rectTransform != null) ? rectTransform.sizeDelta : new Vector2(1920f, 1080f);
		component.anchorMin = HeroGuidePosterView.CenterAnchor;
		component.anchorMax = HeroGuidePosterView.CenterAnchor;
		component.pivot = HeroGuidePosterView.CenterAnchor;
		component.sizeDelta = new Vector2(width, height);
		component.anchoredPosition = new Vector2(x - vector.x * 0.5f + width * 0.5f, vector.y * 0.5f - y - height * 0.5f);
		return component;
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x0006F5A8 File Offset: 0x0006D7A8
	private void EnsureCanvas()
	{
		Canvas canvas = base.GetComponent<Canvas>();
		if (canvas == null)
		{
			canvas = base.gameObject.AddComponent<Canvas>();
		}
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		canvas.pixelPerfect = true;
		CanvasScaler canvasScaler = base.GetComponent<CanvasScaler>();
		if (canvasScaler == null)
		{
			canvasScaler = base.gameObject.AddComponent<CanvasScaler>();
		}
		canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		canvasScaler.referenceResolution = new Vector2(1920f, 1080f);
		canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
		canvasScaler.matchWidthOrHeight = 0.5f;
		if (base.GetComponent<GraphicRaycaster>() == null)
		{
			base.gameObject.AddComponent<GraphicRaycaster>();
		}
		RectTransform rectTransform = base.transform as RectTransform;
		if (rectTransform != null)
		{
			rectTransform.anchorMin = HeroGuidePosterView.CenterAnchor;
			rectTransform.anchorMax = HeroGuidePosterView.CenterAnchor;
			rectTransform.pivot = HeroGuidePosterView.CenterAnchor;
			rectTransform.sizeDelta = new Vector2(1920f, 1080f);
			rectTransform.anchoredPosition = Vector2.zero;
			rectTransform.localScale = Vector3.one;
		}
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x0006F6A4 File Offset: 0x0006D8A4
	private void ClearSpawnedObjects()
	{
		for (int i = base.transform.childCount - 1; i >= 0; i--)
		{
			Transform child = base.transform.GetChild(i);
			if (Application.isPlaying)
			{
				Object.Destroy(child.gameObject);
			}
			else
			{
				Object.DestroyImmediate(child.gameObject);
			}
		}
		this.spawnedObjects.Clear();
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x0006F700 File Offset: 0x0006D900
	private Font ResolveFont()
	{
		if (this.preset != null && this.preset.font != null)
		{
			return this.preset.font;
		}
		return Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x0006F739 File Offset: 0x0006D939
	private static Sprite LoadSprite(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return null;
		}
		return Resources.Load<Sprite>(path);
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void OnValidate()
	{
	}

	// Token: 0x040010F5 RID: 4341
	public const int PosterWidth = 1920;

	// Token: 0x040010F6 RID: 4342
	public const int PosterHeight = 1080;

	// Token: 0x040010F7 RID: 4343
	public HeroGuidePosterPreset preset;

	// Token: 0x040010F8 RID: 4344
	public bool rebuildOnValidate = true;

	// Token: 0x040010F9 RID: 4345
	private static readonly Vector2 CenterAnchor = new Vector2(0.5f, 0.5f);

	// Token: 0x040010FA RID: 4346
	private readonly List<GameObject> spawnedObjects = new List<GameObject>();
}
