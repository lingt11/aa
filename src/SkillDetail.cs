using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200036C RID: 876
public class SkillDetail : MonoBehaviour
{
	// Token: 0x060013E5 RID: 5093 RVA: 0x0007A90E File Offset: 0x00078B0E
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x0007A91C File Offset: 0x00078B1C
	public void ShowInfo(UI_DecTip.TipInfo tipInfo)
	{
		if (tipInfo.isRelic)
		{
			this.icon.color = ColorDefine.QuaUIColor[tipInfo.quality];
		}
		else
		{
			this.icon.color = Color.white;
		}
		this.textName.text = tipInfo.nameStr;
		this.needSetHeight = true;
		this.info.text = tipInfo.info;
		this.icon.sprite = Resources.Load<Sprite>(tipInfo.iconPath);
		if (string.IsNullOrEmpty(tipInfo.exInfo))
		{
			if (this.exInfo.gameObject.activeSelf)
			{
				this.exInfo.gameObject.SetActive(false);
			}
		}
		else
		{
			if (!this.exInfo.gameObject.activeSelf)
			{
				this.exInfo.gameObject.SetActive(true);
			}
			this.exInfo.text = tipInfo.exInfo;
		}
		if (tipInfo.cd > 0f)
		{
			if (!this.cdText.gameObject.activeSelf)
			{
				this.cdText.gameObject.SetActive(true);
			}
			this.cdText.text = PathDefine.Concat(Util.FormatFloat(tipInfo.cd), Game.Language.Get("秒", ""));
			return;
		}
		if (this.cdText.gameObject.activeSelf)
		{
			this.cdText.gameObject.SetActive(false);
		}
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x0007AA88 File Offset: 0x00078C88
	private void LateUpdate()
	{
		if (this.needSetHeight)
		{
			this.needSetHeight = false;
			float y = Mathf.Max(197f, this.info.preferredHeight + 115f);
			this.rectTransform.sizeDelta = new Vector2(this.rectTransform.sizeDelta.x, y);
		}
	}

	// Token: 0x0400128D RID: 4749
	public Image icon;

	// Token: 0x0400128E RID: 4750
	public Text textName;

	// Token: 0x0400128F RID: 4751
	public Text info;

	// Token: 0x04001290 RID: 4752
	public Text exInfo;

	// Token: 0x04001291 RID: 4753
	public Text cdText;

	// Token: 0x04001292 RID: 4754
	private bool needSetHeight;

	// Token: 0x04001293 RID: 4755
	private RectTransform rectTransform;
}
