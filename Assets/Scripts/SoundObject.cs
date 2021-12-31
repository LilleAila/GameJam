using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Object", menuName = "Sounds")]
public class SoundObject : ScriptableObject
{
    public Sound[] sounds;
}
