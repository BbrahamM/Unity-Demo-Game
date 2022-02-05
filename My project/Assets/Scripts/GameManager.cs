using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if( _Instance == null)
        {
            _Instance = this;
            _InputSystem = new InputManager();
            _Camera = Camera.main;
            Spawner = Spawn();
            for(int i = 0; i < _SpownPoionts.childCount; i++)
            {
                _Spowns.Add(_SpownPoionts.GetChild(i).transform);
            }
            DontDestroyOnLoad(gameObject);
        }
        else {
            Time.timeScale = 1;
            _Instance._Camera = Camera.main;
            _Instance._Spowns.Clear();
            _Instance._Player = this._Player;
            _Instance._SpownPoionts = this._SpownPoionts;
            for (int i = 0; i < _SpownPoionts.childCount; i++)
            {
                _Instance._Spowns.Add(_SpownPoionts.GetChild(i).transform);
            }
            Destroy(gameObject);
        }
    }



    public Player GM_Player
    {
        get { return _Instance._Player;}
    }

    public InputManager GM_InputManager
    {
        get { return _InputSystem; }
    }

    public Camera GM_Camera
    {
        get { return _Camera;}
    }

    public IEnumerator SpawnEnemies
    {
        get { return Spawner; }
    }

    

    IEnumerator Spawn()
    {
        while (true)
        {
            if(_EnemiCoubter < _LimitEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
        
    }

    public void AddNewZombie(Transform zombie)
    {
        _Zombies.Add(zombie);
    }
    
    public void RemoveZombie(Transform zombie)
    {
        _Zombies.Remove(zombie);
        _EnemiCoubter--;
    }

    public bool CanIHaveZombies()
    {
        return _Zombies.Count > 0;
    }

    public Transform GetTargget()
    {
        return _Zombies[Random.Range(0,_Zombies.Count)];
    }
    public int ShowZombiesNum()
    {
        return _Zombies.Count;
    }

    

    public void ChangeTarggets()
    {
        foreach(GameObject _cops in _currentCops)
        {
            if(_cops.GetComponent<Zombies>()._cop_Targget == null)
            {
                _cops.GetComponent<Zombies>().ChageTargget();
            }
        }
    }

    InputManager _InputSystem;
    [SerializeField]
    Player _Player;
    [SerializeField]
    Camera _Camera;
    [SerializeField]
    Transform _SpownPoionts;
    List<Transform> _Spowns = new List<Transform>();

    int _EnemiCoubter = 0;
    [SerializeField]
    int _LimitEnemies = 100;
    [SerializeField]
    GameObject Police;

    List<Transform> _Zombies = new List<Transform>();
    List<GameObject> _currentCops = new List<GameObject>();


    IEnumerator Spawner;

    private void SpawnEnemy()
    {
        Transform[] close = _Spowns.FindAll(e => Vector3.Distance(e.position, _Player.transform.position) <= 100 && Vector3.Distance(e.position, _Player.transform.position) >= 50).ToArray();

        foreach(Transform t in close)
        {
            int random = Random.Range(0, 40)+1;
            if (random >= _EnemiCoubter)
            {
                Vector3 newPos = t.transform.position;
                newPos.x += Random.Range(5, 30);
                newPos.z += Random.Range(5, 30);
                Instantiate(Police, newPos, Quaternion.identity);
                _EnemiCoubter++;
            }

        }
    }
}
