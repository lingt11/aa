using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200033C RID: 828
public class UIKingDecRelic : MonoBehaviour
{
	// Token: 0x060012F0 RID: 4848 RVA: 0x00071300 File Offset: 0x0006F500
	public void SetRelic(SaveLoadManager.PlayerKingRelicData relicData1, SaveLoadManager.PlayerKingRelicData relicData2)
	{
		if (string.IsNullOrEmpty(relicData1.relicName) && string.IsNullOrEmpty(relicData2.relicName))
		{
			return;
		}
		Dictionary<string, object> dic = (Dictionary<string, object>)ExcelManager.allExcelData["remains"].DIC(relicData1.relicName);
		this.image1.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Remains/" + dic.DIC("icon"));
		this.image1.color = ColorDefine.QuaUIColor[Game.GameData.RemainsDataDic[(ItemType)int.Parse(relicData1.relicName)].grade];
		this.text1.text = Util.GetLevelStarName(Game.Language.Get("pickitem_" + relicData1.relicName, ""), relicData1.relicLevel);
		if (string.IsNullOrEmpty(relicData2.relicName))
		{
			if (this.image2.gameObject.activeSelf)
			{
				this.image2.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			if (!this.image2.gameObject.activeSelf)
			{
				this.image2.gameObject.SetActive(true);
			}
			dic = (Dictionary<string, object>)ExcelManager.allExcelData["remains"].DIC(relicData2.relicName);
			this.image2.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Remains/" + dic.DIC("icon"));
			this.image2.color = ColorDefine.QuaUIColor[Game.GameData.RemainsDataDic[(ItemType)int.Parse(relicData2.relicName)].grade];
			this.text2.text = Util.GetLevelStarName(Game.Language.Get("pickitem_" + relicData2.relicName, ""), relicData2.relicLevel);
		}
	}

	// Token: 0x04001150 RID: 4432
	public Image image1;

	// Token: 0x04001151 RID: 4433
	public Text text1;

	// Token: 0x04001152 RID: 4434
	public Image image2;

	// Token: 0x04001153 RID: 4435
	public Text text2;
}
