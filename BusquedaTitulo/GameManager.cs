using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public const int BOARD_SIZE = 21;
    public const int TOTAL_SEMESTERS = 10;

    [Header("Game State")]
    public float initialEnergyTime = 100f;
    public float energyTimer;
    public int maxEnergy = 4;
    public int currentEnergy;
    public int maxLives = 5;
    public int currentLives;
    public int objectsFound = 0;
    public int score = 0;
    public int currentSemester = 1;
    public bool isPaused = false;
    private Vector2Int playerCurrentPosition;

    private int stepsSinceBonus = 0;
    private Vector2Int currentLevelStartPosition;

    public int shieldInventory = 0;
    public int chargerInventory = 0;
    private bool isShieldActive = false;
    private float shieldTimer = 0f;

    [Header("Boss Level")]
    public int bossTrapCount = 5;
    private List<Vector2Int> activeBossTraps = new List<Vector2Int>();
    private Vector2Int titlePosition;

    public enum TileType { Wall, Floor, PlayerStart, Trap, DamageTrap, Title, Pista, Life, Charger, Shield, FinalBoss }

    private TileType[,] board = new TileType[BOARD_SIZE, BOARD_SIZE];
    private GameObject[,] tileObjects = new GameObject[BOARD_SIZE, BOARD_SIZE];
    private bool[,] isExplored = new bool[BOARD_SIZE, BOARD_SIZE];
    private List<string[]> levelMaps = new List<string[]>();

    [Header("Visual Assets")]
    public GameObject tilePrefab;

    public Sprite trapSprite;
    public Sprite damageTrapSprite;
    public Sprite titleSprite;
    public Sprite pistaSprite;
    public Sprite lifeSprite;
    public Sprite chargerSprite;
    public Sprite shieldSprite;
    public Sprite blackHermanSprite;

    public Color wallColor = new Color(0.3f, 0.3f, 0.3f);
    public Color floorColor = new Color(0.8f, 0.8f, 0.8f);
    public Color hiddenColor = Color.black;
    public Color titleColor = Color.yellow;
    public Color lifeColor = Color.magenta;
    public Color chargerColor = Color.cyan;
    public Color trapColor = Color.red;
    public Color pistaColor = Color.green;
    public Color damageTrapColor = new Color(1f, 0.5f, 0f);
    public Color shieldColor = Color.blue;
    public Color blackHermanColor = Color.magenta;

    [Header("Referencias de UI")]
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI semesterText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pistaText;
    public TextMeshProUGUI inventoryText;

    public JugadorController jugadorController;

    [Header("UI Victoria Boss")]
    public GameObject bossVictoryPanel;
    public TextMeshProUGUI bossDialogueText;
    public TextMeshProUGUI titleEarnedText;
    public Animator bossAnimator;

    private Sprite baseTileSprite = null;
    private int totalPistasEnNivel = 0;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); return; }
        if (tilePrefab != null && tilePrefab.GetComponent<SpriteRenderer>() != null)
        {
            baseTileSprite = tilePrefab.GetComponent<SpriteRenderer>().sprite;
            if (baseTileSprite == null) Debug.LogWarning("El tilePrefab no tiene Sprite base.");
        }
        else { Debug.LogError("¡¡¡tilePrefab no está asignado o no tiene SpriteRenderer!!!"); }
        PopulateLevelData();
    }

    void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (bossVictoryPanel != null) bossVictoryPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        currentLives = 3;
        score = 0;
        shieldInventory = 0;
        chargerInventory = 0;

        LoadLevel(currentSemester - 1);
    }

    void Update()
    {
        if (IsGamePaused()) { return; }
        if (jugadorController != null && !jugadorController.isMoving)
        {
            energyTimer -= Time.deltaTime;
            if (energyTimer <= 0)
            {
                energyTimer = 0;
                UpdateUI();
                Debug.Log("¡Reprobaste! Se acabó el tiempo.");
                HandleFailure();
                return;
            }
            if (isShieldActive)
            {
                shieldTimer -= Time.deltaTime;
                if (shieldTimer <= 0)
                {
                    shieldTimer = 0;
                    isShieldActive = false;
                    Debug.Log("¡Escudo desactivado!");
                }
            }
            UpdateUI();
        }
    }

    void PopulateLevelData()
    {
        levelMaps.Clear();
        string[] level1 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WFFFFFFFFFWFFFFFFFFFW", "WFWWWWWWWFWFWWWWWWWFW", "WFWFFFFFFFWFWFFFFFFFW", "WFWFWWWWWFWFWWWWWFWFW",
            "WFWFWCRFWFWFWTFFWFWFW", "WFWWWFWFWWWWWFWFWFWFW", "WFFFFFFTFFFFFFFFLFWFW", "WFWWWFWFWWWWWWWWWFWFW", "WFWFFFFFWFFFFFFFFFFFW",
            "WFWFWWWWWFWPWWWWWWWWW", "WFWFWFFFFFFFWFFFFFFFW", "WFWFWWWWWWWFWFWWWWWFW", "WFWFFFFFFFFFWFWFFFFFF", "WFWFWWWWWWWFWFWFWWWFW",
            "WFWFWFFFFFFFWFWFWCTFW", "WFWFWFWWWWWFWFWWWWWFW", "WFWFWFWFFFFFFFWFFFFFF", "WFWFWFWFWWWWWWWWWWWFW", "WFLFFRFFFFFFFFFFFFSFW",
            "WWWWWWWWWWWWWWWWWWWWW"
        };
        levelMaps.Add(level1);
        string[] level2 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WPFFFFFFFFFFFFFFFFSFW", "WFWWWWWWWWWWWWWWWWWFW", "WFFFFFFFFFFFFFFFFTFFW", "WFWWWWWWWWWWWWWWWWWFW",
            "WFTFFFFFFCFFFFFFFFTFW", "WFWWWWWWWWWWWWWWWWWFW", "WFFFFFFFFFFFFFFFFTFFW", "WFWWWWWWWWWWWWWWWWWFW", "WFTFFFFFFFFFFFFFTFFFW",
            "WFWWWWWWWWWWWWWWWWWFW", "WFFFFFFFFFFFFFFFFTFFW", "WFWWWWWWWWWWWWWWWWWFW", "WFTFFFFFFFFFFFFFTFFFW", "WFWWWWWWWWWWWWWWWWWFW",
            "WFFFFFFFFFFFFFFFFTFFW", "WFWWWWWWWWWWWWWWWWWFW", "WFTFFFFFLRFFFFTFFFFFW", "WFWWWWWWWWWWWWWWWWWFW", "WFFFFFFFFFFFFFFFFFFFW",
            "WWWWWWWWWWWWWWWWWWWWW"
        };
        levelMaps.Add(level2);
        string[] level3 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WFFFFFFFFFWFFFFFFFFFW", "WFWWWWWFWFWFWWWWWFWFW", "WFPFTFFFWFWFFFTFFFWFW", "WFWWWWWFWFWFWWWWWFWFW",
            "WFFFFFFFFFWFFFFFFFFFW", "WFWWWWWFWWWWWFWWWWWFW", "WFWFFFFFWFFFFFWFFFFFW", "WFWFWWWFWFWWWWFWWWFWW", "WFWFWFFFWFFFFFFFWFFFW",
            "WFWFWFWWWFWFWWWFWFWFW", "WFWFFFWFFFWFWFFFWFWFW", "WFWWWWWFWFWFWWWWWFWFW", "WFFFFFFFFFWFFFFFFFFFW", "WWWWFWWWWWWWWWFWWWWWWW",
            "WCRFFFWFFFFFFFWFFTFFW", "WFWWWFWFWWWWWFWFWWWFW", "WFWFFFWFWFFFFFWFWFFFW", "WFWWWWWWWWWWWWWWWWWFW", "WFFFFFFFFFLFFFFFFSFFW",
            "WWWWWWWWWWWWWWWWWWWWW"
        };
        levelMaps.Add(level3);
        string[] level4 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WFFFFFFFFFFFWWWWWFFFW", "WFWWWWWWWWWFWFFSFFFFW", "WFWFFFFFFFFFWFWWWWWFW", "WFWFWWWWWWWFWFWFFFFFW",
            "WFWFWFFFFTFFWFWWWWWFW", "WFWFWWWWWWWFWTFFFFTFW", "WFWFFFFCFFFWWWWWWWWFW", "WFWWWWWWWFWFFFFFFFFFW", "WFFFFTFFFFWFWWWWWWWFW",
            "WFWWWWWWWFWFWFFFFFFFW", "WFWFFFFFFFFFWWWWWWWFW", "WFWWWWWWWWWFWFFFFFFFW", "WFTFFFFFFFFFWWWWWWWFW", "WFWWWWWWWWWFWFFFFFFFW",
            "WFWFFFFFFFFFWWWWWWWFW", "WFWWWWWWWWWFWFFFFFFFW", "WFLFFFFFFFFFWWWWWWWFW", "WFWWWWWWWWWFFFFFFFFFW", "WPFFFFFFFFFFFFFFFFTFW",
            "WWWWWWWWWWWWWWWWWWWWW"
        };
        levelMaps.Add(level4);
        string[] level5 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WPFFFFFFFFFFFFFFFFFFW", "WFWWWWWWWWWWWWWWWWWFW", "WFWFTFTFTFTFTFTFTFWFW", "WFWFWWWWWWWWWWWWWFWFW",
            "WFWFWFFFFFDFFFFFFFWFW", "WFWFWFWWWWWWWWWWWFWFW", "WFWFWFWFFFFFFFFFFFWFW", "WFWFWFWFWWWWWWWWWFWFW", "WFWFWFWFWFFFFFFFFFWFW",
            "WFWFWFWFWFWWWWWWWFWFW", "WFWFWFWFWFWFFFFCFFWFW", "WFWFWFWFWFWFWWWWWFWFW", "WFWFWFWFWFWFWFFFFFWFW", "WFWFWFWFWFWFWFWWWFWFW",
            "WFWFWFWFWFWFWFWFFFWFW", "WFWFWFWFWFWFWFWFWFWFW", "WLTFTFTFTFTFTFTFTFTFW", "WFWWWWWWWWWWWWWWWWWFW", "WFFFFFFFFFFFFSFFFFFFW",
            "WWWWWWWWWWWWWWWWWWWWW"
        };
        levelMaps.Add(level5);
        string[] level6 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WPFFFFFFFFFFFFFFFFTFW", "WWWWWWWWWWWWWWWWWFFWW", "WFFFFFFFFFFFFFFFFFWFW", "WFWWWWWWWWWWWWWWWFWFW",
            "WFWFFFFFFFFFFFFFFFWFW", "WFWWWWWWWWWWWWWWWFWFW", "WFWFFFFFFFFTFFFFFFWFW", "WFWWWWWWWWWWWWFWWWWWW", "WFWFFFFFFFFFWFWFFFFFW",
            "WFWWWWWWWWWFWFWWWWWFW", "WFWFFFFFFFWFWFFFFTFFW", "WFWWWWWWWFWFWWWWWWWFW", "WFLFFFFFFFWCFFFFFDFFW", "WFWWWWWWWWWWWWWWWWWFW",
            "WFWTFFFFFFFFFFFFFTFFW", "WFWWWWWWWWWWWWWWWFFFW", "WFWFFFFFFFFFFFFFFFFFW", "WFWWWWWWWWWWWWWWWWWFW", "WFFFFFFFFFFFFFFFFFSFW",
            "WWWWWWWWWWWWWWWWWWWWW"
        };
        levelMaps.Add(level6);
        string[] level7 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WPFTRTFTFWFTFWFTFWFTW", "WFWFWFWFWFWFWFWFWFWFW", "WFTFWFTFWFTFWFTFWFTFW", "WFWFWFWFWFWFWFWFWFWFW",
            "WFTFWFTDWFTFWFTFWFTFW", "WFWFWFWFWFWFWFWFWFWFW", "WFTFWFTFWFTFWFTFWFTFW", "WFWFWFWFWFWFWFWFWFWFW", "WFTFWFTFWFTFWFTFWFTFW",
            "WFWFWFWFWFWFWFWFWFWFW", "WFTFWFTFWFTFWFTFWFTFW", "WFWFWFWFWFWFWFWFWFWFW", "WFTFWFTFWFTFWFTFWFTFW", "WFWFWFWFWFWFWFWFWFWFW",
            "WFTFWFTFWFTFWFTFWFTFW", "WFWFWFWFWFWFWFWFWFWFW", "WFTFWFTFWFTFWFTFWFTFW", "WFWFWFWFWFWFWFWFWFWFW", "WLFFFFFFFFFFFFFFFFFCF",
            "WWWWWWWWWWWWWWWWWWS"
        };
        levelMaps.Add(level7);
        string[] level8 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WPFWFFFFFFFFFWFFFFSFW", "WFWWWWWWWWWWWFWFWWWWWFW", "WFWFFTFWFFFFWFWFFTFFW", "WFWWWWWFWWWWWWWWWWWFW",
            "WFWFFFFFWFWFFFFFFFFFW", "WFWFWWWFWFWFWWWWWWWFW", "WFTFWFFFFFWFWFFCFFFFW", "WFWWWWWWWWWWWWWWWWWFW", "WFWFFFFTFFFFFFFFFTFFW",
            "WFWFWWWWWWWWWWWFWWWFW", "WFWFWFFFFFFFFFWFWFFFW", "WFWFWFWWWWWWWFWFWWWFW", "WFWFWFWFFTFFFFWFFFFFD", "WFWWWWWWWWWWWWWWWWWFW",
            "WFWFFFFFFFFFFFFTFFFFW", "WFWWWWWFWWWWWWWWWWWFW", "WFWFFFFFWFFFFFFFFFFFW", "WFWWWWWWWWWWWWWWWWWFW", "WLFFFFFFFFFFFFFFFFFFW",
            "WWWWWWWWWWWWWWWWWWWWW"
        };
        levelMaps.Add(level8);
        string[] level9 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WFFFFFFFFFFFFFFFWWWWW", "WFWWWWWWWWWWWWWFWWWWW", "WFWFFFFFFFFFFFFFWWWWW", "WFWFWWWWWWWWWWWWWWWWW",
            "WFWFWFFFFCFFFFFFFFFFW", "WFWWWWWFWWWWWWWWWWWFW", "WFWFFTFWFTFTFTFTFTTFW", "WFWWWWWWWWWWWWWWWWWFW", "WFWFFFFFFFFFFFFFWFFFW",
            "WFWFWWWWWWWWWWWFWFWWW", "WFWFWFFFFFFFFFWFWFWWW", "WFWFWWWWWWWWWFWFWFWWW", "WFWFWFFFFFFTFWFWFWWWW", "WFWWWWWWWWWWWWWFWWWWW",
            "WFWFFFFFFFFFFFFFWFFFW", "WFWWWWWWWWWWWWWWWWWFW", "WPFTFTFTFTFTFTFTDTTFW", "WFWWWWWWWWWWWWWWWWWFW", "WLFFFFFFFFFFFFFFFFSFW",
            "WWWWWWWWWWWWWWWWWWWWW"
        };
        levelMaps.Add(level9);
        string[] level10 = new string[] {
            "WWWWWWWWWWWWWWWWWWWWW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW",
            "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW",
            "WFFFFFFFFFBFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW",
            "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WFFFFFFFFFFFFFFFFFFFFFW", "WPFFFFFFFFFFFFFFFFFFFLW",
            "WWWWWWWWWWWWWWWWWWWWW"
        };
        levelMaps.Add(level10);
    }


    void LoadLevel(int levelIndex)
    {
        Debug.Log($"Cargando Nivel {levelIndex + 1} (Índice {levelIndex})");
        if (levelIndex < 0 || levelIndex >= levelMaps.Count) { Debug.LogError("..."); return; }

        if (MusicManager.Instance != null)
        {
            if (levelIndex == 9)
            {
                MusicManager.Instance.PlayBossMusic();
            }
            else
            {
                MusicManager.Instance.PlayNormalMusic();
            }
        }
        else { Debug.LogWarning("MusicManager.Instance no encontrado. No se puede cambiar la música."); }

        ClearAllBossTraps();
        energyTimer = initialEnergyTime;
        currentEnergy = maxEnergy;
        stepsSinceBonus = 0;
        objectsFound = 0;
        isShieldActive = false;
        shieldTimer = 0f;

        string[] mapData = levelMaps[levelIndex];
        Vector2Int startPos = Vector2Int.zero;

        List<Vector2Int> pistaPositions = new List<Vector2Int>();
        List<Vector2Int> shieldPositions = new List<Vector2Int>();

        totalPistasEnNivel = 0;

        if (mapData == null || mapData.Length != BOARD_SIZE)
        {
            Debug.LogError($"Error en mapa {levelIndex + 1}: ¡mapData es null o no tiene {BOARD_SIZE} filas!");
            return;
        }

        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                int mapY = (BOARD_SIZE - 1) - y;
                char tileChar = 'F';
                if (mapY >= 0 && mapY < mapData.Length && mapData[mapY] != null && mapData[mapY].Length > x) { tileChar = mapData[mapY][x]; }
                else { tileChar = 'W'; board[x, y] = TileType.Wall; continue; }

                switch (tileChar)
                {
                    case 'W': board[x, y] = TileType.Wall; break;
                    case 'T': board[x, y] = TileType.Trap; break;
                    case 'D': board[x, y] = TileType.DamageTrap; break;
                    case 'S': board[x, y] = TileType.Title; titlePosition = new Vector2Int(x, y); break;
                    case 'B': board[x, y] = TileType.FinalBoss; titlePosition = new Vector2Int(x, y); break;
                    case 'R':
                        board[x, y] = TileType.Pista;
                        pistaPositions.Add(new Vector2Int(x, y));
                        totalPistasEnNivel++;
                        break;
                    case 'L': board[x, y] = TileType.Life; break;
                    case 'C': board[x, y] = TileType.Charger; break;
                    case 'E': board[x, y] = TileType.Shield; shieldPositions.Add(new Vector2Int(x, y)); break;
                    case 'P':
                        board[x, y] = TileType.PlayerStart;
                        startPos = new Vector2Int(x, y);
                        currentLevelStartPosition = startPos;
                        break;
                    case 'F': default: board[x, y] = TileType.Floor; break;
                }
                isExplored[x, y] = true;
            }
        }

        if (levelIndex < 9)
        {
            ForceObjectCount(pistaPositions, TileType.Pista, 3);
            totalPistasEnNivel = 3;
            ForceObjectCount(shieldPositions, TileType.Shield, 2);
        }

        DrawBoard();

        playerCurrentPosition = startPos;
        if (jugadorController != null)
        {
            jugadorController.SetInitialPosition(startPos);
        }
        else { Debug.LogError("¡Referencia a jugadorController es NULL en LoadLevel!"); }

        UpdateUI();
    }

    void ForceObjectCount(List<Vector2Int> positions, TileType type, int desiredCount)
    {
        int currentCount = positions.Count;
        if (currentCount > desiredCount)
        {
            int toRemove = currentCount - desiredCount;
            for (int i = 0; i < toRemove; i++)
            {
                int randIdx = Random.Range(0, positions.Count);
                Vector2Int pos = positions[randIdx];
                board[pos.x, pos.y] = TileType.Floor;
                positions.RemoveAt(randIdx);
            }
        }
        else if (currentCount < desiredCount)
        {
            int toAdd = desiredCount - currentCount;
            int attempts = 0;
            while (toAdd > 0 && attempts < 100)
            {
                attempts++;
                Vector2Int randPos = GetRandomFloorPosition();
                if (board[randPos.x, randPos.y] == TileType.Floor)
                {
                    board[randPos.x, randPos.y] = type;
                    toAdd--;
                }
            }
        }
    }

    Vector2Int GetRandomFloorPosition()
    {
        int x = 0, y = 0;
        int attempts = 0;
        do
        {
            x = Random.Range(1, BOARD_SIZE - 1);
            y = Random.Range(1, BOARD_SIZE - 1);
            attempts++;
            if (attempts > 100) { Debug.LogWarning("..."); return new Vector2Int(BOARD_SIZE / 2, BOARD_SIZE / 2); }
        } while (x < 0 || x >= BOARD_SIZE || y < 0 || y >= BOARD_SIZE || board[x, y] != TileType.Floor);
        return new Vector2Int(x, y);
    }

    void UpdateBossTraps()
    {
        ClearAllBossTraps();
        int placedTraps = 0;
        int attempts = 0;
        while (placedTraps < bossTrapCount && attempts < 200)
        {
            attempts++;
            Vector2Int randomPos = GetRandomFloorPosition();
            if (randomPos.x >= 0 && randomPos.x < BOARD_SIZE && randomPos.y >= 0 && randomPos.y < BOARD_SIZE &&
                board[randomPos.x, randomPos.y] == TileType.Floor &&
                randomPos != playerCurrentPosition &&
                randomPos != titlePosition)
            {
                board[randomPos.x, randomPos.y] = TileType.Trap;
                UpdateTileAppearance(randomPos);
                activeBossTraps.Add(randomPos);
                placedTraps++;
            }
        }
    }

    void ClearAllBossTraps()
    {
        List<Vector2Int> trapsToClear = new List<Vector2Int>(activeBossTraps);
        activeBossTraps.Clear();
        foreach (Vector2Int trapPos in trapsToClear)
        {
            if (trapPos.x >= 0 && trapPos.x < BOARD_SIZE && trapPos.y >= 0 && trapPos.y < BOARD_SIZE && board[trapPos.x, trapPos.y] == TileType.Trap)
            {
                board[trapPos.x, trapPos.y] = TileType.Floor;
                UpdateTileAppearance(trapPos);
            }
        }
    }

    void UpdateTileAppearance(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x >= BOARD_SIZE || pos.y < 0 || pos.y >= BOARD_SIZE) return;
        if (tileObjects == null || pos.x >= tileObjects.GetLength(0) || pos.y >= tileObjects.GetLength(1) || tileObjects[pos.x, pos.y] == null) return;

        GameObject tileObject = tileObjects[pos.x, pos.y];
        SpriteRenderer sr = tileObject.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        TileType type = board[pos.x, pos.y];
        Sprite objectSprite = GetTileDisplaySprite(type);

        if (objectSprite != null)
        {
            sr.sprite = objectSprite;
            sr.color = Color.white;
        }
        else
        {
            sr.sprite = baseTileSprite;
            switch (type)
            {
                case TileType.Wall: sr.color = wallColor; break;
                case TileType.Floor:
                case TileType.PlayerStart:
                default: sr.color = floorColor; break;
            }
        }
    }


    void DrawBoard()
    {
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount - 1; i >= 0; i--) { Destroy(transform.GetChild(i).gameObject); }
        }
        tileObjects = new GameObject[BOARD_SIZE, BOARD_SIZE];

        if (tilePrefab == null) { Debug.LogError("..."); return; }
        if (tilePrefab.GetComponent<SpriteRenderer>() == null) { Debug.LogError("..."); }

        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                if (tile == null) { continue; }

                tile.transform.SetParent(transform);
                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                if (sr == null) { sr = tile.AddComponent<SpriteRenderer>(); }

                tileObjects[x, y] = tile;
                UpdateTileAppearance(new Vector2Int(x, y));
            }
        }
    }

    Sprite GetTileDisplaySprite(TileType type)
    {
        switch (type)
        {
            case TileType.Trap: return trapSprite;
            case TileType.DamageTrap: return damageTrapSprite;
            case TileType.Title: return titleSprite;
            case TileType.Pista: return pistaSprite;
            case TileType.Life: return lifeSprite;
            case TileType.Charger: return chargerSprite;
            case TileType.Shield: return shieldSprite;
            case TileType.FinalBoss: return blackHermanSprite;
            case TileType.Wall:
            case TileType.PlayerStart:
            case TileType.Floor:
            default:
                return null;
        }
    }

    Color GetTileDisplayColor(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x >= BOARD_SIZE || pos.y < 0 || pos.y >= BOARD_SIZE) return hiddenColor;

        TileType type = board[pos.x, pos.y];

        if (GetTileDisplaySprite(type) != null)
        {
            return Color.white;
        }

        switch (type)
        {
            case TileType.Wall:
                return wallColor;
            case TileType.PlayerStart:
            case TileType.Floor:
            case TileType.Trap: return trapColor;
            case TileType.DamageTrap: return damageTrapColor;
            case TileType.Title: return titleColor;
            case TileType.Pista: return pistaColor;
            case TileType.Life: return lifeColor;
            case TileType.Charger: return chargerColor;
            case TileType.Shield: return shieldColor;
            case TileType.FinalBoss: return blackHermanColor;
            default:
                return floorColor;
        }
    }


    public void UpdateFogOfWar(Vector2Int centerPos) { playerCurrentPosition = centerPos; }

    public void UpdateUI()
    {
        string shieldText = "";
        if (isShieldActive)
        {
            shieldText = $" | ESCUDO: {Mathf.CeilToInt(shieldTimer)}";
        }

        if (energyText != null)
        {
            energyText.text = $"ENERGÍA: {currentEnergy}/{maxEnergy} | TIEMPO: {Mathf.CeilToInt(energyTimer)}{shieldText}";
        }
        if (livesText != null) livesText.text = $"VIDAS: {currentLives}";
        if (semesterText != null) semesterText.text = (currentSemester == 10) ? "JEFE FINAL: BLACK HERMAN" : $"SEMESTRE: {currentSemester}";
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {score}";
        }
        if (pistaText != null)
        {
            pistaText.text = $"PISTAS: {objectsFound}/{totalPistasEnNivel}";
        }
        if (inventoryText != null)
        {
            inventoryText.text = $"ESCUDOS (1): {shieldInventory} | CARGADORES (2): {chargerInventory}";
        }
    }

    public TileType GetTileType(Vector2Int position)
    {
        if (position.x >= 0 && position.x < BOARD_SIZE && position.y >= 0 && position.y < BOARD_SIZE)
        {
            return board[position.x, position.y];
        }
        return TileType.Wall;
    }
    public void UpdatePlayerPosition(Vector2Int newPosition) { playerCurrentPosition = newPosition; }

    public void HandleTileEffect(TileType type, Vector2Int position)
    {
        TileType originalType = board[position.x, position.y];
        bool changedType = false;
        bool isBossLevel = (currentSemester == 10);
        bool shouldUpdateBossTraps = isBossLevel && type != TileType.Title && type != TileType.FinalBoss;

        switch (type)
        {
            case TileType.Title:
            case TileType.FinalBoss:
                board[position.x, position.y] = TileType.Floor; changedType = true;
                stepsSinceBonus = 0;
                LoadNextLevel(); return;

            case TileType.Pista:
                Debug.Log("¡Pista encontrada! +10 segundos y +10 score.");
                objectsFound++;
                energyTimer += 10f;
                score += 10;
                stepsSinceBonus = 0;
                board[position.x, position.y] = TileType.Floor;
                changedType = true;
                break;

            case TileType.Life:
                if (currentLives < maxLives) { currentLives++; }
                stepsSinceBonus = 0;
                board[position.x, position.y] = TileType.Floor;
                changedType = true;
                break;

            case TileType.Charger:
                Debug.Log("Cargador recogido, añadido al inventario.");
                chargerInventory++;
                stepsSinceBonus = 0;
                board[position.x, position.y] = TileType.Floor;
                changedType = true;
                break;

            case TileType.Shield:
                Debug.Log("¡Escudo recogido, añadido al inventario!");
                shieldInventory++;
                stepsSinceBonus = 0;
                board[position.x, position.y] = TileType.Floor;
                changedType = true;
                break;

            case TileType.Trap:
                stepsSinceBonus = 0;
                if (isShieldActive)
                {
                    Debug.Log("¡Escudo bloqueó una trampa de energía!");
                    board[position.x, position.y] = TileType.Floor;
                    changedType = true;
                }
                else if (currentSemester == 10)
                {
                    Debug.Log("¡Trampa del jefe! Regresando al inicio.");
                    if (jugadorController != null) { jugadorController.SetInitialPosition(currentLevelStartPosition); }
                    shouldUpdateBossTraps = true;
                }
                else
                {
                    Debug.Log("Trampa de Energía pisada, -1 energía.");
                    LoseEnergy(1);
                    board[position.x, position.y] = TileType.Floor;
                    changedType = true;
                    if (currentLives <= 0) return;
                }
                break;

            case TileType.DamageTrap:
                stepsSinceBonus = 0;
                if (isShieldActive)
                {
                    Debug.Log("¡Escudo bloqueó una trampa de daño!");
                    board[position.x, position.y] = TileType.Floor;
                    changedType = true;
                }
                else
                {
                    Debug.Log("¡Trampa de Daño! -1 Vida.");
                    LoseLife(1);
                    board[position.x, position.y] = TileType.Floor;
                    changedType = true;
                    if (currentLives <= 0) return;
                }
                break;

            case TileType.Floor:
            case TileType.PlayerStart:
                stepsSinceBonus++;
                if (stepsSinceBonus >= 3)
                {
                    Debug.Log("¡3 pasos! +1 energía.");
                    AddEnergy(1);
                    stepsSinceBonus = 0;
                }
                if (isBossLevel) { shouldUpdateBossTraps = true; }
                break;
        }

        if (shouldUpdateBossTraps) { UpdateBossTraps(); }
        if (changedType) { UpdateTileAppearance(position); }
        UpdateUI();
    }

    public void LoseEnergy(int amount)
    {
        currentEnergy -= amount;
        Debug.Log($"Energía perdida, actual: {currentEnergy}/{maxEnergy}");
        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            UpdateUI();
            Debug.Log("¡Sin energía 4/4! Pierdes una vida.");
            HandleFailure();
        }
        else { UpdateUI(); }
    }

    public void AddEnergy(int amount)
    {
        if (currentEnergy >= maxEnergy) return;
        currentEnergy += amount;
        if (currentEnergy > maxEnergy) { currentEnergy = maxEnergy; }
        Debug.Log($"Energía ganada, actual: {currentEnergy}/{maxEnergy}");
        UpdateUI();
    }

    public void LoseLife(int amount)
    {
        currentLives -= amount;
        Debug.Log($"Vida perdida, actual: {currentLives}");
        UpdateUI();

        if (currentLives <= 0)
        {
            Debug.Log("¡Sin vidas! Game Over.");
            GameOver();
        }
    }

    public void HandleFailure()
    {
        currentLives--;
        UpdateUI();
        if (currentLives <= 0)
        {
            Debug.Log("HandleFailure -> GameOver");
            GameOver();
        }
        else
        {
            Debug.Log("HandleFailure -> RestartCurrentLevel");
            RestartCurrentLevel();
        }
    }

    public void GameOver()
    {
        Debug.Log("🛑 JUEGO TERMINADO.");
        isPaused = true;
        Time.timeScale = 0f;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("¡GameOverPanel no asignado! Reiniciando...");
            RestartGame();
        }
    }

    public void RestartCurrentLevel()
    {
        Debug.Log($"Perdiste vida. Reiniciando Semestre {currentSemester}. Vidas: {currentLives}");
        if (currentSemester == 10) { ClearAllBossTraps(); }
        isShieldActive = false;
        shieldTimer = 0f;
        LoadLevel(currentSemester - 1);
    }

    public void RestartGame()
    {
        Debug.Log("Reiniciando el juego desde el principio...");
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        Time.timeScale = 1f;
        isPaused = false;

        score = 0;
        currentLives = 3;
        currentSemester = 1;
        shieldInventory = 0;
        chargerInventory = 0;

        ClearAllBossTraps();
        LoadLevel(0);
    }

    public void LoadNextLevel()
    {
        if (currentSemester >= TOTAL_SEMESTERS)
        {
            Debug.Log("🏆 ¡VICTORIA FINAL!"); PauseGame(); ShowBossVictoryScreen(); return;
        }
        currentSemester++; Debug.Log($"Iniciando Semestre {currentSemester}..."); LoadLevel(currentSemester - 1);
    }

    public void UseShield()
    {
        if (isShieldActive)
        {
            Debug.Log("¡El escudo ya está activo!");
            return;
        }
        if (shieldInventory > 0)
        {
            shieldInventory--;
            isShieldActive = true;
            shieldTimer = 10f;
            Debug.Log("¡Escudo activado desde el inventario!");
            UpdateUI();
        }
        else
        {
            Debug.Log("¡No tienes escudos en el inventario!");
        }
    }

    public void UseCharger()
    {
        if (chargerInventory > 0)
        {
            chargerInventory--;
            AddEnergy(maxEnergy);
            Debug.Log("¡Cargador usado desde el inventario!");
            UpdateUI();
        }
        else
        {
            Debug.Log("¡No tienes cargadores en el inventario!");
        }
    }

    void ShowBossVictoryScreen()
    {
        if (bossVictoryPanel != null)
        {
            bossVictoryPanel.SetActive(true);
            if (bossDialogueText != null) bossDialogueText.text = "¡Maldición, estudiante! Lo lograste...";
            if (titleEarnedText != null) titleEarnedText.text = "¡HAS OBTENIDO EL TÍTULO...";
            if (bossAnimator != null) { }
        }
        else { Debug.LogError("bossVictoryPanel no asignado."); }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
    }

    public void PauseGame()
    {
        if (bossVictoryPanel != null && bossVictoryPanel.activeSelf) return;
        if (gameOverPanel != null && gameOverPanel.activeSelf) return;
        isPaused = true;
        Time.timeScale = 0f;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
    }

    public void TogglePause()
    {
        if (bossVictoryPanel != null && bossVictoryPanel.activeSelf) return;
        if (gameOverPanel != null && gameOverPanel.activeSelf) return;

        if (isPaused) { ResumeGame(); }
        else { PauseGame(); }
    }

    public bool IsGamePaused()
    {
        return isPaused || (bossVictoryPanel != null && bossVictoryPanel.activeSelf) || (gameOverPanel != null && gameOverPanel.activeSelf);
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Volviendo al menú principal / Reiniciando juego.");
        Time.timeScale = 1f;
        if (bossVictoryPanel != null) bossVictoryPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        ClearAllBossTraps();
        isPaused = false;

        currentLives = maxLives;
        currentSemester = 1;
        score = 0;
        shieldInventory = 0;
        chargerInventory = 0;

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}