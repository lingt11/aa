using System;

// Token: 0x0200017C RID: 380
public class Buff护盾神符 : RoleBuff
{
	// Token: 0x06000756 RID: 1878 RVA: 0x0002C01C File Offset: 0x0002A21C
	public override void OnInit()
	{
		this.info = ExcelManager.allExcelData["amulet"].DIC("1403").DIC("info");
		this.icon = "Amulet/" + ExcelManager.allExcelData["amulet"].DIC("1403").DIC("icon");
		this.shieldNum = ConstDefine.ClampBattleValue((double)this.roleBase.maxHp * 0.5);
		this.roleBase.AddShield(this.shieldNum);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0002C0B7 File Offset: 0x0002A2B7
	public override void OnExit()
	{
		this.roleBase.ClearShield(this.shieldNum);
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0002C0CA File Offset: 0x0002A2CA
	public override void Update()
	{
		base.Update();
		if (this.roleBase.Shield == 0L)
		{
			this.roleBase.roleBuffManager.RemoveBuff("Buff护盾神符");
		}
	}

	// Token: 0x04000B39 RID: 2873
	private long shieldNum;
}
