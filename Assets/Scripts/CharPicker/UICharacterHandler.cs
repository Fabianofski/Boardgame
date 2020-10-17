using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterHandler : MonoBehaviour
{

    public GameObject Player;
    public GameObject CustomizationButtons;
    public CharacterContainer charContainer;
    public Button AddButton;
    public Button DelButton;

    public float xoffset;

    public Vector3 StartingPos;
    public int ColoumnCount;
    public float yoffset;

    public void NewPlayer()
    {
        GameObject CB = Instantiate(CustomizationButtons, AddButton.transform.position, Quaternion.identity, transform.parent);

        Vector3 pos = AddButton.transform.position;
        pos.z = 0;
        DelButton.transform.position = pos;
        pos.y -= 0.5f;
        GameObject P = Instantiate(Player, pos, Quaternion.identity);

        CB.GetComponent<Customization>().animator = P.GetComponent<Animator>();

        AddButton.transform.position = new Vector3(AddButton.transform.position.x + xoffset, AddButton.transform.position.y, AddButton.transform.position.z);
        CheckForNewColoumn();
    }

    public void CheckForNewColoumn()
    {
        if (AddButton.transform.position.x == xoffset * ColoumnCount + StartingPos.x)
        {
            if (AddButton.transform.position.y == StartingPos.y - yoffset)
                AddButton.GetComponent<Image>().enabled = false;

            AddButton.transform.position = new Vector3(StartingPos.x, StartingPos.y - yoffset, StartingPos.z);
        }
    }

    public void DeletePlayer()
    {
        if (charContainer.Chars.Length < 3) return;

        AddButton.transform.position = charContainer.Chars[0].transform.position;
        DelButton.transform.position = charContainer.Chars[1].transform.position;
        AddButton.GetComponent<Image>().enabled = true;

        Destroy(charContainer.Chars[0].animator.gameObject);
        Destroy(charContainer.Chars[0].gameObject);
    }

}
