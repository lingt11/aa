using System;

// Token: 0x020000EA RID: 234
public class EquipSkillHenshinBelt : EquipSkillBase
{
	// Token: 0x060004D2 RID: 1234 RVA: 0x0001D0FC File Offset: 0x0001B2FC
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.addHenshin += this.equipValue;
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0001D14F File Offset: 0x0001B34F
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.addHenshin += this.equipValue;
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0001D16F File Offset: 0x0001B36F
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.addHenshin -= this.equipValue;
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0001D18F File Offset: 0x0001B38F
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.addHenshin -= this.equipValue;
		}
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0001D1B8 File Offset: 0x0001B3B8
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.addHenshin += (float)updateLevel * this.equipUpValue;
	}

	// Token: 0x04000463 RID: 1123
	private float equipValue;

	// Token: 0x04000464 RID: 1124
	private float equipUpValue;
}
