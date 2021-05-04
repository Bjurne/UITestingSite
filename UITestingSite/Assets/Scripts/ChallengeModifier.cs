using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeModifier : MonoBehaviour
{
    [SerializeField] RectTransform diceFacesContainer;
    [SerializeField] TextMeshProUGUI fieldChallengeEvents;
    [SerializeField] ChallengeModifierData data;

    private List<Image> diceFaceImages;

    private void Start()
    {
        SetUp(data);
    }

    private void SetUp(ChallengeModifierData newData)
    {
        data = newData;
        diceFaceImages = new List<Image>(diceFacesContainer.GetComponentsInChildren<Image>());

        for (int i = 0; i < data.diceFaces.Count; i++)
        {
            var item = diceFaceImages[i];
            if (item != null)
            {
                item.sprite = data.diceFaces[i].resultSprite;
            }
        }

        fieldChallengeEvents.text = data.challengeEvents;
    }
}
