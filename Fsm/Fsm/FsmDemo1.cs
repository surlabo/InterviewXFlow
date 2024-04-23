namespace Fsm
{
    public class FsmDemo1
    {
        private enum State
        {
            State0,
            State1,
            State2,
            State3
        }

        private State _innerState;

        private int _innerData;

        public void OnStart()
        {
            _innerState = State.State1;
        }

        public void OnStop()
        {
            _innerState = State.State0;
        }

        public void Action1()
        {
            if (_innerState != State.State2)
            {
                _innerData++;
            }

            if (_innerData >= 10)
            {
                _innerState = State.State2;
            }
        }

        public void Action2()
        {
            _innerData--;

            if (_innerData < 10)
            {
                _innerState = State.State1;
            }
        }

        public void Action3()
        {
            _innerState = State.State2;
        }

        public void Update()
        {
            switch (_innerState)
            {
                case State.State0:
                    return;

                case State.State1:
                    if (Condition1())
                    {
                        _innerState = State.State3;
                    }
                    break;

                case State.State2:
                    if (Condition2())
                    {
                        _innerState = State.State3;
                    }
                    break;

                case State.State3:
                    if (!Condition1())
                    {
                        _innerState = State.State1;
                    }
                    if (!Condition2())
                    {
                        _innerState = State.State2;
                    }
                    break;
            }
        }

        private bool Condition1()
        {
            return false;
        }

        private bool Condition2()
        {
            return false;
        }
    }
}
