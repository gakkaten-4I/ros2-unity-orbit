using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Winner_Right, Winner_Left;

    // Start is called before the first frame update
    void Start()
    {
        Winner_Right.enabled = false;
        Winner_Left.enabled = false;
        StartCoroutine(ResultAnimation());
    }
    private IEnumerator ResultAnimation()
    {
        // ‘Ò‹@
        yield return new WaitForSeconds(3f);

        // “¾“_•\Ž¦
        yield return StartCoroutine(FlashWinner());
    }
    private IEnumerator FlashWinner()
    {
        for(int i = 0; i < 6; i++)
        {
            Winner_Right.enabled = !Winner_Right.enabled;
            Winner_Left.enabled = !Winner_Left.enabled;
            yield return new WaitForSeconds(0.5f);
        }
        yield break;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
