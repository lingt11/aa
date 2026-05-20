using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200035C RID: 860
public class UI_MyRank_View : UGUIView
{
	// Token: 0x060013AD RID: 5037 RVA: 0x00079B6C File Offset: 0x00077D6C
	public override void Init(Transform trans)
	{
		this.trans_load = trans.GetChild(1).GetChild(1).GetComponent<Transform>();
		this.pool_rank = trans.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetComponent<PoolView>();
		this.btn_back = trans.GetChild(2).GetChild(0).GetComponent<Button>();
		this.btn_one = trans.GetChild(4).GetChild(0).GetComponent<Button>();
		this.btn_two = trans.GetChild(4).GetChild(1).GetComponent<Button>();
		this.btn_thr = trans.GetChild(4).GetChild(2).GetComponent<Button>();
		this.btn_four = trans.GetChild(4).GetChild(3).GetComponent<Button>();
		this.text_rankOne = trans.GetChild(5).GetChild(1).GetComponent<TMP_Text>();
		this.img_rankOne = trans.GetChild(5).GetChild(1).GetChild(3).GetComponent<Image>();
		this.text_rankTwo = trans.GetChild(5).GetChild(2).GetComponent<TMP_Text>();
		this.img_rankTwo = trans.GetChild(5).GetChild(2).GetChild(3).GetComponent<Image>();
		this.text_rankThree = trans.GetChild(5).GetChild(3).GetComponent<TMP_Text>();
		this.img_rankThree = trans.GetChild(5).GetChild(3).GetChild(3).GetComponent<Image>();
		this.text_rankFour = trans.GetChild(5).GetChild(4).GetComponent<TMP_Text>();
		this.img_rankFour = trans.GetChild(5).GetChild(4).GetChild(3).GetComponent<Image>();
		this.trans_heroTip = trans.GetChild(6).GetComponent<Transform>();
	}

	// Token: 0x0400123D RID: 4669
	public Transform trans_load;

	// Token: 0x0400123E RID: 4670
	public PoolView pool_rank;

	// Token: 0x0400123F RID: 4671
	public Button btn_back;

	// Token: 0x04001240 RID: 4672
	public Button btn_one;

	// Token: 0x04001241 RID: 4673
	public Button btn_two;

	// Token: 0x04001242 RID: 4674
	public Button btn_thr;

	// Token: 0x04001243 RID: 4675
	public Button btn_four;

	// Token: 0x04001244 RID: 4676
	public TMP_Text text_rankOne;

	// Token: 0x04001245 RID: 4677
	public Image img_rankOne;

	// Token: 0x04001246 RID: 4678
	public TMP_Text text_rankTwo;

	// Token: 0x04001247 RID: 4679
	public Image img_rankTwo;

	// Token: 0x04001248 RID: 4680
	public TMP_Text text_rankThree;

	// Token: 0x04001249 RID: 4681
	public Image img_rankThree;

	// Token: 0x0400124A RID: 4682
	public TMP_Text text_rankFour;

	// Token: 0x0400124B RID: 4683
	public Image img_rankFour;

	// Token: 0x0400124C RID: 4684
	public Transform trans_heroTip;
}
