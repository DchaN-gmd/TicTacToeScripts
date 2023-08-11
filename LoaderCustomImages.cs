using Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class LoaderCustomImages : MonoBehaviour
{
    [SerializeField] private bool _isLoadOnStart;

    [Header("Reference")]
    [SerializeField] private FinishPanel _winPanel;

    [Header("Paths")]
    [SerializeField] private string _crossPath;
    [SerializeField] private string _circlePath;
    [SerializeField] private string _backgroundPath;

    [Header("Images")]
    [SerializeField] private List<RawImage> _crossImages;
    [SerializeField] private List<RawImage> _circleImages;
    [SerializeField] private RawImage _backgroundImage;

    private Texture2D _crossTexture;
    private Texture2D _circleTexture;
    private Texture2D _backgroundTexture;

    private void Start()
    {
        if (_isLoadOnStart) LoadAndSetTextures();
    }

    public void LoadAndSetTextures()
    {
        _crossTexture = LoadTexture(_crossPath);
        _circleTexture = LoadTexture(_circlePath);
        _backgroundTexture = LoadTexture(_backgroundPath);

        SetTextures();
    }

    private void SetTextures()
    {
        foreach(var image in _crossImages)
        {
            image.texture = _crossTexture;
        }

        foreach(var image in _circleImages)
        {
            image.texture = _circleTexture;
        }

        _backgroundImage.texture = _backgroundTexture;

        _winPanel.SetCustomImages(_crossTexture, _circleTexture);
    }

    private Texture2D LoadTexture(string path)
    {
        var fileName = UnityEngine.Application.streamingAssetsPath + path;
        WWW customImage = new WWW(fileName);

        return customImage.texture;
    }
}
