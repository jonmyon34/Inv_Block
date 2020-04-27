using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockHealthManager : MonoBehaviour, IDamagable
{
    [SerializeField] int hp = 1;
    [SerializeField] bool exist = true;

    GameObject effectPrefab;
    AudioSource audioSource;
    AudioClip damageSound;
    AudioClip explosionSound;

    bool isFlashingMode;
    Renderer blockRenderer;

    void Awake()
    {
        effectPrefab = (GameObject)Resources.Load("Prefabs/BigExplosionCustomed");
        audioSource = this.gameObject.GetComponent<AudioSource>();
        explosionSound = (AudioClip)Resources.Load("Sounds/Explosion");
        damageSound = (AudioClip)Resources.Load("Sounds/Damage");

        isFlashingMode = false;
        blockRenderer = this.gameObject.GetComponent<Renderer>();
    }

    void Start()
    {
        //hp<=0ならexistをfalse
        this.UpdateAsObservable()
            .First(_ => hp <= 0)
            .Subscribe(_ => { exist = false; }, () => Debug.Log("OnCompleted"))
            .AddTo(gameObject);

        //exist = falseなら死
        this.UpdateAsObservable()
            .First(_ => !exist)
            .Subscribe(_ =>
            {
                //エフェクト処理 && コライダ消去 && 表示消去 && 音
                GameObject obj = Instantiate(effectPrefab, this.gameObject.transform.position, Quaternion.identity);
                obj.transform.parent = this.gameObject.transform;
                obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                this.gameObject.GetComponent<Renderer>().enabled = false;
                this.gameObject.GetComponent<Collider>().enabled = false;
                audioSource.PlayOneShot(explosionSound);

                StartCoroutine("Die");
            }, () => Debug.Log("OnCompleted"))
            .AddTo(gameObject);

        //ダメージ後の非表示が終わった時に表示が消えていたら戻す
        this.UpdateAsObservable()
            .Where(_ => !isFlashingMode && !blockRenderer.enabled && exist)
            .Subscribe(_ => blockRenderer.enabled = true)
            .AddTo(gameObject);

    }

    public void AddDamage()
    {
        hp--;
        StartCoroutine("FlashingMode");
        StartCoroutine("DamageFlashing");
    }

    IEnumerator FlashingMode()
    {
        isFlashingMode = true;
        audioSource.PlayOneShot(damageSound);
        yield return new WaitForSeconds(0.05f);
        isFlashingMode = false;
    }

    IEnumerator DamageFlashing()
    {
        while (isFlashingMode && exist)
        {
            blockRenderer.enabled = !blockRenderer.enabled;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

}