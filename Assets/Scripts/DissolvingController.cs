using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class DissolvingController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public VisualEffect vfxGraph;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    private Material[] _skinnedMaterials;

    private void Start()
    {
        if (skinnedMeshRenderer != null)
            _skinnedMaterials = skinnedMeshRenderer.materials;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(DissolveCo());
    }

    private IEnumerator DissolveCo()
    {
        if (vfxGraph != null)
            vfxGraph.Play();

        if (_skinnedMaterials.Length > 0)
        {
            var counter = 0f;
            while (_skinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                foreach (var material in _skinnedMaterials)
                    material.SetFloat("_DissolveAmount", counter);

                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}