using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000013 RID: 19
public class MyPointClick : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x0600003B RID: 59 RVA: 0x00002DD8 File Offset: 0x00000FD8
	public void OnPointerClick(PointerEventData eventData)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
	}
}
