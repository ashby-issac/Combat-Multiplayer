using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private PhotonView photonView;

    public static UIManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void UpdateText(float value)
    {
        if (photonView.IsMine)
            text.text = value.ToString();
    }
}
