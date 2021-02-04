using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject playerGraphics;
    [SerializeField] TrailRenderer trailRenderer;
    private Collider _collider;

    private PlayerMovement _movement;

    private bool _hasConsumedAnyItem;

    private ItemBehaviourData _currentConsumedItemData;

    private bool _isPlayerDamageable;

    private float _cooldownTime = 3.0f;

    private float blinkStartTime;
    bool _isDie;

    void Start()
    {
        _currentConsumedItemData = new ItemBehaviourData();
        _movement = this.gameObject.GetComponent<PlayerMovement>();
        _collider = GetComponent<Collider>();

        _collider.enabled = false;
        explosionEffect.SetActive(false);
        _isPlayerDamageable = false;

        Invoke("MakePlayerDamageable", _cooldownTime);
        blinkStartTime = Time.time;
    }


    private void Update()
    {
        if (_isPlayerDamageable == false)
        {
            // blink
            if (Time.time - blinkStartTime >= .1f)
            {
                playerGraphics.SetActive(!playerGraphics.activeInHierarchy);
                blinkStartTime = Time.time;
            }
        }


        if (GetPlayerCurrentPosition().x >= 4.8 || GetPlayerCurrentPosition().x <= -4.8)
        {
            if (_isDie == false)
                Die();

        }
    }

    void MakePlayerDamageable()
    {
        _collider.enabled = true;
        _isPlayerDamageable = true;

        playerGraphics.SetActive(true);
    }

    public Vector3 GetPlayerCurrentPosition()
    {
        return transform.position;
    }

    public TrailRenderer GetTrailRenderer()
    {
        return trailRenderer;
    }

    public void Die()
    {
        _isDie = true;
        _movement.StopMovement();
        playerGraphics.SetActive(false);
        //Debug.Log("Die in Player C");
        GameAudioManager.instance.PlayPlayerDieSound();
        explosionEffect.SetActive(true);

        Invoke("CallEndGame", 1f);
    }

    private void CallEndGame()
    {
        GameManager.instance.PlayerDied(transform.position);
		_collider.enabled = false;
        //Destroy(this.gameObject);
    }

    public void ActivateConsumedItem(ItemBehaviourData data)
    {
        _currentConsumedItemData = data;
        _hasConsumedAnyItem = true;

        if (_currentConsumedItemData.itemType == ItemTypeEnum.ItemType.Breaker)
        {
            _movement.SetExtremeSpeed();
        }
        CancelInvoke();
        Invoke("DeactivateConsumedItem", data.activeTime);
    }

    private void DeactivateConsumedItem()
    {
        if (_currentConsumedItemData.itemType == ItemTypeEnum.ItemType.Breaker)
        {
			_collider.enabled = false;
			_isPlayerDamageable = false;
			Invoke("MakePlayerDamageable", _cooldownTime);

			_movement.GoNormalSpeed();

        }
        _currentConsumedItemData = new ItemBehaviourData();
        _hasConsumedAnyItem = false;
    }

    public ItemBehaviourData GetCurrentConsumedItemData()
    {
        return _currentConsumedItemData;
    }

    public PlayerMovement GetPlayerMovement()
    {
        if (_movement == null)
            _movement = this.gameObject.GetComponent<PlayerMovement>();
        return _movement;
    }
}
