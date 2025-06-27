using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{

    [SerializeField] private float _dissolve = 0f;
    [SerializeField] private float _speed = 1f;

    private MeshRenderer _meshRenderer;
    private Material _material;
    private MeshCollider _meshCollider;

    private static readonly int Dissolve = Shader.PropertyToID("_DissolveAmount");

    private bool _isDissolving = false;
    private bool _isRestoring = false;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        _material.SetFloat(Dissolve, _dissolve);
        _meshCollider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isDissolving = true;
            _isRestoring = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isRestoring = true;
            _isDissolving = false;
        }

        if (_isDissolving)
        {
            _dissolve += Time.deltaTime * _speed;
            _dissolve = Mathf.Clamp01(_dissolve);
            _material.SetFloat(Dissolve, _dissolve);

            if (_dissolve >= 1f)
                _isDissolving = false;

            if (_meshCollider != null)
            {
                _meshCollider.enabled = false;
            }
        }

        if (_isRestoring)
        {
            _dissolve -= Time.deltaTime * _speed;
            _dissolve = Mathf.Clamp01(_dissolve);
            _material.SetFloat(Dissolve, _dissolve);

            if (_dissolve <= 0f)
                _isRestoring = false;

            if (_meshCollider != null)
            {
                _meshCollider.enabled = true;
            }
        }
    }
}