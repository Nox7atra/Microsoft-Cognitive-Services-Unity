using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SubscriptionKeys : ScriptableObject
{
    private const string DEFAULT_TEXT = "Write your subscription key here";
    private const string ASSET_NAME = "SubscriptionKeys.asset";
    private const string ASSET_FOLDER = "Assets/Plugins/MicrosoftCognitive";
    [MenuItem("Microsoft Cognitive/Subscription Keys/Create default")]
    public static void CreateDefault()
    {
        var path = Path.Combine(ASSET_FOLDER, ASSET_NAME);
        AssetDatabase.CreateAsset(new SubscriptionKeys(), path);
    }
    static SubscriptionKeys _Instance;

    public static SubscriptionKeys Instance
    {
        get
        {
            if (_Instance == null)
            {
                var path = Path.Combine(ASSET_FOLDER, ASSET_NAME);
                _Instance = AssetDatabase.LoadAssetAtPath<SubscriptionKeys>(path);

                if (_Instance == null)
                {
                    _Instance = CreateInstance<SubscriptionKeys>();
                    AssetDatabase.CreateAsset(_Instance, path);
                }
            }

            return _Instance;
        }
    }
    
    public string EmotionsApiKey;
    public string FaceApiKey;

    private SubscriptionKeys()
    {
        EmotionsApiKey = DEFAULT_TEXT;
        FaceApiKey = DEFAULT_TEXT;
    }
}
