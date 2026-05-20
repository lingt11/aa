using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000367 RID: 871
public class PlayerState_Bag : MonoBehaviour
{
	// Token: 0x060013D5 RID: 5077 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void Init()
	{
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x0007A5A1 File Offset: 0x000787A1
	public void ClearBag()
	{
		this.poolView.RemoveAllView();
		this.bagList.Clear();
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x0007A5B9 File Offset: 0x000787B9
	public List<GameObject> GetList()
	{
		return this.bagList;
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x0007A5C4 File Offset: 0x000787C4
	public bool AddItem(BagItem bagItem)
	{
		GameObject gameObject = this.poolView.AddView();
		this.bagList.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		Image component = gameObject.transform.GetChild(0).GetComponent<Image>();
		if (bagItem.bagItemType == BagItemType.Remains)
		{
			object dic = ExcelManager.allExcelData["remains"];
			int bookType = (int)bagItem.bookType;
			Dictionary<string, object> dic2 = (Dictionary<string, object>)dic.DIC(bookType.ToString());
			component.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Remains/" + dic2.DIC("icon"));
			gameObject.GetComponent<DraggableItem>().dic = dic2;
			gameObject.GetComponent<DraggableItem>().bagItem = bagItem;
			component.color = ColorDefine.QuaUIColor[Game.GameData.RemainsDataDic[bagItem.bookType].grade];
		}
		else if (bagItem.bagItemType == BagItemType.Card)
		{
			CardData cardData = Game.GameData.CardDataDic[bagItem.bookType - ItemType.Card_0];
			component.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Card/" + cardData.icon);
			gameObject.GetComponent<DraggableItem>().bagItem = bagItem;
			component.color = Color.white;
		}
		else
		{
			Dictionary<string, object> dic3 = (Dictionary<string, object>)ExcelManager.allExcelData["shop"].DIC(bagItem.id);
			gameObject.GetComponent<DraggableItem>().dic = dic3;
			gameObject.GetComponent<DraggableItem>().bagItem = bagItem;
			string str = dic3.DIC("icon");
			component.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Shop/" + str);
			component.color = Color.white;
		}
		return true;
	}

	// Token: 0x04001274 RID: 4724
	public PoolView poolView;

	// Token: 0x04001275 RID: 4725
	public List<GameObject> bagList = new List<GameObject>();
}
