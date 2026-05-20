using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000378 RID: 888
public class UI_QTEMusic : UGUICtrl
{
	// Token: 0x06001458 RID: 5208 RVA: 0x0007EA28 File Offset: 0x0007CC28
	public UI_QTEMusic()
	{
		this.selfView = new UI_QTEMusic_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_qTEMusic", base.GetType());
		this.arrowRectTransform = this.selfView.trans_arrow.gameObject.GetComponent<RectTransform>();
		this.bgRectTransform = this.selfView.trans_bg.gameObject.GetComponent<RectTransform>();
		int childCount = this.selfView.trans_trigger.childCount;
		this.checkImages = new Image[childCount];
		for (int i = 0; i < childCount; i++)
		{
			this.checkImages[i] = this.selfView.trans_trigger.GetChild(i).GetComponent<Image>();
		}
		this.checkResults = new int[childCount];
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x0007EB44 File Offset: 0x0007CD44
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.arrowRectTransform.anchoredPosition = Vector2.zero;
		this.timer = 0f;
		this.checkImageIndex = 0;
		int num = this.checkImages.Length;
		for (int i = 0; i < num; i++)
		{
			this.checkImages[i].color = this.ReadyColor;
			this.checkResults[i] = 0;
		}
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x0007EBAC File Offset: 0x0007CDAC
	public override void Update()
	{
		this.timer += Time.deltaTime;
		this.arrowRectTransform.anchoredPosition = new Vector2(Mathf.Lerp(0f, this.bgRectTransform.sizeDelta.x, this.timer / 3.711f), 0f);
		if (this.checkImageIndex >= this.checkImages.Length)
		{
			return;
		}
		Image image = this.checkImages[this.checkImageIndex];
		if (Input.GetMouseButtonDown(0) && this.checkResults[this.checkImageIndex] == 0)
		{
			if (Mathf.Abs(this.arrowRectTransform.anchoredPosition.x - image.rectTransform.anchoredPosition.x) < image.rectTransform.sizeDelta.x / 2f)
			{
				image.color = this.SuccessColor;
				this.checkResults[this.checkImageIndex] = 2;
			}
			else
			{
				image.color = this.FailColor;
				this.checkResults[this.checkImageIndex] = 1;
			}
		}
		if (this.arrowRectTransform.anchoredPosition.x > image.rectTransform.anchoredPosition.x + image.rectTransform.sizeDelta.x / 2f)
		{
			if (this.checkResults[this.checkImageIndex] == 0)
			{
				image.color = this.FailColor;
				this.checkResults[this.checkImageIndex] = 1;
			}
			this.checkImageIndex++;
		}
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x0007ED24 File Offset: 0x0007CF24
	public bool IsSussess()
	{
		int num = this.checkResults.Length;
		for (int i = 0; i < num; i++)
		{
			if (this.checkResults[i] != 2)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04001302 RID: 4866
	public UI_QTEMusic_View selfView;

	// Token: 0x04001303 RID: 4867
	private RectTransform arrowRectTransform;

	// Token: 0x04001304 RID: 4868
	private RectTransform bgRectTransform;

	// Token: 0x04001305 RID: 4869
	public const float PlayTime = 3.711f;

	// Token: 0x04001306 RID: 4870
	private float timer;

	// Token: 0x04001307 RID: 4871
	private Image[] checkImages;

	// Token: 0x04001308 RID: 4872
	private int checkImageIndex;

	// Token: 0x04001309 RID: 4873
	private int[] checkResults;

	// Token: 0x0400130A RID: 4874
	private readonly Color ReadyColor = new Color(1f, 0.8812718f, 0f, 0.651f);

	// Token: 0x0400130B RID: 4875
	private readonly Color SuccessColor = new Color(0f, 1f, 0f, 0.8f);

	// Token: 0x0400130C RID: 4876
	private readonly Color FailColor = new Color(1f, 0f, 0f, 0.8f);
}
