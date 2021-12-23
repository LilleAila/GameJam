using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowInputs : MonoBehaviour
{
    public InputObject inputs;
    public GameObject inputsParent;
    public GameObject inputPrefab;
    public GameObject keyPrefab;
    public GameObject axisPrefab;

    void Start()
    {
        foreach(Transform child in inputsParent.transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < inputs.inputs.Length; i++)
        {
            GameObject inputGameObject = Instantiate(inputPrefab);
            inputGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = inputs.inputs[i].name;
            int a = i;
            inputGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            inputGameObject.GetComponent<Button>().onClick.AddListener(() => setButton(a));
            inputGameObject.transform.SetParent(inputsParent.transform, false);
        }

        for(int i = 0; i < inputs.axes.Length; i++)
        {
            GameObject inputGameObject = Instantiate(inputPrefab);
            inputGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = inputs.axes[i].name;
            int a = i;
            inputGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            inputGameObject.GetComponent<Button>().onClick.AddListener(() => setAxis(a));
            inputGameObject.transform.SetParent(inputsParent.transform, false);
        }
    }

    public void setAxis(int id)
    {
        //
    }

    public void setButton(int id)
    {
        //
    }
}
