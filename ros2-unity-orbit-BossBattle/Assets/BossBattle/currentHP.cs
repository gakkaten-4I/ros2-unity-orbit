using UnityEngine;
using UnityEngine.UI;
 
public class GameControlScript : MonoBehaviour {
    GameObject Boss;
    hitpoint move1;
    // UI Text指定用
    public Text HP;
    // 表示する変数
    private int frame;
 
    // Use this for initialization
    void Start () {
        Boss = GameObject.Find("Boss");
        move1 = Boss.GetComponent<hitpoint>();
    }
     
    // Update is called once per frame
    void Update () {
        HP.text = string.Format("BossHP:{}",move1.hp );
        
    }
}