using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000365 RID: 869
public class MoveUI : MonoBehaviour
{
	// Token: 0x060013CC RID: 5068 RVA: 0x0007A48C File Offset: 0x0007868C
	private void Start()
	{
		this.myPos = base.transform.position;
		base.transform.DOLocalMoveY(85f, 0.5f, false).SetLoops(-1, LoopType.Yoyo);
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void Update()
	{
	}

	// Token: 0x0400126B RID: 4715
	private Vector3 myPos;
}
