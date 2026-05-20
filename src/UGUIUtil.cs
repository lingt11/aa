using System;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000062 RID: 98
public static class UGUIUtil
{
	// Token: 0x060001D7 RID: 471 RVA: 0x0000B014 File Offset: 0x00009214
	public static void AddButtonEvent(this Button button, UnityAction ac)
	{
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate()
		{
			ac();
			if (button.GetComponent<ButtonAudio>() != null)
			{
				button.GetComponent<ButtonAudio>().PlayButtonAudio();
			}
			if (Game.AudioManager != null)
			{
				Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			}
		});
	}
}
