using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSelectOnEnable : MonoBehaviour
{
	private Button button;

	private void OnEnable()
	{
		button = GetComponent<Button>();
		button.Select();
		button.OnSelect(null);
	}
}
