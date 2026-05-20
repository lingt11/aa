using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000165 RID: 357
public class BtnAnimXue : UINavigation, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x060006FB RID: 1787 RVA: 0x0002ACC8 File Offset: 0x00028EC8
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.selectSelectAction = delegate()
		{
			this.OnPointerEnter(null);
		};
		this.selectNormalAction = delegate()
		{
			this.OnPointerExit(null);
		};
		this.selectPressAction = delegate()
		{
			this.OnPointerDown(null);
		};
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0002AD18 File Offset: 0x00028F18
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.btnImage.sprite = this.select;
		if (this.btnText != null)
		{
			this.btnText.color = this.selectColor;
		}
		this.Reset();
		this.quence.Append(base.transform.DOScale(1.2f, 0.1f));
		this.quence.Append(base.transform.DOScale(1.1f, 0.1f));
		this.quence.Append(base.transform.DOScale(1.2f, 0.1f));
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0002ADC0 File Offset: 0x00028FC0
	private void OnEnable()
	{
		base.transform.localScale = Vector3.one;
		this.btnImage.sprite = this.normal;
		if (this.btnText != null)
		{
			this.btnText.color = this.normalColor;
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0002AE10 File Offset: 0x00029010
	public void OnPointerExit(PointerEventData eventData)
	{
		this.btnImage.sprite = this.normal;
		if (this.btnText != null)
		{
			this.btnText.color = this.normalColor;
		}
		this.Reset();
		this.quence.Append(base.transform.DOScale(1f, 0.1f));
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0002AE74 File Offset: 0x00029074
	public void OnPointerDown(PointerEventData eventData)
	{
		this.Reset();
		this.quence.Append(base.transform.DOScale(0.9f, 0.1f));
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0002AE9D File Offset: 0x0002909D
	public void OnPointerUp(PointerEventData eventData)
	{
		this.Reset();
		this.quence.Append(base.transform.DOScale(1.2f, 0.1f));
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0002AEC6 File Offset: 0x000290C6
	private void Reset()
	{
		if (this.quence != null)
		{
			this.quence.Kill(false);
		}
		this.quence = DOTween.Sequence();
	}

	// Token: 0x04000B12 RID: 2834
	public Image btnImage;

	// Token: 0x04000B13 RID: 2835
	public Text btnText;

	// Token: 0x04000B14 RID: 2836
	public Sprite normal;

	// Token: 0x04000B15 RID: 2837
	public Sprite select;

	// Token: 0x04000B16 RID: 2838
	public Color normalColor;

	// Token: 0x04000B17 RID: 2839
	public Color selectColor;

	// Token: 0x04000B18 RID: 2840
	private Sequence quence;
}
