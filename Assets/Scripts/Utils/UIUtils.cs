using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Utils
{
    public class UIUtils : MonoBehaviour
    {
        public static void ClearChildren(Transform parentGameObject)
        {
            foreach (Transform child in parentGameObject)
            {
                if (child.gameObject.activeSelf) Destroy(child.gameObject);
            }
        }

        public static void AddTrigger(Transform go, EventTriggerType type, UnityAction<BaseEventData> action, bool clear = false)
        {
            if (go == null) return;
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(action);
            EventTrigger trigger = go.GetComponent<EventTrigger>();
            if (trigger == null) trigger = go.gameObject.AddComponent<EventTrigger>();
            if (clear) trigger.triggers.Clear();
            trigger.triggers.Add(entry);
        }
    }
}