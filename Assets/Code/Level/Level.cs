using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public Section[] Sections;

    public List<Section> ActiveSections = new List<Section>();

    private Section LastCreatedSection { get { if (ActiveSections.Count > 0) return ActiveSections[ActiveSections.Count - 1]; else return null; } }

    private PlayerController PlayerController { get { return PlayerManager.Instance.Controller; } }

    private void Awake()
    {
        if (Sections.Length == 0)
            Utils.Crash(transform.name + " has no sections attached to script");

        foreach (Section s in Sections)
            s.gameObject.SetActive(false);
    }

    /// <summary>
    /// Choose a random section and attach it to the end of the most recently created section.
    /// </summary>
    public Section GenerateNewSection()
    {
        GameObject copy = GameObject.Instantiate(Utils.GetRandom(Sections).gameObject);
        copy.gameObject.SetActive(true);

        Section newSection = copy.GetComponent<Section>();
        newSection.ID = UnityEngine.Random.Range(1, 1000000);

        if (LastCreatedSection != null)
            newSection.AlignWithSection(LastCreatedSection);
        else
            copy.transform.position = Vector3.zero;

        ActiveSections.Add(newSection);
        return newSection;
    }

    /// <summary>
    /// Generate the given number of sections
    /// </summary>
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

    /// <summary>
    /// Begin this level by generating sections and moving the player to the spawn
    /// </summary>
    public void BeginLevel()
    {
        Section firstSection = this.GenerateMultipleSections(Constants.TotalSpawnedSections);
        PlayerController.CurrentSection = firstSection;

        PlayerController.Teleport(firstSection.Spawn.transform.position);
    }

    /// <summary>
    /// Destroy old sections and generate new ones
    /// </summary>
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
