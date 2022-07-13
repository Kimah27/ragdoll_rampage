using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterSelect : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    private LevelManagement levelManagement;
    private bool confirmed;

    private int selectedCharacterIndex;
    private Color desiredColor;

    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image backgroundColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip confirmSFX;
    [SerializeField] private AudioClip characterSelectMusic;

    [Header("Tweaks")]
    [SerializeField] private float backgroundColorTransitionSpeed = 0.1f;

    private void Awake()
    {
        playerInputManager = GameObject.Find("PlayerManager").GetComponent<PlayerInputManager>();
        levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
    }

    private void Start()
    {
        FadeManager.Instance.FadeIn(1f, Color.white);

        UpdateCharacterSelectionUI();
        AudioManager.Instance.PlayMusic(characterSelectMusic);
    }

    private void Update()
    {
        backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
    }

    public void LeftArrow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selectedCharacterIndex -= 1;
            if (selectedCharacterIndex < 0)
            {
                selectedCharacterIndex = characterList.Count - 1;
            }

            UpdateCharacterSelectionUI();
            AudioManager.Instance.PlaySFX(arrowClickSFX, 0.5f);
        }
    }

    public void RightArrow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selectedCharacterIndex += 1;
            if (selectedCharacterIndex == characterList.Count)
            {
                selectedCharacterIndex = 0;
            }

            UpdateCharacterSelectionUI();
            AudioManager.Instance.PlaySFX(arrowClickSFX, 0.5f);

        }
    }

    public void Confirm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!confirmed)
            {
                levelManagement.P1Selection = selectedCharacterIndex;
                levelManagement.p1characterSelected = true;
                Debug.Log("P1 character: " + characterList[selectedCharacterIndex].characterName + "(" + selectedCharacterIndex + ")");
                AudioManager.Instance.PlaySFX(confirmSFX, 0.5f);
                FadeManager.Instance.FadeOut(1f, Color.black, LoadScene);
                confirmed = true;
                playerInputManager.EnableJoining();
            }
        }
    }

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FrozenSuburbs");
    }

    private void UpdateCharacterSelectionUI()
    {
        characterSplash.sprite = characterList[selectedCharacterIndex].splash;
        characterName.text = characterList[selectedCharacterIndex].characterName;
        desiredColor = characterList[selectedCharacterIndex].characterColor;
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Color characterColor;
    }
}
