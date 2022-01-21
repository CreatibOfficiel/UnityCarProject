using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menuButtonPrototype;
    [SerializeField]
    private GameObject menuParameters;

    void Start()
    {
        Utils.UIUtils.ClearChildren(menuButtonPrototype.transform.parent);
        AddMenuButton("Jouer", () =>
        {
            if (GameManager.GetInstance().gamemode == GameManager.GameMode.STANDART)
                SceneController.GetInstance().loadScene(1);

            if (GameManager.GetInstance().gamemode == GameManager.GameMode.CHRONOMODE)
                    SceneController.GetInstance().loadScene(2);
        });

        AddMenuButton("Paramètres", () =>
        {
            menuParameters.SetActive(!menuParameters.activeSelf);
        });

        AddMenuButton("Quitter", () =>
        {
            Application.Quit();
        });
    }

    private void AddMenuButton(string title, UnityAction clickAction)
    {
        GameObject menuButton = Instantiate(menuButtonPrototype, menuButtonPrototype.transform.parent);
        menuButton.transform.Find("Text").GetComponent<Text>().text = title;

        RectTransform line = menuButton.transform.Find("Panel").GetComponent<RectTransform>(); ;

        Utils.UIUtils.AddTrigger(menuButton.transform, UnityEngine.EventSystems.EventTriggerType.PointerClick, (eventData) =>
        {
            clickAction?.Invoke();
        });

        Utils.UIUtils.AddTrigger(menuButton.transform, UnityEngine.EventSystems.EventTriggerType.PointerEnter, (eventData) =>
        {
            menuButton.transform.Find("Text").GetComponent<Text>().fontSize += 6; 
            line.sizeDelta = new Vector2(598, 6);
        });
        
        Utils.UIUtils.AddTrigger(menuButton.transform, UnityEngine.EventSystems.EventTriggerType.PointerExit, (eventData) =>
        {
            menuButton.transform.Find("Text").GetComponent<Text>().fontSize -= 6;
            line.sizeDelta = new Vector2(598, 3);
        });

        menuButton.SetActive(true);
    }
}
