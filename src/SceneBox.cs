using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class SceneBox : MonoBehaviour
{
	// Token: 0x0600070E RID: 1806 RVA: 0x0002AFF2 File Offset: 0x000291F2
	private void Awake()
	{
		this.animator.Play("boxidle", 0, 1f);
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0002B00A File Offset: 0x0002920A
	private void OnEnable()
	{
		MySystemEvent.Instance.RegisterMessage(20, new Action<Body>(this.BoxIdle));
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0002B024 File Offset: 0x00029224
	private void OnDisable()
	{
		MySystemEvent.Instance.UnregisterMessage(20, new Action<Body>(this.BoxIdle));
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0002B040 File Offset: 0x00029240
	public void OpenBox()
	{
		if (this.isOpening)
		{
			return;
		}
		this.isOpening = true;
		this.animator.Play("openbox");
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/safe_open", 1f, 3f);
		Game.TimerManager.AddTimer(0.3f, delegate()
		{
			Game.UI.OpenUI<UI_MyCard>(null);
		});
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0002B0B6 File Offset: 0x000292B6
	public void BoxIdle(Body body)
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/safe_close", 1f, 3f);
		this.isOpening = false;
		this.animator.Play("boxidle");
	}

	// Token: 0x04000B1F RID: 2847
	public Animator animator;

	// Token: 0x04000B20 RID: 2848
	private bool isOpening;
}
