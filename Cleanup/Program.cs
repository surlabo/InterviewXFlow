namespace Cleanup
{
    internal class Program
    {
        private const double TargetChangeTime = 1;

        private double _previousTargetSetTime;
        private bool _isTargetSet;
        private object _lockedCandidateTarget;
        private object _lockedTarget;
        private object _target;
        private object _previousTarget;
        private object _activeTarget;
        private object _targetInRangeContainer;

        public void CleanupTest(Frame frame)
        {
            try
            {
                if (_lockedCandidateTarget && !_lockedCandidateTarget.CanBeTarget)
                {
                    _lockedCandidateTarget = null;
                }

                if (_lockedTarget && !_lockedTarget.CanBeTarget)
                {
                    _lockedTarget = null;
                }

                _isTargetSet = false;
                TrySetActiveTargetFromQuantum(frame);

                // If target exists and can be targeted, it should stay within Target Change Time since last target change
                if (_target && _target.CanBeTarget && Time.time - _previousTargetSetTime < TargetChangeTime)
                {
                    _isTargetSet = true;
                }
                _previousTarget = _target;

                if (!_isTargetSet)
                {
                    if (_lockedTarget && _lockedTarget.CanBeTarget)
                    {
                        _target = _lockedTarget;
                        _isTargetSet = true;
                        return;
                    }
                }


                if (!_isTargetSet)
                {
                    if (_activeTarget && _activeTarget.CanBeTarget)
                    {
                        _target = _activeTarget;
                        _isTargetSet = true;
                        return;
                    }
                }

                if (!_isTargetSet)
                {
                    _target = _targetInRangeContainer.GetTarget();
                    if (_target)
                    {
                        _isTargetSet = true;
                    }
                }
            }
            finally
            {
                if (_isTargetSet)
                {
                    if (_previousTarget != _target)
                    {
                        _previousTargetSetTime = Time.time;
                    }
                }
                else
                {
                    _target = null;
                }
                TargetableEntity.Selected = _target;
            }
        }
    }
}
