namespace Fsm
{
    public class FsmDemo2
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

        private bool _isStarted;
        private int _innerDataDelta;

        private bool _forceState2Required;

        public void OnStart()
        {
            _isStarted = true;
            UpdateState();
        }

        public void OnStop()
        {
            _isStarted = false;
            UpdateState();
        }

        public void Action1()
        {
            _innerDataDelta = 1;
            UpdateState();
            _innerDataDelta = 0;
        }

        public void Action2()
        {
            _innerDataDelta = -1;
            UpdateState();
            _innerDataDelta = 0;
        }

        public void Action3()
        {
            _forceState2Required = true;
            UpdateState();
            _forceState2Required = false;
        }

        private bool Condition1()
        {
            return false;
        }

        private bool Condition2()
        {
            return false;
        }

        private void UpdateState()
        {
            switch (_innerState)
            {

                case State.State0:
                    if (_isStarted)
                    {
                        _innerState = State.State1;
                        break;
                    }
                    break;

                case State.State1:
                    if (AnalyzeStarted())
                    {
                        _innerState = State.State0;
                        break;
                    }

                    if (AnalyzePositiveInnerDataDelta())
                    {
                        _innerState = State.State2;
                        break;
                    }

                    if (_forceState2Required)
                    {
                        _innerState = State.State2;
                        break;
                    }

                    if (Condition1())
                    {
                        _innerState = State.State3;
                        break;
                    }
                    break;

                case State.State2:
                    if (AnalyzeStarted())
                    {
                        _innerState = State.State0;
                        break;
                    }

                    if (AnalyzeNegativeInnerDataDelta())
                    {
                        _innerState = State.State1;
                        break;
                    }
                    if (Condition2())
                    {
                        _innerState = State.State3;
                        break;
                    }
                    break;

                case State.State3:
                    if (AnalyzeStarted())
                    {
                        _innerState = State.State0;
                        break;
                    }

                    if (AnalyzePositiveInnerDataDelta())
                    {
                        _innerState = State.State2;
                        break;
                    }
                    if (_forceState2Required)
                    {
                        _innerState = State.State2;
                        break;
                    }
                    if (!Condition1())
                    {
                        _innerState = State.State1;
                        break;
                    }
                    if (!Condition2())
                    {
                        _innerState = State.State2;
                        break;
                    }
                    break;
            }

            bool AnalyzePositiveInnerDataDelta()
            {
                if (_innerDataDelta < 0)
                    return false;

                _innerData += _innerDataDelta;
                _innerDataDelta = 0;

                if (_innerData >= 10)
                {
                    return true;
                }

                return false;
            }

            bool AnalyzeNegativeInnerDataDelta()
            {
                if (_innerDataDelta > 0)
                    return false;

                _innerData += _innerDataDelta;
                _innerDataDelta = 0;

                if (_innerData < 10)
                {
                    return true;
                }

                return false;
            }

            bool AnalyzeStarted()
            {
                return !_isStarted;
            }
        }
    }
}
