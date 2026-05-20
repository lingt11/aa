using System;
using UnityEngine;

// Token: 0x020002CE RID: 718
public class BuffActiveSkill : ActiveSkillBase
{
	// Token: 0x060010CC RID: 4300 RVA: 0x0005E90A File Offset: 0x0005CB0A
	public virtual void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, string effectName, ActiveSkillData activeSkillData)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = activeSkillData;
		this.attackRoleBase = attackRole;
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0005E924 File Offset: 0x0005CB24
	protected void InitEffect(string effectName, float scale, float localY)
	{
		this.buffEffect = AssetManager.LoadPrefab(effectName, null, true);
		scale += this.attackRoleBase.RoleModeBase.addRange * 2f;
		Transform transform = this.buffEffect.transform;
		transform.SetParent(this.attackRoleBase.MyTransform);
		transform.localPosition = new Vector3(0f, localY, 0f);
		transform.localScale = new Vector3(scale, scale, scale);
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0005E998 File Offset: 0x0005CB98
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.buffEffect != null)
		{
			AssetManager.UnLoadPrefab(this.buffEffect, false);
			this.buffEffect = null;
		}
	}

	// Token: 0x04000EC4 RID: 3780
	protected GameObject buffEffect;
}
