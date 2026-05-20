using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class DamageNum : MonoBehaviour
{
	// Token: 0x06000AC8 RID: 2760 RVA: 0x000374A8 File Offset: 0x000356A8
	public void Init(long num, bool isCrit, AttackType attackType)
	{
		this.textMeshPro.text = num.ToString();
		int num2 = 0;
		if (isCrit)
		{
			this.textMeshPro.color = new Color(1f, 0.5f, 0f);
			num2 = 10;
		}
		else if (attackType == AttackType.Normal)
		{
			this.textMeshPro.color = Color.white;
		}
		else if (attackType == AttackType.Skill)
		{
			this.textMeshPro.color = new Color(0f, 0.953125f, 0.9453125f);
		}
		else
		{
			this.textMeshPro.color = new Color(0.8627451f, 0.08235294f, 1f);
		}
		float num3 = Mathf.Min((float)num / this.maxDamage * (float)this.maxFontSize, (float)this.maxFontSize);
		if (num3 < (float)this.minFontSize)
		{
			num3 = (float)this.minFontSize;
		}
		if (num3 > (float)this.maxFontSize)
		{
			num3 = (float)this.maxFontSize;
		}
		this.textMeshPro.fontSize = (float)((int)num3 + num2);
		int num4 = (Random.Range(0, 2) == 1) ? 1 : -1;
		float num5 = Random.Range(30f, 60f);
		float y = Random.Range(30f, 60f);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(base.transform.DOLocalMove(new Vector3(num5 * (float)num4, -5f, 0f), 0.1f, false));
		sequence.Append(base.transform.DOLocalMove(new Vector3(num5 * 2f * (float)num4, y, 0f), 0.8f, false));
		sequence.onComplete = delegate()
		{
			base.transform.parent.gameObject.UnLoadPrefabNotMove();
		};
		Sequence s = DOTween.Sequence();
		s.Append(base.transform.DOScale(Vector3.one * 0.8f, 0.1f));
		s.Append(base.transform.DOScale(Vector3.one * 1f, 0.3f));
		s.Append(this.textMeshPro.DOFade(0f, 0.5f));
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x000376A8 File Offset: 0x000358A8
	public void Init(int num)
	{
		this.textMeshPro.text = "+" + num.ToString() + "g";
		this.textMeshPro.color = new Color(255f, 200f, 0f);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(base.transform.DOLocalMove(new Vector3(0f, 60f, 0f), 1f, false));
		sequence.onComplete = delegate()
		{
			base.transform.parent.gameObject.UnLoadPrefabNotMove();
		};
		this.textMeshPro.fontSize = 30f;
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x00037748 File Offset: 0x00035948
	public void ShowDoge()
	{
		this.textMeshPro.text = StringDefine.Miss;
		this.textMeshPro.color = new Color(0.9921569f, 0.1176471f, 0.1803922f);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(base.transform.DOLocalMove(new Vector3(0f, 60f, 0f), 1f, false));
		sequence.onComplete = delegate()
		{
			base.transform.parent.gameObject.UnLoadPrefabNotMove();
		};
		this.textMeshPro.fontSize = 30f;
	}

	// Token: 0x04000BF8 RID: 3064
	public TextMeshProUGUI textMeshPro;

	// Token: 0x04000BF9 RID: 3065
	private float maxDamage = 500000f;

	// Token: 0x04000BFA RID: 3066
	private int minFontSize = 40;

	// Token: 0x04000BFB RID: 3067
	private int maxFontSize = 100;
}
