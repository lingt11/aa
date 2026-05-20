using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000366 RID: 870
public class MyTalkUI : MonoBehaviour
{
	// Token: 0x170000CF RID: 207
	// (get) Token: 0x060013CF RID: 5071 RVA: 0x0007A4BD File Offset: 0x000786BD
	public bool IsOver
	{
		get
		{
			return this.isOver;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0007A4C5 File Offset: 0x000786C5
	public RoleBase Role
	{
		get
		{
			return this.role;
		}
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x0007A4CD File Offset: 0x000786CD
	public void ShowTalkText(RoleBase roleValue, string[] showStrsValue, float showTimeValue)
	{
		this.showStrs = showStrsValue;
		this.showIndex = 0;
		this.role = roleValue;
		this.isOver = false;
		this.showTime = showTimeValue;
		this.ShowText();
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x0007A4F8 File Offset: 0x000786F8
	private void ShowText()
	{
		this.talkText.text = this.showStrs[this.showIndex];
		this.showNextTime = Time.time + this.showTime;
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x0007A524 File Offset: 0x00078724
	public void UpdatePosition()
	{
		if (this.isOver)
		{
			return;
		}
		if (Time.time > this.showNextTime)
		{
			this.showIndex++;
			if (this.showIndex >= this.showStrs.Length)
			{
				this.isOver = true;
				return;
			}
			this.ShowText();
		}
		this.rectTransform.anchoredPosition = Util.GetScreenPosition(this.role.GetHeadUIPos());
	}

	// Token: 0x0400126C RID: 4716
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x0400126D RID: 4717
	[SerializeField]
	private Text talkText;

	// Token: 0x0400126E RID: 4718
	private RoleBase role;

	// Token: 0x0400126F RID: 4719
	private bool isOver;

	// Token: 0x04001270 RID: 4720
	private int showIndex;

	// Token: 0x04001271 RID: 4721
	private float showNextTime;

	// Token: 0x04001272 RID: 4722
	private float showTime = 5f;

	// Token: 0x04001273 RID: 4723
	private string[] showStrs;
}
