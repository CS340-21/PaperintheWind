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

        // Hide the pre-designed section objects from the world
        foreach (Section s in Sections)
            s.gameObject.SetActive(false);
    }

    /// <summary>
    /// Choose a random section and attach it to the end of the most recently created section.
    /// </summary>
    public Section GenerateNewSection()
    {
        // Copy one of the pre-designed sections in this level
        GameObject copy = GameObject.Instantiate(Utils.GetRandom(Sections).gameObject);
        copy.gameObject.SetActive(true);

        Section newSection = copy.GetComponent<Section>();
        newSection.ID = UnityEngine.Random.Range(1, 1000000);

        // If this isn't the first section, align it with the previous one
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
    /// Destroy a section's gameobject and remove it from the active sections list
    /// </summary>
    public void DestroySection(Section section)
    {
        // Remove the section from the active list
        int idx = ActiveSections.FindIndex(s => s.ID == section.ID);
        if (idx >= 0)
            ActiveSections.RemoveAt(idx);

        Destroy(section.gameObject);
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
            if (s != null) Destroy(s.gameObject);

        ActiveSections = new List<Section>();

        PlayerController.Revive();
        this.BeginLevel();
    }

}
