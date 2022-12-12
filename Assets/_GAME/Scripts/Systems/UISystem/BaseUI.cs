using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private RectTransform panel;

    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Ease animationEase = Ease.OutBack;
    [SerializeField] private RectTransform[] revealAnimatedObjects;

    public void SetShown()
    {
        panel.gameObject.SetActive(true);

        Animate();
    }

    public void SetHidden()
    {
        panel.gameObject.SetActive(false);
    }

    private void Animate()
    {
        for (int i = 0; i < revealAnimatedObjects.Length; i++)
        {
            RectTransform revealAnimatedObject = revealAnimatedObjects[i];
            int animationIndex = Random.Range(0, 5);

            if (animationIndex == 4)
            {
                revealAnimatedObject.DOScale(Vector3.zero, animationDuration).From().SetEase(animationEase);
            }
            else
            {
                Vector3 pos = revealAnimatedObject.localPosition;
                int screenSize;
                float sizeDelta;
                int multiplier = 1;
                float axisValue;

                if (animationIndex % 2 == 0)
                {
                    multiplier = -1;
                }

                if (animationIndex > 1)
                {
                    screenSize = Screen.width;
                    sizeDelta = revealAnimatedObject.sizeDelta.x / 2f;

                    axisValue = screenSize + sizeDelta;
                    axisValue *= multiplier;

                    pos.x = axisValue;
                }
                else
                {
                    screenSize = Screen.height;
                    sizeDelta = revealAnimatedObject.sizeDelta.y / 2f;

                    axisValue = screenSize + sizeDelta;
                    axisValue *= multiplier;

                    pos.y = axisValue;
                }

                revealAnimatedObject.DOLocalMove(pos, animationDuration).From().SetEase(animationEase);
            }
        }
    }
}
