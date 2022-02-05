using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 _velocity = Vector3.zero;

    [SerializeField]
    float _GeneralSpeed = 3f;

    Vector3 _Rotation = Vector3.zero;

    [SerializeField]
    NavMeshAgent _navMesh;
    [SerializeField]
    LayerMask _hitLayer;

    float _Life = 200;
    float initLife = 0;
    [SerializeField]
    Material []_Materials;
    [SerializeField]
    MeshRenderer _meshRender;

    [SerializeField]
    GameObject dead_Panel;
    [SerializeField]
    GameObject win_Panel;

    [SerializeField]
    Image _lifeImage;

    public GameObject[] terrains;
    
    public TextMeshProUGUI UI_ZombiCounter;
    [SerializeField]
    GameObject[] Doors;
    void Start()
    {
        initLife = _Life;
        GameManager._Instance.GM_InputManager.Controller.MousePos1.performed += cntx => Move(cntx.ReadValue<Vector2>());
        GameManager._Instance.GM_InputManager.Controller.Enable();
    }
    public void SetUpDoors()
    {
        Doors[Random.Range(0,Doors.Length)].SetActive(true);
    }
    private void Move(Vector2 move)
    {
        try
        {
            Ray targetPos = GameManager._Instance.GM_Camera.ScreenPointToRay(move);

            RaycastHit hit;


            if (Physics.Raycast(targetPos, out hit, Mathf.Infinity, _hitLayer))
            {
                Debug.DrawLine(targetPos.origin, hit.point);
                //Debug.Log("fjkldñsjfklñasdjf " + hit.point);
                _navMesh.SetDestination(hit.point);
            }
        }
        catch (MissingReferenceException) { }
        catch (System.NullReferenceException) { }
    }

    public void GetDamage(float damage)
    {
        _Life -= damage;
        StartCoroutine(ShowDamage());
        float ammount = _Life / initLife;
        _lifeImage.fillAmount = ammount;
        if(_Life <= 0)
        {
            GameManager._Instance.GM_InputManager.Controller.MousePos1.performed -= cntx => Move(cntx.ReadValue<Vector2>());
            GameManager._Instance.StopAllCoroutines();
            Destroy(gameObject);
            dead_Panel.SetActive(true);
        }
    }

    IEnumerator ShowDamage()
    {
        _meshRender.material = _Materials[1];
        yield return new WaitForSeconds(0.5f);
        _meshRender.material = _Materials[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            Time.timeScale = 0;
            dead_Panel.SetActive(true);
            win_Panel.SetActive(true);
        }
    }

    
}
