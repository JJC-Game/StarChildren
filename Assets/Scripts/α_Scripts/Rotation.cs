using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 pos;
    public Vector3 rot;


    public int movePattern;

    public int rotatePattern;

    float timeCount;
    int direction = 1;

    void Start()
    {

    }

    void Update()
    {
        switch (movePattern)
        {
            case 0:
                //何もしない
                break;

            case 1:
                transform.Translate(pos * Time.deltaTime * direction);
                timeCount += Time.deltaTime;

                if (timeCount > 1)
                {
                    timeCount = 0;
                    direction = -1;
                }

                break;

            default:
                //上記以外のケースの時　今回は何もしない
                break;
        }




        /*switch (rotatePattern)
        {
            case 0:
                何もしない
                break;

            case 1:
                transform.Rotate(rot Time.deltaTime);
                break;

            default:
                上記以外のケースの時　今回は何もしない
               break;

        }*/

    }
}