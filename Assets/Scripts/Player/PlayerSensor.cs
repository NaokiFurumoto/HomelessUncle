using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのセンサー
/// </summary>
public class PlayerSensor : MonoBehaviour
{
    private Player player;
    private UIController uiCtrl;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").
                                     GetComponent<Player>();
        uiCtrl = UIController.Instance;
        //検証のため一旦false
        uiCtrl.SetBtnInteractable(NameType.FISH, false);
    }

    /// <summary>
    /// センサーに衝突
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FishArea"))
        {
            //UIの掘るボタンを無効に
            uiCtrl.SetBtnInteractable(NameType.DIG, false);
            //条件が整えば
            uiCtrl.SetBtnInteractable(NameType.FISH, true);
        }
    }

    /// <summary>
    /// センサーに衝突中
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision) { }


    /// <summary>
    /// センサーから抜けた
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FishArea"))
        {
            //UIの掘るボタンを有効に
            uiCtrl?.SetBtnInteractable(NameType.DIG, true);
            //条件が整えば
            uiCtrl.SetBtnInteractable(NameType.FISH, false);
        }
    }
}
