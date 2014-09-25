using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    private Sprite[] _actions;
    private Dictionary<String, Category> _categories;

    public GUISkin guiSkin;
    public System.Random randomizer;

    private bool _loaded = false;

    void Awake()
    {
        CardLoader.Load(out _actions, out _categories);
        _loaded = true;
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
            Texture2D tx = cat.GetMainEmotion().texture;
            GUILayout.Box(tx, style, GUILayout.Width(width));
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndArea();
         */
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
