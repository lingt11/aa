using System;

// Token: 0x020000AC RID: 172
public class FrostRoleBuffBase : RoleBuffBase
{
	// Token: 0x06000342 RID: 834 RVA: 0x00015838 File Offset: 0x00013A38
	public override void InitBuff()
	{
		base.InitBuff();
		this.roleBase.moveSpeedPercent -= this.buffValue;
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00015858 File Offset: 0x00013A58
	public override void ClearBuff()
	{
		this.roleBase.moveSpeedPercent += this.buffValue;
		base.ClearBuff();
	}
}
