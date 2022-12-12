using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class ArenaSection
{
    public float ParachuteSpawnMaxRange;
    public List<GameObject> sectionParts;
    public List<MeshRenderer> partMeshs;

}
public class ArenaController : BaseManager
{
    [SerializeField] ArenaSection currentArenaSection;
    [SerializeField] public List<ArenaSection> arenaSections;
    [SerializeField] float shrinkCooldown;
    [SerializeField] float shrinkRate;
    bool cooldownWarning;
    [SerializeField] Material shrinkWarningMaterial;
    [SerializeField] Material arenaDefaulMaterial;
    bool isCompleteShrink;

    void Start()
    {

        shrinkCooldown = shrinkRate;
        FillMeshList();
        currentArenaSection = arenaSections[0];
    }
    public float CurrentMaxRange()
    {
        if (isCompleteShrink)
        {
            return 1;
        }
        else
        {
            return currentArenaSection.ParachuteSpawnMaxRange;
        }

    }
    IEnumerator WarningForShrink()
    {
        cooldownWarning = true;
        int warnCount = 10;
        float waitSeconds = 0.7f;
        while (warnCount >= 2)
        {
            if (warnCount % 2 == 0)
            {
                ChangeMaterialArenaParts(shrinkWarningMaterial);
            }
            else
            {
                ChangeMaterialArenaParts(arenaDefaulMaterial);
            }

            yield return new WaitForSeconds(waitSeconds);
            waitSeconds -= 0.1f;
            warnCount--;
        }
    }
    void ChangeMaterialArenaParts(Material material)
    {
        int currentArenaSectionIndex = arenaSections.IndexOf(currentArenaSection);
        for (int i = 0; i < arenaSections[currentArenaSectionIndex].partMeshs.Count; i++)
        {
            arenaSections[currentArenaSectionIndex].partMeshs[i].sharedMaterial = material;
        }
    }
    void FillMeshList()
    {
        for (int i = 0; i < arenaSections.Count; i++)
        {
            for (int k = 0; k < arenaSections[i].sectionParts.Count; k++)
            {
                arenaSections[i].partMeshs.Add(arenaSections[i].sectionParts[k].GetComponent<MeshRenderer>());
            }
        }
    }
    void Update()
    {
        if (isCompleteShrink) return;
        shrinkCooldown -= Time.deltaTime;
        if (shrinkCooldown < 5 && !cooldownWarning)
        {
            StartCoroutine(WarningForShrink());
        }
        if (shrinkCooldown <= 0)
        {
            shrinkCooldown = shrinkRate;
            ShrinkTheArea();
        }
    }
    void ShrinkTheArea()
    {
        int currentArenaSectionIndex = arenaSections.IndexOf(currentArenaSection);//comment
        if (currentArenaSectionIndex <= arenaSections.Count - 1)//comment
        {

            for (int i = 0; i < arenaSections[currentArenaSectionIndex].sectionParts.Count; i++)
            {
                float partYPos = arenaSections[currentArenaSectionIndex].sectionParts[i].transform.position.y;
                arenaSections[currentArenaSectionIndex].sectionParts[i].transform.DOMoveY(partYPos - 10, 3).SetEase(Ease.InOutBounce).SetDelay(UnityEngine.Random.RandomRange(1, 3));
            }
            cooldownWarning = false;
            if (currentArenaSectionIndex == arenaSections.Count - 1)
            {
                isCompleteShrink = true;
            }
            else
            {
                currentArenaSection = arenaSections[currentArenaSectionIndex + 1];
            }
        }

    }
}
