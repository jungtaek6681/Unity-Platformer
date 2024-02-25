using UnityEngine;

public class Eagle : Monster
{
    public enum State { Idle, Trace, Return, Size }

    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;

    private IState[] states;
    private State state;
    private Transform target;
    private Vector2 startPos;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        startPos = transform.position;

        states = new IState[(int)State.Size];
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Trace] = new TraceState(this);
        states[(int)State.Return] = new ReturnState(this);

        state = State.Idle;
        states[(int)State.Idle].Enter();
    }

    private void Update()
    {
        states[(int)state].Update();
    }

    private class BaseState : IState
    {
        protected Eagle owner;

        public BaseState(Eagle owner)
        {
            this.owner = owner;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }

    private class IdleState : BaseState
    {
        public IdleState(Eagle owner) : base(owner) { }

        public override void Update()
        {
            if (Vector2.Distance(owner.target.position, owner.transform.position) < owner.findRange)
            {
                owner.state = State.Trace;
            }
        }
    }

    private class TraceState : BaseState
    {
        public TraceState(Eagle owner) : base(owner) { }

        public override void Update()
        {
            Vector2 dir = (owner.target.position - owner.transform.position).normalized;
            owner.transform.Translate(dir * owner.moveSpeed * Time.deltaTime, Space.World);

            if (Vector2.Distance(owner.target.position, owner.transform.position) > owner.findRange)
            {
                owner.state = State.Return;
            }
        }
    }

    private class ReturnState : BaseState
    {
        public ReturnState(Eagle owner) : base(owner) { }

        public override void Update()
        {
            Vector2 dir = ((Vector3)owner.startPos - owner.transform.position).normalized;
            owner.transform.Translate(dir * owner.moveSpeed * Time.deltaTime, Space.World);

            if (Vector2.Distance(owner.startPos, owner.transform.position) < 0.1f)
            {
                owner.state = State.Idle;
            }
        }
    }
}
