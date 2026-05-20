using System;

// Token: 0x02000180 RID: 384
public class Buff痊愈神符 : RoleBuff
{
	// Token: 0x06000763 RID: 1891 RVA: 0x0002C1FF File Offset: 0x0002A3FF
	public override void OnInit()
	{
		((PlayerBase)this.roleBase).AddPlayerHp(this.roleBase.maxHp);
	}
}
