using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    public TMP_Text tutorialText;

    public void setTutorialText(string text)
    {
        tutorialText.text = text;
    }
}
