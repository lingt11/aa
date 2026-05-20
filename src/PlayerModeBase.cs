using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class PlayerModeBase : RoleModeBase
{
	// Token: 0x06000EBD RID: 3773 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnSkillKeyUp(int index)
	{
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x000545B0 File Offset: 0x000527B0
	public override void OnInitMode()
	{
		base.OnInitMode();
		this.playerBase = (this.roleBase as PlayerBase);
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x000545CC File Offset: 0x000527CC
	public override void OnStartSkill()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill, 1.5f * (1f + this.playerBase.castSpeed), 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
	}

	// Token: 0x04000DB8 RID: 3512
	[Header("玩家攻击声音")]
	public AttackHitSound attackHitSound;

	// Token: 0x04000DB9 RID: 3513
	[Header("模型高度 Y 轴偏移")]
	public float modelOffsetY;

	// Token: 0x04000DBA RID: 3514
	[Header("Workshop Humanoid Attachments")]
	public List<PlayerHumanoidAttachmentBinding> humanoidAttachmentBindings = new List<PlayerHumanoidAttachmentBinding>();

	// Token: 0x04000DBB RID: 3515
	[HideInInspector]
	public PlayerBase playerBase;
}
