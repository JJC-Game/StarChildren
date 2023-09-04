using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour
{
    public Sprite[] charactersprite;
    public GameObject[]　DisplayChara;
    public TextMeshProUGUI[] CharaName;

    private void Start()
    {
        if (DataManager.Instance.LoadInt("Clear") == 1 && DataManager.Instance.LoadBool("Album"))
        {
            if (DataManager.Instance.LoadBool("E3FFF"))
            {
                DataManager.Instance.SaveInt("Display0", 0);
                DataManager.Instance.SaveString("CName0", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOO"))
            {
                DataManager.Instance.SaveInt("Display0", 1);
                DataManager.Instance.SaveString("CName0", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3FFO"))
            {
                DataManager.Instance.SaveInt("Display0", 2);
                DataManager.Instance.SaveString("CName0", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOF"))
            {
                DataManager.Instance.SaveInt("Display0", 3);
                DataManager.Instance.SaveString("CName0", DataManager.Instance.LoadString("Name"));
            }
            DataManager.Instance.SaveBool("Album",false);
        }
        else if (DataManager.Instance.LoadInt("Clear") == 2 && DataManager.Instance.LoadBool("Album"))
        {
            if (DataManager.Instance.LoadBool("E3FFF"))
            {
                DataManager.Instance.SaveInt("Display1", 0);
                DataManager.Instance.SaveString("CName1", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOO"))
            {
                DataManager.Instance.SaveInt("Display1", 1);
                DataManager.Instance.SaveString("CName1", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3FFO"))
            {
                DataManager.Instance.SaveInt("Display1", 2);
                DataManager.Instance.SaveString("CName1", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOF"))
            {
                DataManager.Instance.SaveInt("Display1", 3);
                DataManager.Instance.SaveString("CName1", DataManager.Instance.LoadString("Name"));
            }
            DataManager.Instance.SaveBool("Album", false);
        }
        else if (DataManager.Instance.LoadInt("Clear") == 3 && DataManager.Instance.LoadBool("Album"))
        {
            if (DataManager.Instance.LoadBool("E3FFF"))
            {
                DataManager.Instance.SaveInt("Display2", 0);
                DataManager.Instance.SaveString("CName2", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOO"))
            {
                DataManager.Instance.SaveInt("Display2", 1);
                DataManager.Instance.SaveString("CName2", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3FFO"))
            {
                DataManager.Instance.SaveInt("Display2", 2);
                DataManager.Instance.SaveString("CName2", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOF"))
            {
                DataManager.Instance.SaveInt("Display2", 3);
                DataManager.Instance.SaveString("CName2", DataManager.Instance.LoadString("Name"));
            }
            DataManager.Instance.SaveBool("Album", false);
        }
        else if (DataManager.Instance.LoadInt("Clear") == 4 && DataManager.Instance.LoadBool("Album"))
        {
            if (DataManager.Instance.LoadBool("E3FFF"))
            {
                DataManager.Instance.SaveInt("Display3", 0);
                DataManager.Instance.SaveString("CName3", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOO"))
            {
                DataManager.Instance.SaveInt("Display3", 1);
                DataManager.Instance.SaveString("CName3", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3FFO"))
            {
                DataManager.Instance.SaveInt("Display3", 2);
                DataManager.Instance.SaveString("CName3", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOF"))
            {
                DataManager.Instance.SaveInt("Display3", 3);
                DataManager.Instance.SaveString("CName3", DataManager.Instance.LoadString("Name"));
            }
            DataManager.Instance.SaveBool("Album", false);
        }
        else if (DataManager.Instance.LoadInt("Clear") == 5 && DataManager.Instance.LoadBool("Album"))
        {
            if (DataManager.Instance.LoadBool("E3FFF"))
            {
                DataManager.Instance.SaveInt("Display4", 0);
                DataManager.Instance.SaveString("CName4", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOO"))
            {
                DataManager.Instance.SaveInt("Display4", 1);
                DataManager.Instance.SaveString("CName4", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3FFO"))
            {
                DataManager.Instance.SaveInt("Display4", 2);
                DataManager.Instance.SaveString("CName4", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOF"))
            {
                DataManager.Instance.SaveInt("Display4", 3);
                DataManager.Instance.SaveString("CName4", DataManager.Instance.LoadString("Name"));
            }
            DataManager.Instance.SaveBool("Album", false);
        }
        else if (DataManager.Instance.LoadInt("Clear") == 6 && DataManager.Instance.LoadBool("Album"))
        {
            if (DataManager.Instance.LoadBool("E3FFF"))
            {
                DataManager.Instance.SaveInt("Display5", 0);
                DataManager.Instance.SaveString("CName5", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOO"))
            {
                DataManager.Instance.SaveInt("Display5", 1);
                DataManager.Instance.SaveString("CName5", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3FFO"))
            {
                DataManager.Instance.SaveInt("Display5", 2);
                DataManager.Instance.SaveString("CName5", DataManager.Instance.LoadString("Name"));
            }
            else if (DataManager.Instance.LoadBool("E3OOF"))
            {
                DataManager.Instance.SaveInt("Display5", 3);
                DataManager.Instance.SaveString("CName5", DataManager.Instance.LoadString("Name"));
            }
            DataManager.Instance.SaveBool("Album", false);
        }

        DisplayCharacter0();
        DisplayCharacter1();
        DisplayCharacter2();
        DisplayCharacter3();
        DisplayCharacter4();
        DisplayCharacter5();

    }

    private void Update()
    {

    }

    public void DisplayCharacter0()
    {
        Image targetsimage = DisplayChara[0].GetComponent<Image>();
        targetsimage.sprite = charactersprite[DataManager.Instance.LoadInt("Display0")];
        CharaName[0].text = DataManager.Instance.LoadString("CName0");
    }

    public void DisplayCharacter1()
    {
        Image targetimage = DisplayChara[1].GetComponent<Image>();
        targetimage.sprite = charactersprite[DataManager.Instance.LoadInt("Display1")];
        CharaName[1].text = DataManager.Instance.LoadString("CName1");
    }

    public void DisplayCharacter2()
    {
        Image targetimage = DisplayChara[2].GetComponent<Image>();
        targetimage.sprite = charactersprite[DataManager.Instance.LoadInt("Display2")];
        CharaName[2].text = DataManager.Instance.LoadString("CName2");
    }

    public void DisplayCharacter3()
    {
        Image targetimage = DisplayChara[3].GetComponent<Image>();
        targetimage.sprite = charactersprite[DataManager.Instance.LoadInt("Display3")];
        CharaName[3].text = DataManager.Instance.LoadString("CName3");
    }

    public void DisplayCharacter4()
    {
        Image targetimage = DisplayChara[4].GetComponent<Image>();
        targetimage.sprite = charactersprite[DataManager.Instance.LoadInt("Display4")];
        CharaName[4].text = DataManager.Instance.LoadString("CName4");
    }

    public void DisplayCharacter5()
    {
        Image targetimage = DisplayChara[5].GetComponent<Image>();
        targetimage.sprite = charactersprite[DataManager.Instance.LoadInt("Display5")];
        CharaName[5].text = DataManager.Instance.LoadString("CName5");
    }

}
