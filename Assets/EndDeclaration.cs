using UnityEngine;
using TMPro;
using System.Collections;

public class EndDeclaration : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro1;   // �����ɕ\������e�L�X�g
    public TextMeshProUGUI textMeshPro2;   // �E���ɕ\������e�L�X�g
    public float duration = 1f;            // �t�F�[�h�C���ƃt�F�[�h�A�E�g�ɂ����鎞��
    public float waitDuration = 1f;        // �t�F�[�h�C����̑ҋ@����
    public float startOffsetY = 200f;      // �t�F�[�h�C���J�n�ʒu��Y�I�t�Z�b�g

    private Vector3 startPosition1;
    private Vector3 endPosition1;
    private Vector3 startPosition2;
    private Vector3 endPosition2;
    private CanvasGroup canvasGroup1;
    private CanvasGroup canvasGroup2;

    void Start()
    {
        // �����̃e�L�X�g�ݒ�
        RectTransform rectTransform1 = textMeshPro1.GetComponent<RectTransform>();
        startPosition1 = rectTransform1.anchoredPosition + new Vector2(0, -startOffsetY);
        endPosition1 = new Vector2(-175, rectTransform1.anchoredPosition.y);
        rectTransform1.anchoredPosition = startPosition1;
        rectTransform1.localRotation = Quaternion.Euler(0, 0, 270);

        // CanvasGroup�̎擾�Ə����ݒ�
        canvasGroup1 = textMeshPro1.GetComponent<CanvasGroup>();
        if (canvasGroup1 == null)
            canvasGroup1 = textMeshPro1.gameObject.AddComponent<CanvasGroup>();
        canvasGroup1.alpha = 0f;

        // �E���̃e�L�X�g�ݒ�
        RectTransform rectTransform2 = textMeshPro2.GetComponent<RectTransform>();
        startPosition2 = rectTransform2.anchoredPosition + new Vector2(0, startOffsetY);
        endPosition2 = new Vector2(175, rectTransform2.anchoredPosition.y);
        rectTransform2.anchoredPosition = startPosition2;
        rectTransform2.localRotation = Quaternion.Euler(0, 0, 90);

        // CanvasGroup�̎擾�Ə����ݒ�
        canvasGroup2 = textMeshPro2.GetComponent<CanvasGroup>();
        if (canvasGroup2 == null)
            canvasGroup2 = textMeshPro2.gameObject.AddComponent<CanvasGroup>();
        canvasGroup2.alpha = 0f;

        // �t�F�[�h�C���E�ҋ@�E�t�F�[�h�A�E�g�̃V�[�P���X�J�n
        StartCoroutine(FadeInOutSequence());
    }

    private IEnumerator FadeInOutSequence()
    {
        //���ʉ�
        GetComponent<AudioSource>().Play();
        // �t�F�[�h�C��
        yield return StartCoroutine(FadeIn());

        // �ҋ@
        yield return new WaitForSeconds(waitDuration);

        // �t�F�[�h�A�E�g
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // �����̃e�L�X�g�̈ʒu�Ɠ����x���X�V
            RectTransform rectTransform1 = textMeshPro1.GetComponent<RectTransform>();
            rectTransform1.anchoredPosition = Vector3.Lerp(startPosition1, endPosition1, progress);
            canvasGroup1.alpha = Mathf.Lerp(0, 1, progress);

            // �E���̃e�L�X�g�̈ʒu�Ɠ����x���X�V
            RectTransform rectTransform2 = textMeshPro2.GetComponent<RectTransform>();
            rectTransform2.anchoredPosition = Vector3.Lerp(startPosition2, endPosition2, progress);
            canvasGroup2.alpha = Mathf.Lerp(0, 1, progress);

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;

        RectTransform rectTransform1 = textMeshPro1.GetComponent<RectTransform>();
        RectTransform rectTransform2 = textMeshPro2.GetComponent<RectTransform>();
        startPosition1.y = endPosition1.y - startPosition1.y;
        startPosition2.y = endPosition2.y - startPosition2.y;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // �����̃e�L�X�g�̈ʒu�Ɠ����x���t�����ɍX�V
            rectTransform1.anchoredPosition = Vector3.Lerp(endPosition1, startPosition1, progress);
            canvasGroup1.alpha = Mathf.Lerp(1, 0, progress);

            // �E���̃e�L�X�g�̈ʒu�Ɠ����x���t�����ɍX�V
            rectTransform2.anchoredPosition = Vector3.Lerp(endPosition2, startPosition2, progress);
            canvasGroup2.alpha = Mathf.Lerp(1, 0, progress);

            yield return null;
        }
    }
}
