using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000304 RID: 772
public class MyHpBar : MonoBehaviour
{
	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0006885E File Offset: 0x00066A5E
	public RectTransform MyRectTransform
	{
		get
		{
			return this.myRectTransform;
		}
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x00068868 File Offset: 0x00066A68
	public void UpdateValue(float fillAmount, float shieldFill)
	{
		if (!Mathf.Approximately(this.healthBarImage.fillAmount, fillAmount))
		{
			this.healthBarImage.fillAmount = fillAmount;
		}
		if (shieldFill > 0f)
		{
			if (!this.shieldGo.activeSelf)
			{
				this.shieldGo.SetActive(true);
			}
			if (shieldFill > 1f)
			{
				if (!this.shieldNum.gameObject.activeSelf)
				{
					this.shieldNum.gameObject.SetActive(true);
				}
				this.shieldNum.text = PathDefine.Concat("X", Mathf.CeilToInt(shieldFill / 1f));
				shieldFill %= 1f;
				if (Mathf.Approximately(shieldFill, 0f))
				{
					shieldFill = 1f;
				}
			}
			else if (this.shieldNum.gameObject.activeSelf)
			{
				this.shieldNum.gameObject.SetActive(false);
			}
			if (!Mathf.Approximately(this.shieldImage.fillAmount, shieldFill))
			{
				this.shieldImage.fillAmount = shieldFill;
			}
		}
		else if (this.shieldGo.activeSelf)
		{
			this.shieldGo.SetActive(false);
		}
		this.UpdateEvent();
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x0006898B File Offset: 0x00066B8B
	public void Hide()
	{
		if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x000689A6 File Offset: 0x00066BA6
	public void Show()
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x000689C4 File Offset: 0x00066BC4
	public void ShowName(string roleName, Color color, bool showName = true)
	{
		if (this.nameGo != null && this.nameGo.activeSelf != showName)
		{
			this.nameGo.SetActive(showName);
		}
		if (this.nameText != null && this.nameText.gameObject.activeSelf != showName)
		{
			this.nameText.gameObject.SetActive(showName);
		}
		if (!showName)
		{
			return;
		}
		this.nameText.text = roleName;
		this.nameText.color = color;
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x00068A48 File Offset: 0x00066C48
	private void UpdateEvent()
	{
		if (this.whiteBar == null)
		{
			return;
		}
		float fillAmount = this.whiteBar.fillAmount;
		float fillAmount2 = this.healthBarImage.fillAmount;
		if (fillAmount > fillAmount2 && Mathf.Approximately(this.showDelay, 0f))
		{
			this.showDelay = 0.5f;
		}
		if (this.showDelay > 0f)
		{
			this.showDelay -= Time.deltaTime;
			if (this.showDelay <= 0f)
			{
				this.showDelay = -1f;
			}
			return;
		}
		float deltaTime = Time.deltaTime;
		if (Mathf.Approximately(fillAmount, fillAmount2))
		{
			this.showDelay = 0f;
			return;
		}
		if (fillAmount > fillAmount2)
		{
			this.whiteBar.fillAmount = fillAmount - deltaTime * 0.5f;
			return;
		}
		this.whiteBar.fillAmount = fillAmount2;
	}

	// Token: 0x04000FDF RID: 4063
	[SerializeField]
	private Image healthBarImage;

	// Token: 0x04000FE0 RID: 4064
	[SerializeField]
	private RectTransform myRectTransform;

	// Token: 0x04000FE1 RID: 4065
	[SerializeField]
	private Text nameText;

	// Token: 0x04000FE2 RID: 4066
	[SerializeField]
	private GameObject nameGo;

	// Token: 0x04000FE3 RID: 4067
	[SerializeField]
	private Image whiteBar;

	// Token: 0x04000FE4 RID: 4068
	[SerializeField]
	private Image shieldImage;

	// Token: 0x04000FE5 RID: 4069
	[SerializeField]
	private GameObject shieldGo;

	// Token: 0x04000FE6 RID: 4070
	[SerializeField]
	private TextMeshProUGUI shieldNum;

	// Token: 0x04000FE7 RID: 4071
	private float showDelay;
}
