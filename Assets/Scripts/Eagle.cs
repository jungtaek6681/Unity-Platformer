using UnityEngine;

public class Eagle : Monster
{
    public enum State { Idle, Trace, Return }

    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;

    private StateMachine stateMachine;
    private Transform target;
    private Vector2 startPos;

    private void Awake()
    {
        stateMachine = gameObject.AddComponent<StateMachine>();
        stateMachine.AddState(State.Idle, new IdleState(this));
        stateMachine.AddState(State.Trace, new TraceState(this));
        stateMachine.AddState(State.Return, new ReturnState(this));
        stateMachine.InitState(State.Idle);
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        startPos = transform.position;
    }

    private class EagleState : BaseState
    {
        protected Eagle owner;
        protected Transform transform => owner.transform;

        protected float moveSpeed => owner.moveSpeed;
        protected float findRange => owner.findRange;
        protected Transform target => owner.target;
        protected Vector2 startPos => owner.startPos;

        public EagleState(Eagle owner)
        {
            this.owner = owner;
        }
    }

    private class IdleState : EagleState
    {
        public IdleState(Eagle owner) : base(owner) { }

        public override void Transition()
        {
            if (Vector2.Distance(target.position, transform.position) < findRange)
            {
                ChangeState(State.Trace);
            }
        }
    }

    private class TraceState : EagleState
    {
        public TraceState(Eagle owner) : base(owner) { }

        public override void Update()
        {
            Vector2 dir = (target.position - transform.position).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
        }

        public override void Transition()
        {
            if (Vector2.Distance(target.position, transform.position) > findRange)
            {
                ChangeState(State.Return);
            }
        }
    }

    private class ReturnState : EagleState
    {
        public ReturnState(Eagle owner) : base(owner) { }

        public override void Update()
        {
            Vector2 dir = ((Vector3)startPos - transform.position).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
        }

        public override void Transition()
        {
            if (Vector2.Distance(startPos, transform.position) < 0.1f)
            {
                ChangeState(State.Idle);
            }
        }
    }
}
