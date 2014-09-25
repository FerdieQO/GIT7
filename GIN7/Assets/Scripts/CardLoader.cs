using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CardLoader
{

    public static void Load(out Sprite[] actions, out Dictionary<String, Category> categories)
    {
        actions = Resources.LoadAll<Sprite>("Sprites/Actions");
        Sprite[] emotionSprites = Resources.LoadAll<Sprite>("Sprites/Emotions");
        Dictionary<String, Sprite> emotions = emotionSprites.ToDictionary(sprite => sprite.name);

        var categoriesAsset = Resources.Load<TextAsset>("Data/Emotions");
        categories = new Dictionary<String, Category>();
        var categoryItems = JObject.Parse(categoriesAsset.text);
        foreach (var cat in categoryItems["categories"])
        {
            var catName = (String) cat["Name"];
            var category = new Category();
            categories.Add(catName, category);

            foreach (var card in cat["cards"])
            {
                var emotionName = (String) card["Name"];
                var cardName = (String) card["SpriteName"];
                Sprite emotion;
                if (!emotions.TryGetValue(cardName, out emotion))
                {
                    Debug.Log("Couldn't find emotion '" + cardName + "' in the dictionary.");
                    continue;
                }
                category.AddEmotion(emotionName, emotion);
            }
        }
    }
}
