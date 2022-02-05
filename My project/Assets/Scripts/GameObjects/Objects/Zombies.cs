using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombies : MonoBehaviour
{
    Player z_Player;
    [SerializeField]
    NavMeshAgent z_Agent;

    IEnumerator Follwing;
    IEnumerator Attack;

    float DistanceToStopPolice = 30;
    float DistanceToStopZombie = 10;
    [SerializeField]
    LineRenderer lineRenderer;
    [HideInInspector]
    public Transform _cop_Targget = null;

    float Life = 100;
    float _InitLife = 0;

    [SerializeField]
    MeshRenderer _meshRender;
    [SerializeField]
    Material[] _Materials;
    [SerializeField]
    LayerMask _hitLayer;
    Vector3[] Positions = null;
    [SerializeField]
    Image _LiferImage;

    float ammount = 1;
    // Start is called before the first frame update
    void Awake()
    {
        Follwing = FollowPlayer();
        z_Player = GameManager._Instance.GM_Player;
        z_Agent.stoppingDistance = DistanceToStopPolice;
        Attack = Attacking();
        ChageTargget();
        z_Agent.acceleration = 30;
        z_Agent.speed = 20;
        _InitLife = Life;
        _LiferImage.transform.parent.parent.gameObject.GetComponent<Canvas>().worldCamera = GameManager._Instance.GM_Camera;
        StartCoroutine(Follwing);
    }


    IEnumerator FollowPlayer()
    {
        while (true)
        {
            try
            {
                //if (Vector3.Distance(z_Agent.transform.position, z_Player.transform.position) > 2)
                z_Agent.SetDestination(z_Player.transform.position);
                /*if(gameObject.layer == 7 && _cop_Targget != null)
                {
                   lineRenderer.enabled = 
                    Debug.Log("i am following ");
                }
                else*/
                if (gameObject.layer == 7 && _cop_Targget == null)
                {
                    ChageTargget();
                    //lineRenderer.enabled = false;
                }
                else if (gameObject.layer == 6)
                {
                    lineRenderer.enabled = false;
                }

               // yield return new WaitForEndOfFrame();

                if (Positions != null)
                {

                    Positions[1] = transform.position;
                    lineRenderer.SetPositions(Positions);
                }
            }
            catch (System.NullReferenceException) { }
            catch (MissingReferenceException) { }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Attacking()
    {

        Debug.Log(_cop_Targget == null);
        while (_cop_Targget != null)
        {
            lineRenderer.enabled = true;
            if (_cop_Targget != z_Player.transform)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (_cop_Targget.position - transform.position).normalized, out hit, Mathf.Infinity, _hitLayer))
                {

                    Positions = new Vector3[2];
                    Positions[0] = hit.point;
                    Positions[1] = transform.position;
                    
                    if(hit.transform.gameObject.layer == 6)
                        _cop_Targget.GetComponent<Zombies>().GetDamage(Random.Range(5, 10), this);
                }

                

                //yield return new WaitForSeconds(Random.Range(0.4f, 1));
            }
            else
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position,(z_Player.transform.position - transform.position).normalized,out hit, Mathf.Infinity, _hitLayer))
                {
                    Positions = new Vector3[2];
                    Positions[0] = hit.point;
                    Positions[1] = transform.position;
                    if (hit.transform.gameObject.layer == 3)
                    {
                        //lineRenderer.SetPositions(Positions);
                        try
                        {
                            z_Player.GetDamage(Random.Range(1, 3));
                        }
                        catch (System.NullReferenceException) {
                            _cop_Targget = null;
                            StopAllCoroutines();
                        }catch (MissingReferenceException)
                        {
                            _cop_Targget = null;
                            StopAllCoroutines();
                        }
                    }
                }

            }

            yield return new WaitForSeconds(0.2f);
            lineRenderer.enabled = false;
            yield return new WaitForSeconds(Random.Range(2f,3f));
            //Debug.Log("I hit two " + name);
        }
    }


    public void GetDamage(float Damage, Zombies whoAttacks)
    {
        Life -= Damage;
        ammount = Life / _InitLife;
        _LiferImage.fillAmount = ammount;
        StartCoroutine(ShowDamage());
        if(Life <= 0)
        {
            GameManager._Instance.RemoveZombie(transform);
            Destroy(gameObject);
            whoAttacks.ChageTargget();
            z_Player.UI_ZombiCounter.text = GameManager._Instance.ShowZombiesNum().ToString("0000");
        }
    }


    IEnumerator ShowDamage()
    {
        _meshRender.material = _Materials[1];
        yield return new WaitForSeconds(0.2f);
        _meshRender.material = _Materials[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && gameObject.layer == 7)
        {
            gameObject.layer = 6;
            gameObject.name = "Zombie";
            z_Agent.stoppingDistance = DistanceToStopZombie;
            GameManager._Instance.AddNewZombie(transform);
            _meshRender.material = _Materials[0];
            //StopCoroutine(Attack);
            z_Agent.acceleration = 20;
            z_Agent.speed = 15;
            _cop_Targget = null;
            _LiferImage.transform.parent.parent.gameObject.SetActive(true);
            z_Player.UI_ZombiCounter.text = GameManager._Instance.ShowZombiesNum().ToString("0000");
        }
    }

    public void ChageTargget()
    {
        if (GameManager._Instance.CanIHaveZombies())
        {
            _cop_Targget = GameManager._Instance.GetTargget();
        }
        else
        {
            _cop_Targget = GameManager._Instance.GM_Player.transform;
        }
        Debug.Log("I start attacking");
        StartCoroutine(Attacking());
    }
}
