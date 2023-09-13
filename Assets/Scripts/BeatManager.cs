using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BeatManager : MonoBehaviour
{
    float songPosition;
    float songPosInBeats;
    float secPerBeat;
    float dspTimeSong;

    [SerializeField] float bpm;
    [SerializeField] InputInTime[] inputs;
    float[] notes;
    int[] notesButton;
    //public NoteInfo[,] notes;
    int nextIndex = 0;
    int nextUIIndex = 0;
    int nextObstacleIndex = 0;
    public InputUI inputIconPrefab;
    [SerializeField] private float inputIconSpawnTime;
    
    //public Queue<GameObject> noteList = new Queue<GameObject>();
    
    public Material glowMaterial;
    public Material normalMaterial;
    [SerializeField] private Sprite jumpIcon;
    [SerializeField] private Sprite dashIcon;
    [SerializeField] private Sprite slideIcon;
    [SerializeField] private Sprite attackIcon;

    public PlayerController player;

    public RectTransform noteSpawnLeft;
    public RectTransform noteSpawnRight;

    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] public PostProcessManager postProcessManager;

    [SerializeField] GameObject noteTab;

    public float Bpm => bpm;

    public static BeatManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    private void OnEnable()
    {
        GameManager.instance.OnGameStarted += GameStart;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        secPerBeat = 60f / bpm;
        
        
    }

    public void GameStart()
    {
        GetComponent<AudioSource>()?.Play();
        dspTimeSong = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.IsStarted) return;

        songPosition = (float)(AudioSettings.dspTime - dspTimeSong); // position in seconds

        songPosInBeats = songPosition / secPerBeat; // position in beats
        Debug.Log((int)songPosInBeats);

        // VER COMO SPAWNEAR O HACER NIVEL
        if (nextObstacleIndex < inputs.Length && inputs[nextObstacleIndex].beatPlayed < songPosInBeats + (bpm / 60f) * 4f) // Spawnear enemigos 4 segundos antes segun la nota
        {
            levelGenerator.CreateObstacle(inputs[nextObstacleIndex].type, inputs[nextObstacleIndex].beatPlayed, inputs[nextObstacleIndex].height, 3f);
            //enemySpawner.SpawnEnemies(inputs[nextObstacleIndex].type);

            nextObstacleIndex++;
        }

        if (nextUIIndex < inputs.Length && inputs[nextUIIndex].beatPlayed < songPosInBeats + (bpm / 60f) * inputIconSpawnTime) // Spawnear nota en la UI 1.5 segundos antes segun la nota
        {
            SpawnInputUI(inputs[nextUIIndex].type);

            nextUIIndex++;
        }

    }
    
    public void SpawnInputUI(InputType type)
    {
        // TAL VEZ HACER POOL DE PREFABS UI
        Debug.Log("spawn?");
        InputUI inputIcon = Instantiate(inputIconPrefab, inputIconPrefab.transform.position, Quaternion.identity, noteTab.transform);
        inputIcon.GetComponent<RectTransform>().anchoredPosition = noteSpawnLeft.anchoredPosition;
        inputIcon.Init(inputIconSpawnTime);
        //inputIcon.transform.SetParent(noteTab.transform);
        
        switch (type)
        {
            case InputType.Jump:
                inputIcon.GetComponent<Image>().sprite = jumpIcon;
                break;
            case InputType.Dash:
                inputIcon.GetComponent<Image>().sprite = dashIcon;
                break;
            case InputType.Slide:
                inputIcon.GetComponent<Image>().sprite = slideIcon;
                break;
            case InputType.Attack:
                inputIcon.GetComponent<Image>().sprite = slideIcon;
                break;
        }
    }

    public void NoteHit()
    {

    }

    public void NoteMissed()
    {

    }

    public void SpeedUp()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Time.timeScale = 5f;
        }
        else Time.timeScale = 1f;
    }
}
