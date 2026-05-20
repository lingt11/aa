using System;

// Token: 0x02000212 RID: 530
public class RelicEquipMaster : RelicBase
{
	// Token: 0x060009A3 RID: 2467 RVA: 0x00033E1D File Offset: 0x0003201D
	public override void Enter()
	{
		this.playerBase.equipAddValue += base.GetValue(0, 0.5f);
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshPlayerEquip();
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00033E51 File Offset: 0x00032051
	public override void Exit()
	{
		this.playerBase.equipAddValue -= base.GetValue(0, 0.5f);
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshPlayerEquip();
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00033E85 File Offset: 0x00032085
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.equipAddValue += base.GetLevelValueDelta(0, 0.5f, oldLevel, newLevel);
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshPlayerEquip();
	}
}
