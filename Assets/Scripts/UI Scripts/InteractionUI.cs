using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interaction_prompt_text;

    public void updatePromptText(string prompt_message)
    {
        interaction_prompt_text.text = prompt_message;
    }
}
