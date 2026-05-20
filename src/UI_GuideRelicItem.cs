using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000330 RID: 816
public class UI_GuideRelicItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060012B9 RID: 4793 RVA: 0x0006FB8C File Offset: 0x0006DD8C
	private void Awake()
	{
		this.myTransform = base.transform;
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x0006FB9C File Offset: 0x0006DD9C
	public void SetRelic(ItemType itemTypeValue, int quality)
	{
		this.itemType = itemTypeValue;
		object dic = ExcelManager.allExcelData["remains"];
		int num = (int)this.itemType;
		object dic2 = dic.DIC(num.ToString());
		this.icon.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Remains/" + dic2.DIC("icon"));
		this.icon.color = ColorDefine.QuaUIColor[quality];
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x0006FC10 File Offset: 0x0006DE10
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.myTransform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.15f).SetEase(Ease.OutBack);
		object dic = ExcelManager.allExcelData["remains"];
		int num = (int)this.itemType;
		Dictionary<string, object> relicData = (Dictionary<string, object>)dic.DIC(num.ToString());
		UGUIManager ui = Game.UI;
		if (ui == null)
		{
			return;
		}
		UI_GuideRelic ui2 = ui.GetUI<UI_GuideRelic>();
		if (ui2 == null)
		{
			return;
		}
		Vector3 position = this.myTransform.position;
		LanguageManager language = Game.Language;
		string str = "pickitem_";
		num = (int)this.itemType;
		string relicName = language.Get(str + num.ToString(), "");
		LanguageManager language2 = Game.Language;
		string str2 = "pickitem_";
		num = (int)this.itemType;
		ui2.ShowRelicInfo(position, relicName, RelicBase.GetFormatDec(language2.Get(str2 + num.ToString() + "_m", ""), relicData));
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x0006FCF0 File Offset: 0x0006DEF0
	public void OnPointerExit(PointerEventData eventData)
	{
		this.myTransform.DOScale(new Vector3(1f, 1f, 1f), 0.15f).SetEase(Ease.OutQuad);
		UGUIManager ui = Game.UI;
		if (ui == null)
		{
			return;
		}
		UI_GuideRelic ui2 = ui.GetUI<UI_GuideRelic>();
		if (ui2 == null)
		{
			return;
		}
		ui2.HideRelicInfo();
	}

	// Token: 0x04001102 RID: 4354
	public Image icon;

	// Token: 0x04001103 RID: 4355
	private ItemType itemType;

	// Token: 0x04001104 RID: 4356
	private Transform myTransform;
}
