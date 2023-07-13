using UnityEngine;
using TMPro;

public class ItemGet : MonoBehaviour
{
    public TextMeshProUGUI itemCountText; // アイテムカウントを表示するテキスト

    private int itemCountA = 0; // アイテムAのカウントを保持する変数
    private int itemCountB = 0; // アイテムBのカウントを保持する変数

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したオブジェクトがアイテムAであるかチェック
        
        switch (collision.gameObject.tag)
        {
            case "ItemMuki":
                    CollectItemA(collision.gameObject);
                Debug.Log("aaa");
                break;

            case "ItemOmo":
                CollectItemB(collision.gameObject);
                Debug.Log("bbb");
                break;
        }
        
        
    }

    private void CollectItemA(GameObject item)
    {
        // アイテムAを取得したときの処理を実行
        itemCountA++; // アイテムAカウントを増やす
        UpdateItemCountText();

        // アイテムAを削除
        Destroy(item);
    }

    private void CollectItemB(GameObject item)
    {
        // アイテムBを取得したときの処理を実行
        itemCountB++; // アイテムBカウントを増やす
        UpdateItemCountText();

        // アイテムBを削除
        Destroy(item);
    }

    private void UpdateItemCountText()
    {
        // アイテムカウントをテキストに表示
        itemCountText.text = "ItemMuki: " + itemCountA.ToString() + "  ItemOmo: " + itemCountB.ToString();
    }
}


