using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Token: 0x02000371 RID: 881
public class SkillUI
{
	// Token: 0x06001438 RID: 5176 RVA: 0x0007D86A File Offset: 0x0007BA6A
	public void ClearCD()
	{
		this.skillBase.updateCd = 0f;
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x0007D87C File Offset: 0x0007BA7C
	public void SetSkill(SkillBase skill, InputAction inputAction)
	{
		this.skillBase = skill;
		this.skillBase.skillUI = this;
		if (inputAction == null)
		{
			this.customInputUI.transform.parent.gameObject.SetActive(false);
		}
		else
		{
			this.customInputUI.transform.parent.gameObject.SetActive(true);
			this.customInputUI.SetInputActionReference(inputAction);
		}
		if (this.skillBase.cost == 0)
		{
			this.costMp.text = "";
		}
		else
		{
			this.costMp.text = this.skillBase.cost.ToString();
		}
		this.icon.gameObject.SetActive(true);
		this.icon.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Skill/" + skill.iconName);
		this.skillUITouch.skillBase = skill;
		this.switchGo.SetActive(skill.isSwitch || skill.isAuto);
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x0007D978 File Offset: 0x0007BB78
	public void HideSkill()
	{
		this.icon.gameObject.SetActive(false);
		this.customInputUI.transform.parent.gameObject.SetActive(false);
		this.costMp.text = "";
		this.textCDNum.text = "";
		this.imgCD.fillAmount = 0f;
		if (this.skillBase != null)
		{
			this.skillBase.isSwitch = false;
			this.skillBase.isAuto = false;
			this.skillBase = null;
		}
		this.switchGo.SetActive(false);
		this.skillUITouch.skillBase = null;
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x0007DA20 File Offset: 0x0007BC20
	public void Update()
	{
		if (this.skillBase == null)
		{
			return;
		}
		if (this.skillBase.updateCd > 0f)
		{
			this.textCDNum.text = this.skillBase.updateCd.ToString("f1");
			this.imgCD.fillAmount = this.skillBase.updateCd / this.skillBase.cdTime;
			if (!Mathf.Approximately(this.imgCD.color.a, 0.8f))
			{
				this.imgCD.color = new Color(0f, 0.08507979f, 0.3113208f, 0.8f);
				return;
			}
		}
		else
		{
			this.textCDNum.text = "";
			if (this.skillBase.chargingMax > 0 && this.skillBase.curCharging < this.skillBase.chargingMax)
			{
				this.imgCD.fillAmount = this.skillBase.curChargingTime / this.skillBase.chargingCd;
				if (!Mathf.Approximately(this.imgCD.color.a, 0.65f))
				{
					this.imgCD.color = new Color(0.4f, 0.4f, 0.4f, 0.65f);
				}
			}
			else
			{
				this.imgCD.fillAmount = 0f;
			}
			if (this.skillBase.isAuto && this.skillBase.activeSkillEnum != ActiveSkillEnum.None)
			{
				int realCost = Util.GetRealCost(GameHelperClient.localPlayer, Game.GameData.ActiveSkillDataDic[this.skillBase.activeSkillEnum].cost);
				if (GameHelperClient.localPlayer.mp >= realCost)
				{
					this.Use();
				}
			}
		}
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x0007DBD3 File Offset: 0x0007BDD3
	public void Use()
	{
		if (GameHelperClient.isGameOver)
		{
			return;
		}
		if (this.skillBase != null && this.skillBase.updateCd <= 0f)
		{
			this.skillBase.Use();
		}
	}

	// Token: 0x040012C1 RID: 4801
	public SkillUITouch skillUITouch;

	// Token: 0x040012C2 RID: 4802
	public TextMeshProUGUI textCDNum;

	// Token: 0x040012C3 RID: 4803
	public TextMeshProUGUI costMp;

	// Token: 0x040012C4 RID: 4804
	public CustomInputUI customInputUI;

	// Token: 0x040012C5 RID: 4805
	public Image imgCD;

	// Token: 0x040012C6 RID: 4806
	public Image icon;

	// Token: 0x040012C7 RID: 4807
	public SkillBase skillBase;

	// Token: 0x040012C8 RID: 4808
	public GameObject switchGo;

	// Token: 0x040012C9 RID: 4809
	public PlayerState_Skill playerStateSkill;
}
