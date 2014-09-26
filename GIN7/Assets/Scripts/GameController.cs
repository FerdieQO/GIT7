using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    private Sprite[] _actions;
    private Dictionary<String, Category> _categories;

    public GameObject cardPrefab;

    public GUISkin guiSkin;
    public System.Random randomizer;

    private bool _loaded = false;

    void Awake()
    {
        CardLoader.Load(out _actions, out _categories);
        if (GameObject.FindGameObjectsWithTag("GameController").Length != 1)
        {
            print("Multiple GameObjects with tag GameController.");
        }
        else if (cardPrefab == null)
        {
            print("CardPrefab is not assigned.");
        }
        else
        {
            _loaded = true;
        }
        SpawnButtonCards();
    }

    // Use this for initialization
    void Start()
    {
        randomizer = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnButtonCards()
    {
        float points = _categories.Count;
        float dX = Screen.width / points;
        float y = (Screen.height / 100 * 20);
        int i = 0;
        foreach (Category cat in _categories.Values)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(i * dX + (dX / 2), y, 0));
            targetPos.z = 0;
            Sprite emotion = cat.GetMainEmotion();

            var newCard = Instantiate(cardPrefab, targetPos, Quaternion.identity) as GameObject;
            if (newCard != null)
            {
                newCard.name = "ButtonCard";
                newCard.layer = LayerMask.NameToLayer("UI");
                var cardCont = newCard.GetComponent<CardController>();
                cardCont.SetCard(emotion, CardType.Emotion);
                i++;
            }
            else
            {
                print("Couldn't instantiate prefab.");
            }
        }
    }

    void OnGUI()
    {
        if (guiSkin == null)
        {
            guiSkin = GUI.skin;
        }
        if (!_loaded)
        {
            return;
        }


        /*
        float width = (Screen.width / _categories.Count) - (_categories.Count + 1);
        GUILayout.BeginArea(new Rect(0, Screen.height - (width + 20), Screen.width, (width + 20)));
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUIStyle style = guiSkin.label;
        GUILayout.FlexibleSpace();
        foreach (Category cat in _categories.Values)
        {
            GUILayout.BeginVertical();
            Sprite emotion = cat.GetMainEmotion();
            Texture2D tx = emotion.texture;
            if (GUILayout.Button(tx, style, GUILayout.Width(width)))
            {
                DragCard(emotion, CardType.Emotion);
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndArea();
        */


    }

    public static GameController GetGameController()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public bool ChangeButtonCard(CardController card)
    {
        var newCard = Instantiate(card.gameObject, card.transform.position, Quaternion.identity) as GameObject;
        if (newCard == null)
        {
            return false;
        }
        newCard.name = "ButtonCard";
        card.gameObject.layer = LayerMask.NameToLayer("Default");
        card.gameObject.name = "DragCard";
        return true;
    }

    public Sprite[] GetActions()
    {
        return _actions;
    }

    public Sprite GetRandomAction()
    {
        if (_actions.Length < 1)
        {
            return null;
        }
        int index = 0;
        if (_actions.Length > 1)
        {
            index = randomizer.Next(_actions.Length);
        }
        return (Sprite) _actions.GetValue(index);
    }

    public Dictionary<String, Sprite> GetEmotions(String categoryName)
    {
        Category category;
        return _categories.TryGetValue(categoryName, out category) ? category.GetEmotions() : null;
    }
}
