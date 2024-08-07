// To Simplify the code we can break down the logic into smaller and reusable methods.

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
                ValidateLockedTargets();
                
                _isTargetSet = false;

                TrySetActiveTargetFromQuantum(frame);

                if (IsTargetValid(_target)) return;

                _previousTarget = _target;

                if (!_isTargetSet)
                {
                    TrySetTarget(_lockedTarget);
                }

                if (!_isTargetSet)
                {
                    TrySetTarget(_activeTarget);
                }

                if (!_isTargetSet)
                {
                    _target = _targetInRangeContainer.GetTarget();
                    if (_target != null)
                    {
                        _isTargetSet = true;
                    }
                }
            }
            finally
            {
                FinalizeTargetSelection();
            }
        }

        private void ValidateLockedTargets()
        {
            if (_lockedCandidateTarget != null && !_lockedCandidateTarget.CanBeTarget)
            {
                _lockedCandidateTarget = null;
            }

            if (_lockedTarget != null && !_lockedTarget.CanBeTarget)
            {
                _lockedTarget = null;
            }
        }

        // Using object only because I don't know the type
        private bool IsTargetValid(object target)
        {
            return target != null && target.CanBeTarget && Time.time - _previousTargetSetTime < TargetChangeTime;
        }

        // Using object only because I don't know the type
        private void TrySetTarget(object potentialTarget)
        {
            if (potentialTarget != null && potentialTarget.CanBeTarget)
            {
                _target = potentialTarget;
                _isTargetSet = true;
            }
        }

        private void FinalizeTargetSelection()
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

        private void TrySetActiveTargetFromQuantum(Frame frame)
        {
            // Implementation of TrySetActiveTargetFromQuantum
        }
    }
}
