using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000363 RID: 867
public class BagItemDetail : MonoBehaviour
{
	// Token: 0x060013C8 RID: 5064 RVA: 0x0007A238 File Offset: 0x00078438
	public void ShowInfo(BagItemDetail.ShowDetailType showDetailType, string nameStr, string infoStr, string sellStr, string iconPath, bool isShop, Color iconColor, int exQua)
	{
		if (showDetailType == BagItemDetail.ShowDetailType.Shop || showDetailType == BagItemDetail.ShowDetailType.Equip)
		{
			this.rectTransform.anchoredPosition += new Vector2(this.rectTransform.sizeDelta.x + 120f, 0f);
			this.arrowRectTransform.anchoredPosition = new Vector2(-169.8399f, 108f);
			if (showDetailType == BagItemDetail.ShowDetailType.Equip)
			{
				this.arrowRectTransform.anchoredPosition -= new Vector2(0f, 50f);
			}
		}
		else
		{
			this.arrowRectTransform.anchoredPosition = new Vector2(169.8399f, 108f);
		}
		if (exQua == -1)
		{
			if (this.exText.gameObject.activeSelf)
			{
				this.exText.gameObject.SetActive(false);
			}
		}
		else
		{
			if (!this.exText.gameObject.activeSelf)
			{
				this.exText.gameObject.SetActive(true);
			}
			this.exText.text = string.Format(ColorDefine.QuaRelicText[exQua], Game.Language.Get("quality_" + exQua.ToString(), ""));
		}
		if (isShop)
		{
			this.tipText.text = Game.Language.Get("双击或右键直接购买", "");
		}
		else
		{
			this.tipText.text = Game.Language.Get("右键显示详细信息", "");
		}
		this.textName.text = nameStr;
		this.info.text = infoStr;
		this.icon.sprite = Resources.Load<Sprite>(iconPath);
		this.icon.color = iconColor;
		this.sellText.text = sellStr;
		this.needSetHeight = true;
	}

	// Token: 0x060013C9 RID: 5065 RVA: 0x0007A3F8 File Offset: 0x000785F8
	private void LateUpdate()
	{
		if (this.needSetHeight)
		{
			this.needSetHeight = false;
			float num = Mathf.Max(207f, this.info.preferredHeight + 115f);
			this.rectTransform.sizeDelta = new Vector2(this.rectTransform.sizeDelta.x, num);
			this.rectTransform.anchoredPosition += new Vector2(0f, (num - 207f) / 2f);
		}
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x0007A47E File Offset: 0x0007867E
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x0400125E RID: 4702
	public Image icon;

	// Token: 0x0400125F RID: 4703
	public Text textName;

	// Token: 0x04001260 RID: 4704
	public Text info;

	// Token: 0x04001261 RID: 4705
	public Text sellText;

	// Token: 0x04001262 RID: 4706
	public Text tipText;

	// Token: 0x04001263 RID: 4707
	public Text exText;

	// Token: 0x04001264 RID: 4708
	public RectTransform arrowRectTransform;

	// Token: 0x04001265 RID: 4709
	private RectTransform rectTransform;

	// Token: 0x04001266 RID: 4710
	private bool needSetHeight;

	// Token: 0x02000364 RID: 868
	public enum ShowDetailType
	{
		// Token: 0x04001268 RID: 4712
		Normal,
		// Token: 0x04001269 RID: 4713
		Shop,
		// Token: 0x0400126A RID: 4714
		Equip
	}
}
