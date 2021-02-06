using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public Section[] Sections;

    public List<Section> ActiveSections = new List<Section>();

    private Section lastCreatedSection;

    public Section GenerateNewSection()
    {
        GameObject copy = GameObject.Instantiate(Utils.GetRandom(Sections).gameObject);
        Section newSection = copy.GetComponent<Section>();

        if (lastCreatedSection != null)
        {
            if (lastCreatedSection.EndRotation != newSection.BeginRotation)
            {
                int diff = newSection.EndRotation - newSection.BeginRotation;
                int rotate = lastCreatedSection.EndRotation - newSection.BeginRotation;
                copy.transform.Rotate(0f, lastCreatedSection.EndRotation, 0f);
                newSection.BeginRotation = lastCreatedSection.EndRotation;
                newSection.EndRotation = newSection.BeginRotation + diff;
            }

            newSection.AlignWithSection(lastCreatedSection);
        }
        else
        {
            copy.transform.position = new Vector3(100, 0, 100);
        }

        return newSection;
    }

    public Section GenerateMultipleSections(int num)
    {
        Section firstSection = null;
        for (int i = 0; i < num; i++)
        {
            lastCreatedSection = GenerateNewSection();
            if (i == 0) firstSection = lastCreatedSection;
        }
        return firstSection;
    }

    public void BeginLevel()
    {
        Section firstSection = this.GenerateMultipleSections(3);
        PlayerManager.Instance.Controller.CurrentSection = firstSection;

        PlayerManager.Instance.Controller.Teleport(firstSection.Spawn.transform);
    }

}
