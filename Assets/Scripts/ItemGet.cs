using UnityEngine;

public class ItemGet : MonoBehaviour
{
    private int itemCount = 0; // アイテムのカウントを保持する変数

    private void OnTriggerEnter2D(Collider2D colision)
    {
        // 衝突したオブジェクトがアイテムAであるかチェック
        int number = 1;
        switch (number)
        {
            case 1:
                colision.CompareTag("ItemMuki");
                    CollectItemA(colision.gameObject);
                break;

            case 2:
                colision.CompareTag("ItemOmo");
                CollectItemA(colision.gameObject);
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


