using System;

// Token: 0x020000DD RID: 221
public class EquipSkillArmedCore : EquipSkillBase
{
	// Token: 0x06000499 RID: 1177 RVA: 0x0001C2EC File Offset: 0x0001A4EC
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.armedAdd += this.equipValue;
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0001C33F File Offset: 0x0001A53F
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.armedAdd += this.equipValue;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0001C35F File Offset: 0x0001A55F
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.armedAdd -= this.equipValue;
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0001C37F File Offset: 0x0001A57F
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.armedAdd -= this.equipValue;
		}
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.armedAdd += (float)updateLevel * this.equipUpValue;
	}

	// Token: 0x0400041A RID: 1050
	private float equipValue;

	// Token: 0x0400041B RID: 1051
	private float equipUpValue;
}
