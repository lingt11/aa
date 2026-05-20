using System;

// Token: 0x0200017D RID: 381
public class Buff攻速加成 : RoleBuff
{
	// Token: 0x0600075A RID: 1882 RVA: 0x0002C0F4 File Offset: 0x0002A2F4
	public override void OnInit()
	{
		this.icon = "Amulet/咆哮神符";
		this.roleBase.AddAttackSpeed(this.addValue);
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0002C112 File Offset: 0x0002A312
	public override void OnExit()
	{
		this.roleBase.AddAttackSpeed(-this.addValue);
	}

	// Token: 0x04000B3A RID: 2874
	public float addValue;
}
