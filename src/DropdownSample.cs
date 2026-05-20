using System;
using TMPro;
using UnityEngine;

// Token: 0x02000411 RID: 1041
public class DropdownSample : MonoBehaviour
{
	// Token: 0x060017B6 RID: 6070 RVA: 0x000941F8 File Offset: 0x000923F8
	public void OnButtonClick()
	{
		this.text.text = ((this.dropdownWithPlaceholder.value > -1) ? ("Selected values:\n" + this.dropdownWithoutPlaceholder.value.ToString() + " - " + this.dropdownWithPlaceholder.value.ToString()) : "Error: Please make a selection");
	}

	// Token: 0x040016C3 RID: 5827
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x040016C4 RID: 5828
	[SerializeField]
	private TMP_Dropdown dropdownWithoutPlaceholder;

	// Token: 0x040016C5 RID: 5829
	[SerializeField]
	private TMP_Dropdown dropdownWithPlaceholder;
}
