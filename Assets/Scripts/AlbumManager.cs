using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumManager : MonoBehaviour
{
    public Sprite[] character;
    public GameObject[] ClearChara;

    public SpriteRenderer[] targetsprite;
    public int n;


    private void Start()
    {
        CheckClearChara();

        if (DataManager.Instance.LoadInt("Clear") == 1)
        {
            n = 0;
            targetsprite[n] = ClearChara[0].GetComponent<SpriteRenderer>();
            CheckClearChara();

            if (DataManager.Instance.LoadInt("Clear") == 2)
            {
                n = 1;
                targetsprite[n] = ClearChara[1].GetComponent<SpriteRenderer>();
                CheckClearChara();

                if (DataManager.Instance.LoadInt("Clear") == 3)
                {
                    n = 2;
                    targetsprite[n] = ClearChara[2].GetComponent<SpriteRenderer>();
                    CheckClearChara();

                    if (DataManager.Instance.LoadInt("Clear") == 4)
                    {
                        n = 3;
                        targetsprite[n] = ClearChara[3].GetComponent<SpriteRenderer>();
                        CheckClearChara();

                        if (DataManager.Instance.LoadInt("Clear") == 5)
                        {
                            n = 4;
                            targetsprite[n] = ClearChara[4].GetComponent<SpriteRenderer>();
                            CheckClearChara();

                            if (DataManager.Instance.LoadInt("Clear") == 6)
                            {
                                n = 5;
                                targetsprite[n] = ClearChara[5].GetComponent<SpriteRenderer>();
                                CheckClearChara();
                            }
                        }
                    }
                }

            }
        }

    }

    private void Update()
    {

    }

    public void CheckClearChara()
    {
        if (DataManager.Instance.LoadBool("E3FFF"))
        {
            targetsprite[n].sprite = character[0];
            DataManager.Instance.SaveBool("E3FFF", false);

        }
        if (DataManager.Instance.LoadBool("E3OOO"))
        {
            targetsprite[n].sprite = character[1];
            DataManager.Instance.SaveBool("E3OOO", false);

        }
        if (DataManager.Instance.LoadBool("E3FFO"))
        {
            targetsprite[n].sprite = character[2];
            DataManager.Instance.SaveBool("E3FFO", false);

        }
        if (DataManager.Instance.LoadBool("E3OOF"))
        {
            targetsprite[n].sprite = character[3];
            DataManager.Instance.SaveBool("E3OOF", false);

        }

    }

}
