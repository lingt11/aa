using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class CardSkillTrollHeart : CardSkillBase
{
	// Token: 0x06000390 RID: 912 RVA: 0x00002D1D File Offset: 0x00000F1D
	public override void Enter()
	{
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0001725C File Offset: 0x0001545C
	public override void Update()
	{
		if (this.addTime > 0f)
		{
			this.addTime -= Time.deltaTime;
			if (this.addTime <= 0f)
			{
				ItemType itemType = Util.GetRandomRelicDrop()[0];
				object a = Game.Language.Get("card_" + this.cardId.ToString(), "");
				object colon = StringDefine.Colon;
				string normalColor = ColorDefine.NormalColor;
				LanguageManager language = Game.Language;
				string str = "pickitem_";
				int num = (int)itemType;
				Util.ShowTipsNoLanguage(PathDefine.Concat(a, colon, string.Format(normalColor, language.Get(str + num.ToString(), ""))));
				Util.OnLocalPlayerPickItem(itemType, 0);
			}
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00002D1D File Offset: 0x00000F1D
	public override void Exit()
	{
	}

	// Token: 0x04000385 RID: 901
	private float addTime = 1f;
}
