using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IEnemyAttack
{
    void StartAttack();
    IEnumerator RepeatAttack();
}
public abstract class EnemyAttackBase : MonoBehaviour, IEnemyAttack
{
    [SerializeField] protected PoolObjectType poolObjectType;
    protected PlayerMovement playerMovement;

    public float repeatTime;
    public float startDelay;
    protected virtual void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        Invoke(nameof(StartAttack), startDelay);
    }
    public void StartAttack()
    {
        StartCoroutine(RepeatAttack());
    }
    public abstract IEnumerator RepeatAttack();
}
