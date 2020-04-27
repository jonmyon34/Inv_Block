using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamagable
{
    [SerializeField] private int hp = 3;
    [SerializeField] private bool isInvincibleMode = false;
    [SerializeField] private float invincibleTime = 3.0f;
    public bool exist = true;

    Renderer plRendere;

    GameObject effectPrefab;

    AudioSource audioSource;
    AudioClip explosionSound;
    AudioClip damageSound;

    void Awake()
    {
        plRendere = gameObject.GetComponent<Renderer>();

        effectPrefab = (GameObject)Resources.Load("Prefabs/BigExplosionCustomed");
        audioSource = this.gameObject.GetComponent<AudioSource>();
        explosionSound = (AudioClip)Resources.Load("Sounds/Explosion");
        damageSound = (AudioClip)Resources.Load("Sounds/Damage");
    }

    void Start()
    {
        //hp<=0ならexist = false
        this.UpdateAsObservable()
            .First(_ => hp <= 0)
            .Subscribe(_ => { exist = false; }, () => Debug.Log("OnCompleted"))
            .AddTo(gameObject);

        //exist == falseなら死
        this.UpdateAsObservable()
            .First(_ => !exist)
            .Subscribe(_ =>
            {
                //非表示 && コライダー消去
                this.gameObject.GetComponent<Renderer>().enabled = false;
                this.gameObject.GetComponent<Collider>().enabled = false;

                StartCoroutine("Die");
            }, () => Debug.Log("OnCompleted"))
            .AddTo(gameObject);

        //ダメージ後の無敵が終わった時に表示が消えていたら戻す
        this.UpdateAsObservable()
            .Where(_ => !isInvincibleMode && !plRendere.enabled && exist)
            .Subscribe(_ => plRendere.enabled = true)
            .AddTo(gameObject);
    }

    public void AddDamage()
    {
        if (!isInvincibleMode)
        {
            hp--;
            StartCoroutine("InvincibleMode");
            StartCoroutine("DamageFlashing");
        }
    }

    public int GetHp() { return hp; }

    IEnumerator InvincibleMode()
    {
        isInvincibleMode = true;
        audioSource.PlayOneShot(damageSound);
        yield return new WaitForSeconds(invincibleTime);
        isInvincibleMode = false;
    }

    IEnumerator DamageFlashing()
    {
        while (isInvincibleMode && exist)
        {
            plRendere.enabled = !plRendere.enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Die()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = this.gameObject.transform.position;
            pos.x += Random.Range(-2.0f, 2.0f);
            pos.y += 2.0f;
            pos.z += Random.Range(-2.0f, 2.0f);
            GameObject obj = Instantiate(effectPrefab, pos, Quaternion.identity);
            obj.transform.parent = this.gameObject.transform;
            obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            audioSource.PlayOneShot(explosionSound);

            yield return new WaitForSeconds(0.3f);
        }
    }
}