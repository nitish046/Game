using UnityEditor;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if(target.GetType() == typeof(InteractionEventOnly))
        {
            interactable.prompt_message = EditorGUILayout.TextField("Prompt Message", interactable.prompt_message);
            EditorGUILayout.HelpBox("InteractionEventOnly can only use UnityEvents", MessageType.Info);
            addInteractionEventsOnly(interactable);
        }
        else
        {
            base.OnInspectorGUI();
            if (interactable.use_events)
            {
                addInteractionEvents(interactable);
            }
            else
            {
                removeInteractionEvents(interactable);
            }
        }
    }

    public void addInteractionEvents(Interactable interactable)
    {
        if (interactable.GetComponent<InteractionEvent>() == null)
        {
            interactable.gameObject.AddComponent<InteractionEvent>();
        }

    }

    public void addInteractionEventsOnly(Interactable interactable)
    {
        if (interactable.GetComponent<InteractionEvent>() == null)
        {
            interactable.use_events = true;
            interactable.gameObject.AddComponent<InteractionEvent>();
        }

    }

    public void removeInteractionEvents(Interactable interactable)
    {
        if (interactable.GetComponent<InteractionEvent>() != null)
        {
            DestroyImmediate(interactable.GetComponent<InteractionEvent>());
        }
    }
}
