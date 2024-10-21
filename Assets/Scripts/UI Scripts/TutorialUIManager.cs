using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
		public TMP_Text tutorial_text;

		public void setTutorialText(string text)
		{
				tutorial_text.text = text;
		}
}
