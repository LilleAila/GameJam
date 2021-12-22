using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputObject inputs;
    public static Dictionary<string, KeyCode[]> getCodes = new Dictionary<string, KeyCode[]>();
    public static Dictionary<string, AxisClass[]> getAxis = new Dictionary<string, AxisClass[]>();

    // [RuntimeInitializeOnLoadMethod]
    void Start()
    {
        reloadKeys();
    }

    public void reloadKeys()
    {
        getCodes = new Dictionary<string, KeyCode[]>();
        for (int i = 0; i < inputs.inputs.Length; i++)
        {
            getCodes.Add(inputs.inputs[i].name, inputs.inputs[i].keys);
        }

        getAxis = new Dictionary<string, AxisClass[]>();
        for(int i = 0; i < inputs.axes.Length; i++)
        {
            getAxis.Add(inputs.axes[i].name, inputs.axes[i].axes);
        }
    }


    public static bool GetKeyDown(string key)
    {
        bool down = false;
        KeyCode[] codes = getCodes[key];
        for(int i = 0; i < codes.Length; i++)
        {
            if(Input.GetKeyDown(codes[i]))
            {
                down = true;
                break;
            }
        }
        return down;
    }

    public static bool GetKeyUp(string key)
    {
        bool down = false;
        KeyCode[] codes = getCodes[key];
        for (int i = 0; i < codes.Length; i++)
        {
            if (Input.GetKeyUp(codes[i]))
            {
                down = true;
                break;
            }
        }
        return down;
    }

    public static bool GetKey(string key)
    {
        bool down = false;
        KeyCode[] codes = getCodes[key];
        for (int i = 0; i < codes.Length; i++)
        {
            if (Input.GetKey(codes[i]))
            {
                down = true;
                break;
            }
        }
        return down;
    }

    // With keys from the InputObject
    /*public static float GetAxis(string key)
    {
        float axis = 0;
        AxisClass[] axes = getAxis[key];
        for(int i = 0; i < axes.Length; i++)
        {
            if (GetKey(axes[i].positive))
            {
                axis = 1;
                break;
            } else if(GetKey(axes[i].negative))
            {
                axis = -1;
                break;
            }
        }
        return axis;
    }*/

    // With KeyCodes
    public static float GetAxis(string key)
    {
        float axis = 0;
        AxisClass[] axes = getAxis[key];
        for (int i = 0; i < axes.Length; i++)
        {
            if(Input.GetKey(axes[i].positive))
            {
                axis = 1;
                break;
            } else if(Input.GetKey(axes[i].negative))
            {
                axis = -1;
                break;
            }
        }
        return axis;
    }

    public static Vector2 GetMouse()
    {
        // return Input.mousePosition;
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}