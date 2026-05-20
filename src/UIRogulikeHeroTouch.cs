using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000382 RID: 898
public class UIRogulikeHeroTouch : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x0600147E RID: 5246 RVA: 0x0007F627 File Offset: 0x0007D827
	private void Awake()
	{
		this.kingHeadItem = base.gameObject.GetComponent<KingHeadItem>();
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x00078EB4 File Offset: 0x000770B4
	public void OnPointerEnter(PointerEventData eventData)
	{
		MySystemEvent.Instance.DispatchMessage<Vector3>(39, base.transform.position);
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x00078ECD File Offset: 0x000770CD
	public void OnPointerExit(PointerEventData eventData)
	{
		MySystemEvent.Instance.DispatchMessage(40);
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x0007F63A File Offset: 0x0007D83A
	public void OnPointerClick(PointerEventData eventData)
	{
		UI_KingDec ui_KingDec = Game.UI.OpenUI<UI_KingDec>(null) as UI_KingDec;
		if (ui_KingDec == null)
		{
			return;
		}
		ui_KingDec.SetPlayKingData(this.kingHeadItem.KingData);
	}

	// Token: 0x04001329 RID: 4905
	private KingHeadItem kingHeadItem;
}
