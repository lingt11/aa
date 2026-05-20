using System;

// Token: 0x0200015E RID: 350
public class MedicineBase
{
	// Token: 0x060006E1 RID: 1761 RVA: 0x0002A4E8 File Offset: 0x000286E8
	public virtual void Init(ShopItem shopItemValue, PlayerBase playerBaseValue)
	{
		this.shopItem = shopItemValue;
		this.waveCount = this.shopItem.times;
		this.playerBase = playerBaseValue;
		if (this.playerBase.isLocalPlayer)
		{
			string text = Game.Language.Get(PathDefine.Concat(this.shopItem.id, "_m"), "");
			if (this.shopItem.strValues != null)
			{
				string format = text;
				object[] strValues = this.shopItem.strValues;
				text = string.Format(format, strValues);
			}
			string str = string.Format(Game.Language.Get("剩余回合", ""), string.Format(ColorDefine.NormalColor, this.waveCount));
			text = text + "\n" + str;
			this.roleBuff = GameHelperClient.AddShowBuff(Game.Language.Get(this.shopItem.id, ""), text, PathDefine.Concat("Shop/", this.shopItem.iconPath), -1f);
		}
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0002A5E6 File Offset: 0x000287E6
	public virtual void Clear()
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
		this.playerBase = null;
		this.shopItem = null;
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0002A61C File Offset: 0x0002881C
	public virtual void OnWaveAdd()
	{
		string text = Game.Language.Get(PathDefine.Concat(this.shopItem.id, "_m"), "");
		if (this.shopItem.strValues != null)
		{
			string format = text;
			object[] strValues = this.shopItem.strValues;
			text = string.Format(format, strValues);
		}
		string str = string.Format(Game.Language.Get("剩余回合", ""), string.Format(ColorDefine.NormalColor, this.waveCount));
		text = text + "\n" + str;
		this.roleBuff.info = text;
	}

	// Token: 0x04000AF9 RID: 2809
	public int waveCount;

	// Token: 0x04000AFA RID: 2810
	protected RoleBuff roleBuff;

	// Token: 0x04000AFB RID: 2811
	protected PlayerBase playerBase;

	// Token: 0x04000AFC RID: 2812
	protected ShopItem shopItem;
}
