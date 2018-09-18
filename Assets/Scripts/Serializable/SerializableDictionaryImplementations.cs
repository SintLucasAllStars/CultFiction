using System;
 
using UnityEngine;
 
// ---------------
//  String => Int
// ---------------
[Serializable]
public class StringIntDictionary : SerializableDictionary<string, int> {}
 
// ---------------
//  GameObject => Float
// ---------------
[Serializable]
public class GameObjectFloatDictionary : SerializableDictionary<GameObject, float> {}


// ---------------
//  GameObject => Int
// ---------------
[Serializable]
public class GameObjectIntDictionary : SerializableDictionary<GameObject, int> { }
