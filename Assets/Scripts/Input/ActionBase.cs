using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.Input
{
    public class ActionBase
    {
        // Private hidden flags from Action class
		private bool m_startFlag = false;
		private bool m_activeFlag = false;
		private bool m_cancelFlag = false;

		// Public visible flags in Action class
		public bool Start // Set on if active flag was just turned on, turns off with next RuntimeCheck function call
		{
			get => m_startFlag;
			set
			{
				m_startFlag = value;
				OnStart?.Invoke();
			}
		}
		public bool Active // Set on if Enable was called
		{
			get => m_activeFlag;
			set
			{
				m_activeFlag = value;
			}
		}
		public bool Cancel // Set on if Disable just was called, turns off with next RuntimeCheck function call
		{
			get => m_cancelFlag;
			set
			{
				m_cancelFlag = value;
				OnCancel?.Invoke();
			}
		}

		// Public visible System.Action & System.Funcs in Action class
		public System.Action OnStart { protected get; set; } // Called when start flag is turned on
		public System.Action OnCancel { protected get; set; } // Called when cancel flag is turned on
		public System.Func<bool> RuntimeConditionCheck { protected get; set; } // Called by the ActionHandler to check if the action is active
		public System.Action OnRuntimeActive { protected get; set; } // Called every RuntimeCheck call if Active == true
		public System.Action OnRuntimeNonActive { protected get; set; } // Called every RuntimeCheck call if Active == false
		public System.Action OnDelta { protected get; set; } // Called every time Enable or Disable is called
    }
}
