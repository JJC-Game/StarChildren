using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHitCheck : MonoBehaviour
{
    // アイテムカウントを表示するテキスト
    public TextMeshProUGUI MukiCountText;
    public TextMeshProUGUI OmoCountText;
    // public TextMeshProUGUI BetaCountText;
    // public TextMeshProUGUI PataCountText;

    // アクションシーンで使用するitemのカウント
    private int itemCountMuki;
    private int itemCountOmo;
    // private int itemCountBeta;
    // private int itemCountPata;

    // 育成シーンで使用するitemのカウント
    public int MukiCount;
    public int OmoCount;
    // public int BetaCount;
    // public int PataCount;

    private void Start()
    {
        itemCountMuki = 0;
        itemCountOmo = 0;
        // itemCountBeta = 0;
        // itemCountPata = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したオブジェクトがどのアイテムであるかチェック
        switch (collision.gameObject.tag)
        {
            case "ItemMuki":
                CollectItemMuki(collision.gameObject);
                break;

            case "ItemOmo":
                CollectItemOmo(collision.gameObject);
                break;
                /*
            case "ItemBeta":
                CollectItemBeta(collision.gameObject);
                break;

            case "ItemPata":
                CollectItemPata(collision.gameObject);
                break;
                */
        }

    }

    private void CollectItemMuki(GameObject item)
    {
        // アイテムを取得したときの処理を実行
        itemCountMuki=+1; // アイテムカウントを増やす
        MukiCount = DataManager.Instance.LoadInt("MukiCount") + itemCountMuki;
        DataManager.Instance.SaveInt("MukiCount", MukiCount);
        UpdateItemCountText();

        // アイテムを削除
        Destroy(item);
    }

    private void CollectItemOmo(GameObject item)
    {
        itemCountOmo=+1;
        OmoCount = DataManager.Instance.LoadInt("OmoCount") + itemCountOmo;
        DataManager.Instance.SaveInt("OmoCount", OmoCount);
        UpdateItemCountText();

        Destroy(item);
    }

    /*
    private void CollectItemBeta(GameObject item)
    {
        // アイテムを取得したときの処理を実行
        itemCountBeta=+1; // アイテムカウントを増やす
        BetaCount = DataManager.Instance.LoadInt("BetaCount") + itemCountBeta;
        DataManager.Instance.SaveInt("BetaCount", BetaCount);
        UpdateItemCountText();

        // アイテムを削除
        Destroy(item);
    }

    private void CollectItemPata(GameObject item)
    {
        // アイテムを取得したときの処理を実行
        itemCountPata=+1; // アイテムカウントを増やす
        PataCount = DataManager.Instance.LoadInt("PataCount") + itemCountPata;
        DataManager.Instance.SaveInt("PataCount", PataCount);
        UpdateItemCountText();

        // アイテムを削除
        Destroy(item);
    }
    */

    private void UpdateItemCountText()
    {
        // アイテムカウントをテキストに表示
        MukiCountText.text = itemCountMuki.ToString();
        OmoCountText.text = itemCountOmo.ToString();
        // BetaCountText.text = "ItemBeta: " + itemCountBeta.ToString();
        // PataCountText.text = "  ItemPata: " + itemCountPata.ToString();

    }


    /*private bool isPlayerInside = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーが範囲内に入った場合
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            GameManager.Instance.mainGame = false;
        }
    }
    */
}
