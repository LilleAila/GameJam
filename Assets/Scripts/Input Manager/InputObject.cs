using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Input Object", menuName = "Input Object")]
public class InputObject : ScriptableObject
{
    public InputClass[] inputs;
    public AxesClass[] axes;
}

[System.Serializable]
public class InputClass
{
    public string name;
    public KeyCode[] keys;
}

[System.Serializable]
public class AxesClass
{
    public string name;
    public AxisClass[] axes;
}

[System.Serializable]
public class AxisClass
{
    // With keys from this object
    /* public string positive;
    public string negative; */

    // With KeyCodes
    public KeyCode positive;
    public KeyCode negative;
}