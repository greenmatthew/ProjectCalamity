using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.Input
{
    public class Action : ActionBase
    {
        public Action()
            : base()
		{
            ActionHandler.AddAction(this);
		}

		// Enables both the start an active flags unless the Action's active flag was already on
		public void Enable()
		{
			if (!Active)
			{
				Start = true;
				Active = true;
				OnDelta?.Invoke();
			}
		}

		// Disables both the start an active flags unless the Action's active flag was already off
		public void Disable()
		{
			if (Active)
			{
				Active = false;
				Cancel = true;
				OnDelta?.Invoke();
			}
		}

		// Calls the Enable or Disable functions depending on what condition was given
		public void Set(bool? condition)
		{
			if (condition.HasValue)
			{
				if (condition.Value)
					Enable();
				else
					Disable();
			}
		}

		// Toggles the active flag, using the Enable and Disable functions
		public void Toggle()
		{
			Set(!Active);
		}

		// Does runtime duties
		public void PerformRuntimeConditionChecks()
		{
			if (Start)
				Start = false;
			if (Cancel)
				Cancel = false;

			Set(RuntimeConditionCheck?.Invoke());
			if (Active)
				OnRuntimeActive?.Invoke();
			if (!Active)
				OnRuntimeNonActive?.Invoke();
		}
    }
}
