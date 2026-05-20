using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000324 RID: 804
public class UI_GuideEquipItem : MonoBehaviour
{
	// Token: 0x0600127F RID: 4735 RVA: 0x0006E370 File Offset: 0x0006C570
	private void Awake()
	{
		this.button.AddButtonEvent(new UnityAction(this.OnBtnClick));
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x0006E38C File Offset: 0x0006C58C
	public void SetEquip(Dictionary<string, object> data)
	{
		this.equipData = data;
		this.equipIndex = int.Parse(data.DIC("id"));
		string key = PathDefine.Concat("equip_", this.equipIndex);
		this.skillNameText.text = Game.Language.Get(key, "");
		this.skillIcon.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Shop/" + data.DIC("equipmentIcon"));
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x0006E40C File Offset: 0x0006C60C
	private void OnBtnClick()
	{
		EventSystem.current.SetSelectedGameObject(null);
		Game.UI.GetUI<UI_GuideEquip>().ShowDec(this.skillNameText.text, EquipBase.GetEquipInfo("equip_" + this.equipIndex.ToString()), this.skillIcon.sprite);
	}

	// Token: 0x040010BA RID: 4282
	public Button button;

	// Token: 0x040010BB RID: 4283
	public Image skillIcon;

	// Token: 0x040010BC RID: 4284
	public Text skillNameText;

	// Token: 0x040010BD RID: 4285
	private int equipIndex;

	// Token: 0x040010BE RID: 4286
	private Dictionary<string, object> equipData;
}
