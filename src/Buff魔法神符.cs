using System;

// Token: 0x02000186 RID: 390
public class Buff魔法神符 : RoleBuff
{
	// Token: 0x0600076F RID: 1903 RVA: 0x0002C3F0 File Offset: 0x0002A5F0
	public override void OnInit()
	{
		this.info = ExcelManager.allExcelData["amulet"].DIC("1408").DIC("info");
		this.icon = "Amulet/" + ExcelManager.allExcelData["amulet"].DIC("1408").DIC("icon");
		PlayerBase playerBase = this.roleBase as PlayerBase;
		if (playerBase != null)
		{
			playerBase.skillExDamage += 1f;
		}
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0002C47C File Offset: 0x0002A67C
	public override void OnExit()
	{
		PlayerBase playerBase = this.roleBase as PlayerBase;
		if (playerBase != null)
		{
			playerBase.skillExDamage -= 1f;
		}
	}

	// Token: 0x04000B3B RID: 2875
	public int addAttackPower;
}
