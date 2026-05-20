using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200036B RID: 875
public class PlayerState_Skill : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerUpHandler
{
	// Token: 0x060013DD RID: 5085 RVA: 0x0007A77F File Offset: 0x0007897F
	public void SetSkillIndex(int value, SkillUI skillUIValue)
	{
		this.skillIndex = value;
		this.skillUI = skillUIValue;
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x0007A790 File Offset: 0x00078990
	public void CanSwitch(bool value, float defaultSkillItemScale)
	{
		if (value)
		{
			base.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		}
		else
		{
			base.transform.localScale = new Vector3(defaultSkillItemScale, defaultSkillItemScale, defaultSkillItemScale);
		}
		if (value)
		{
			this.canSwitch = true;
			return;
		}
		this.lateSet = true;
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x0007A7E6 File Offset: 0x000789E6
	private void LateUpdate()
	{
		if (this.lateSet)
		{
			this.lateSet = false;
			this.canSwitch = false;
		}
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x0007A7FE File Offset: 0x000789FE
	private void Awake()
	{
		this.switchBtn.AddButtonEvent(new UnityAction(this.OnBtnClick));
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x0007A817 File Offset: 0x00078A17
	private void OnBtnClick()
	{
		if (this.canSwitch)
		{
			Game.UI.GetUI<UI_PlayerState>().OnSwitchSkillCallBack(this.skillIndex);
		}
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x0007A838 File Offset: 0x00078A38
	public void OnPointerUp(PointerEventData eventData)
	{
		if (this.canSwitch)
		{
			return;
		}
		if (eventData.button == PointerEventData.InputButton.Left && this.skillUI != null && !(this.skillUI.skillBase is PasssiveSkill))
		{
			Game.UI.GetUI<UI_PlayerState>().OnSkillBtnUp(this.skillIndex);
		}
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x0007A888 File Offset: 0x00078A88
	public void OnPointerClick(PointerEventData eventData)
	{
		if (this.canSwitch)
		{
			return;
		}
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			if (this.skillUI != null && !(this.skillUI.skillBase is PasssiveSkill))
			{
				Game.UI.GetUI<UI_PlayerState>().BtnRightSkill(this.skillUI);
				return;
			}
		}
		else if (eventData.button == PointerEventData.InputButton.Left && this.skillUI != null && !(this.skillUI.skillBase is PasssiveSkill))
		{
			Game.UI.GetUI<UI_PlayerState>().OnSkillBtnClick(this.skillIndex);
		}
	}

	// Token: 0x04001281 RID: 4737
	public Image icon;

	// Token: 0x04001282 RID: 4738
	public Image cdImg;

	// Token: 0x04001283 RID: 4739
	public TextMeshProUGUI cdText;

	// Token: 0x04001284 RID: 4740
	public SkillUITouch skillUITouch;

	// Token: 0x04001285 RID: 4741
	public TextMeshProUGUI textCostMp;

	// Token: 0x04001286 RID: 4742
	public GameObject switchGo;

	// Token: 0x04001287 RID: 4743
	public Button switchBtn;

	// Token: 0x04001288 RID: 4744
	public CustomInputUI customInputUI;

	// Token: 0x04001289 RID: 4745
	private SkillUI skillUI;

	// Token: 0x0400128A RID: 4746
	private int skillIndex;

	// Token: 0x0400128B RID: 4747
	private bool canSwitch;

	// Token: 0x0400128C RID: 4748
	private bool lateSet;
}
