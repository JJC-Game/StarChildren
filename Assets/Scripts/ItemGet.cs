using UnityEngine;

public class ItemGet : MonoBehaviour
{
    private int itemCount = 0; // アイテムのカウントを保持する変数

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
                CollectItemA(collision.gameObject);
                Debug.Log("bbb");
                break;
        }
        
        
    }

    private void CollectItemA(GameObject item)
    {
        // アイテムを取得したときの処理を実行
        itemCount++; // アイテムカウントを増やす
        Debug.Log("アイテムAを取得しました。現在のアイテムカウント: " + itemCount);

        // アイテムAを削除
        Destroy(item);
    }
}


