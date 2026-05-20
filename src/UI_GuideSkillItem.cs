using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000335 RID: 821
public class UI_GuideSkillItem : MonoBehaviour
{
	// Token: 0x060012D0 RID: 4816 RVA: 0x000705EA File Offset: 0x0006E7EA
	private void Awake()
	{
		this.button.AddButtonEvent(new UnityAction(this.OnBtnClick));
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x00070604 File Offset: 0x0006E804
	public void SetSkill(Dictionary<string, object> data, string id, string skillName, string iconPath, bool isPassive)
	{
		this.skillIcon.sprite = Resources.Load<Sprite>(iconPath);
		this.skillIcon.material = null;
		this.skillNameText.text = skillName;
		this.skillData = data;
		this.skillId = id;
		this.isPassiveSkill = isPassive;
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x00070654 File Offset: 0x0006E854
	private void OnBtnClick()
	{
		EventSystem.current.SetSelectedGameObject(null);
		Game.UI.GetUI<UI_GuideSkill>().ShowSkillDec(this.skillNameText.text, SkillBase.GetSkillInfo(this.skillId, this.skillData, this.isPassiveSkill, true), this.skillIcon.sprite);
	}

	// Token: 0x04001119 RID: 4377
	public Button button;

	// Token: 0x0400111A RID: 4378
	public Image skillIcon;

	// Token: 0x0400111B RID: 4379
	public Text skillNameText;

	// Token: 0x0400111C RID: 4380
	private Dictionary<string, object> skillData;

	// Token: 0x0400111D RID: 4381
	private string skillId;

	// Token: 0x0400111E RID: 4382
	private bool isPassiveSkill;
}
