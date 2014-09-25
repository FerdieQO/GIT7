using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Category
{
    private Dictionary<String, Sprite> _emotions;
    private String _main;

    public Category()
    {
        _emotions = new Dictionary<String, Sprite>();
    }

    public void AddEmotion(String name, Sprite sprite)
    {
        _emotions.Add(name, sprite);
        if (_main == null)
        {
            _main = name;
        }
    }

    public Dictionary<String, Sprite> GetEmotions()
    {
        return _emotions;
    }

    public Sprite GetMainEmotion()
    {
        Sprite emotion;
        return _emotions.TryGetValue(_main, out emotion) ? emotion : null;
    }

}