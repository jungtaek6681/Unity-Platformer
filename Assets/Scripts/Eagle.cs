using UnityEngine;

public class Eagle : Monster
{
    public enum State { Idle, Trace, Return }

    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;

    private State state = State.Idle;
    private Transform target;
    private Vector2 startPos;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        startPos = transform.position;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Trace:
                TraceUpdate();
                break;
            case State.Return:
                ReturnUpdate();
                break;
        }
    }

    private void IdleUpdate()
    {
        // Do nothing

        if (Vector2.Distance(target.position, transform.position) < findRange)
        {
            state = State.Trace;
        }
    }

    private void TraceUpdate()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

        if (Vector2.Distance(target.position, transform.position) > findRange)
        {
            state = State.Return;
        }
    }

    private void ReturnUpdate()
    {
        Vector2 dir = ((Vector3)startPos - transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

        if (Vector2.Distance(startPos, transform.position) < 0.1f)
        {
            state = State.Idle;
        }
    }
}
