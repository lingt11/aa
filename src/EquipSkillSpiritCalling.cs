using System;

// Token: 0x020000F9 RID: 249
public class EquipSkillSpiritCalling : EquipSkillBase
{
	// Token: 0x0600051E RID: 1310 RVA: 0x0001E440 File Offset: 0x0001C640
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.addCallMonsterAttack += this.equipValue;
		this.playerBase.addCallMonsterHp += this.equipValue;
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0001E4AB File Offset: 0x0001C6AB
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.addCallMonsterAttack += this.equipValue;
		this.playerBase.addCallMonsterHp += this.equipValue;
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0001E4E3 File Offset: 0x0001C6E3
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.addCallMonsterAttack -= this.equipValue;
		this.playerBase.addCallMonsterHp -= this.equipValue;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0001E51C File Offset: 0x0001C71C
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.addCallMonsterAttack -= this.equipValue;
			this.playerBase.addCallMonsterHp -= this.equipValue;
		}
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0001E568 File Offset: 0x0001C768
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.addCallMonsterAttack += this.equipUpValue * (float)updateLevel;
		this.playerBase.addCallMonsterHp += this.equipUpValue * (float)updateLevel;
	}

	// Token: 0x04000482 RID: 1154
	private float equipValue;

	// Token: 0x04000483 RID: 1155
	private float equipUpValue;
}
