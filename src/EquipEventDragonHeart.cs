using System;

// Token: 0x020000D9 RID: 217
public class EquipEventDragonHeart : EquipEventBase
{
	// Token: 0x06000472 RID: 1138 RVA: 0x0001B5B4 File Offset: 0x000197B4
	public override void Init(EquipBase equipBaseValue)
	{
		base.Init(equipBaseValue);
		MySystemEvent.Instance.RegisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0001B5D5 File Offset: 0x000197D5
	private void OnWaveLevelUp(Body body)
	{
		if (this.equipBase.level < this.equipBase.maxLevel)
		{
			this.equipBase.OnLevelUpSuccess(true, 1);
		}
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0001B5FC File Offset: 0x000197FC
	public override void Clear()
	{
		base.Clear();
		MySystemEvent.Instance.UnregisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}
}
