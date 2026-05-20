using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000355 RID: 853
public class UIRankHeroTouch : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x0600138E RID: 5006 RVA: 0x00078EA1 File Offset: 0x000770A1
	private void Awake()
	{
		this.uiRankHeroItem = base.gameObject.GetComponent<UIRankHeroItem>();
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x00078EB4 File Offset: 0x000770B4
	public void OnPointerEnter(PointerEventData eventData)
	{
		MySystemEvent.Instance.DispatchMessage<Vector3>(39, base.transform.position);
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x00078ECD File Offset: 0x000770CD
	public void OnPointerExit(PointerEventData eventData)
	{
		MySystemEvent.Instance.DispatchMessage(40);
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x00078EDB File Offset: 0x000770DB
	public void OnPointerClick(PointerEventData eventData)
	{
		UI_KingDec ui_KingDec = Game.UI.OpenUI<UI_KingDec>(null) as UI_KingDec;
		if (ui_KingDec == null)
		{
			return;
		}
		ui_KingDec.SetPlayKingData(this.uiRankHeroItem.PlayerKingData);
	}

	// Token: 0x04001222 RID: 4642
	private UIRankHeroItem uiRankHeroItem;
}
