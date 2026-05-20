using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200012A RID: 298
public class ItemUITouch : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060005A0 RID: 1440 RVA: 0x00020AB5 File Offset: 0x0001ECB5
	private void Start()
	{
		this.sell.gameObject.SetActive(false);
		this.discord.gameObject.SetActive(false);
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00020ADC File Offset: 0x0001ECDC
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.sell.gameObject.SetActive(true);
		this.discord.gameObject.SetActive(true);
		this.sell.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-72.2f, -39f);
		this.discord.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-127.4f, -39f);
		this.sell.GetComponent<CanvasGroup>().alpha = 0f;
		this.discord.GetComponent<CanvasGroup>().alpha = 0f;
		this.sell.transform.GetComponent<RectTransform>().DOAnchorPosX(this.sellPos, 0.2f, false);
		this.discord.transform.GetComponent<RectTransform>().DOAnchorPosX(this.discordPos, 0.2f, false);
		this.sell.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
		this.discord.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00020AB5 File Offset: 0x0001ECB5
	public void OnPointerExit(PointerEventData eventData)
	{
		this.sell.gameObject.SetActive(false);
		this.discord.gameObject.SetActive(false);
	}

	// Token: 0x04000813 RID: 2067
	public GameObject sell;

	// Token: 0x04000814 RID: 2068
	public GameObject discord;

	// Token: 0x04000815 RID: 2069
	public float sellPos = -76f;

	// Token: 0x04000816 RID: 2070
	public float discordPos = -134.1f;
}
