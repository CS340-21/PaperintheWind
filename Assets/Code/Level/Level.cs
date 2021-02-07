using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public Section[] Sections;

    public List<Section> ActiveSections = new List<Section>();

    private Section LastCreatedSection { get { if (ActiveSections.Count > 0) return ActiveSections[ActiveSections.Count - 1]; else return null; } }

    private PlayerController PlayerController { get { return PlayerManager.Instance.Controller; } }

    /// <summary>
    /// Choose a random section and attach it to the end of the most recently created section.
    /// </summary>
    public Section GenerateNewSection()
    {
        GameObject copy = GameObject.Instantiate(Utils.GetRandom(Sections).gameObject);
        Section newSection = copy.GetComponent<Section>();
        newSection.ID = Random.Range(1, 1000000);

        if (LastCreatedSection != null)
            newSection.AlignWithSection(LastCreatedSection);
        else
            copy.transform.position = Constants.WorldStart;

        ActiveSections.Add(newSection);
        return newSection;
    }

    public Section GenerateMultipleSections(int num)
    {
        Section firstSection = null;
        for (int i = 0; i < num; i++)
        {
            GenerateNewSection();
            if (i == 0) firstSection = LastCreatedSection;
        }
        return firstSection;
    }

    public void BeginLevel()
    {
        Section firstSection = this.GenerateMultipleSections(Constants.TotalSpawnedSections);
        PlayerController.CurrentSection = firstSection;

        PlayerController.Teleport(firstSection.Spawn.transform);
    }

    public void RestartLevel()
    {
        foreach (Section s in ActiveSections)
        {
            if (s != null) Destroy(s.gameObject);
        }

        ActiveSections = new List<Section>();

        PlayerController.Revive();
        BeginLevel();
    }

}
