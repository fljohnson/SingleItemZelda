using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    GameObject[] voxelSprites;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StepAnimation());
    }

    private IEnumerator StepAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        voxelSprites[0].SetActive(false);
        voxelSprites[1].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        voxelSprites[1].SetActive(false);
        voxelSprites[2].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        voxelSprites[2].SetActive(false);
        voxelSprites[3].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
