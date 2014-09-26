using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveConfirm : MonoBehaviour
{
    private string clientName;
    private string culpritName;

    public void MoveThatPanelOut()
    {
        clientName = GameObject.Find("ClientNameText").GetComponent<Text>().text;
        GameObject.Find("ClientInteractionPanel").GetComponent<Animation>().Play();
        var culprit = GameObject.Find("CulpInteractionPanel");
        culprit.SetActive(true);
        culprit.GetComponent<Animation>().Play();
    }

    public void CloseMenu()
    {
        culpritName = GameObject.Find("CulpritNameText").GetComponent<Text>().text;
        GameObject.Find("NamePanel").SetActive(false);
        GameObject.Find("ClientName").GetComponent<Text>().text = clientName;
        GameObject.Find("CulpritName").GetComponent<Text>().text = culpritName;
    }
}
