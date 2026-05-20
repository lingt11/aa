using System;

// Token: 0x020000AE RID: 174
public class ReAttackBuff : RoleBuffBase
{
	// Token: 0x06000348 RID: 840 RVA: 0x00015A08 File Offset: 0x00013C08
	public override void InitBuff()
	{
		base.InitBuff();
		this.updatePower = ConstDefine.ClampIntValue((double)((float)this.roleBase.FinalAttackPower * this.buffValue));
		this.roleBase.AddAttackPower(-this.updatePower);
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00015A41 File Offset: 0x00013C41
	public override void ClearBuff()
	{
		this.roleBase.AddAttackPower(this.updatePower);
		base.ClearBuff();
	}

	// Token: 0x04000337 RID: 823
	public int updatePower;
}
