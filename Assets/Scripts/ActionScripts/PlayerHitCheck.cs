using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHitCheck : MonoBehaviour
{
    // アイテムカウントを表示するテキスト
    public TextMeshProUGUI MukiCountText;
    public TextMeshProUGUI OmoCountText;
    public TextMeshProUGUI BetaCountText;
    public TextMeshProUGUI PataCountText;
    
    // アクションシーンで使用するitemのカウント
    private int itemCountMuki;
    private int itemCountOmo;
    private int itemCountBeta;
    private int itemCountPata;

    // 育成シーンで使用するitemのカウント
    public int MukiCount;
    public int OmoCount;
    public int BetaCount;
    public int PataCount;

    public Transform player;

    private void Start()
    {
        itemCountMuki = 0;
        itemCountOmo = 0;
        itemCountBeta = 0;
        itemCountPata = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したオブジェクトがどのアイテムであるかチェック
        switch (collision.gameObject.tag)
        {
            case "ItemMuki":
                CollectItemMuki(collision.gameObject);
                EffectManager.Instance.PlayEffect(0, player);
                break;

            case "ItemOmo":
                CollectItemOmo(collision.gameObject);
                break;
                
            case "ItemBeta":
                CollectItemBeta(collision.gameObject);
                break;
                
            case "ItemPata":
                CollectItemPata(collision.gameObject);
                break;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Mukib":
                Mukibreak(collision.gameObject);
                break;

            case "Omob":
                Omobreak(collision.gameObject);
                break;

        }
    }

    private void CollectItemMuki(GameObject item)
    {
        // アイテムを取得したときの処理を実行
        itemCountMuki++; // アイテムカウントを増やす
        MukiCount = DataManager.Instance.LoadInt("MukiCount") +1;
        DataManager.Instance.SaveInt("MukiCount", MukiCount);
        UpdateItemCountText();
        
        // アイテムを削除
        Destroy(item);
    }

    private void CollectItemOmo(GameObject item)
    {
        itemCountOmo++;
        OmoCount = DataManager.Instance.LoadInt("OmoCount") + 1;
        DataManager.Instance.SaveInt("OmoCount", OmoCount);
        UpdateItemCountText();

        Destroy(item);
    }

    
    private void CollectItemBeta(GameObject item)
    {
        itemCountBeta++;
        BetaCount = DataManager.Instance.LoadInt("BetaCount") + 1;
        DataManager.Instance.SaveInt("BetaCount", BetaCount);
        UpdateItemCountText();

        Destroy(item);
    }
    
    private void CollectItemPata(GameObject item)
    {
        itemCountPata++;
        PataCount = DataManager.Instance.LoadInt("PataCount") + 1;
        DataManager.Instance.SaveInt("PataCount", PataCount);
        UpdateItemCountText();

        Destroy(item);
    }

    private void Mukibreak(GameObject floor)
    {
        if(DataManager.Instance.LoadBool("Muki"))
        {
            Destroy(floor);
        }

    }

    private void Omobreak(GameObject floor)
    {
        if (DataManager.Instance.LoadBool("Omo"))
        {
            Destroy(floor);
        }
    }


    private void UpdateItemCountText()
    {
        // アイテムカウントをテキストに表示
        MukiCountText.text = itemCountMuki.ToString();
        OmoCountText.text = itemCountOmo.ToString();
        BetaCountText.text = itemCountBeta.ToString();
        PataCountText.text = itemCountPata.ToString();

    }

}
