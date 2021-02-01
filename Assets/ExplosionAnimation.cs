using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    public GameObject[] voxelSprites;

    public float explodeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StepAnimation());
    }

    private IEnumerator StepAnimation()
    {
        yield return new WaitForSeconds(explodeSpeed);
        voxelSprites[0].SetActive(false);
        voxelSprites[1].SetActive(true);
        yield return new WaitForSeconds(explodeSpeed);
        voxelSprites[1].SetActive(false);
        voxelSprites[2].SetActive(true);
        yield return new WaitForSeconds(explodeSpeed);
        voxelSprites[2].SetActive(false);
        voxelSprites[3].SetActive(true);
        yield return new WaitForSeconds(explodeSpeed * 2);
        Destroy(this.gameObject);
    }
}
