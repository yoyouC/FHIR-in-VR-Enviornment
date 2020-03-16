using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YoyouOculusFramework
{
    public class HandTrackingButton : Interactable
{private const float ENTRY_DOT_THRESHOLD = 0.8f;
		private const float PERP_DOT_THRESHOLD = 0.5f;

		public UnityEvent OnEnterActionZone;
		public UnityEvent OnExitActionZone;
		[SerializeField] private GameObject _actionZone = null;
		[SerializeField] private ContactTest[] _contactTests = null;
		// for positive side tests, the contact position must be on the positive side of the plane
		// determined by this transform
		[SerializeField] private Transform _buttonPlaneCenter = null;

		// make sure press is coming from "positive" side of button, i.e. above it
		[SerializeField] private bool _makeSureToolIsOnPositiveSide = true;
		// depending on the geometry used, the direction might not always be downwards.
		[SerializeField] private Vector3 _localButtonDirection = Vector3.down;
		[SerializeField]
		private InteractableToolTags[] _allValidToolsTags =
		  new InteractableToolTags[] { InteractableToolTags.All };
		private int _toolTagsMask;


		public override int ValidToolTagsMask
		{
			get
			{
				return _toolTagsMask;
			}
		}

		public enum ContactTest
		{
			PerpenTest = 0, // is tool pointing along button normal?
			BackwardsPress // filter out presses coming backwards?
		}

		public Vector3 LocalButtonDirection
		{
			get { return _localButtonDirection; }
		}

		private InteractableState _currentButtonState = InteractableState.Default;

		private Dictionary<InteractableTool, InteractableState> _toolToState =
		  new Dictionary<InteractableTool, InteractableState>();

		protected override void Awake()
		{
			base.Awake();
			Assert.IsNotNull(_actionZone);
			Assert.IsNotNull(_buttonPlaneCenter);

			foreach (var interactableToolTags in _allValidToolsTags)
			{
				_toolTagsMask |= (int)interactableToolTags;
			}

			_actionZoneCollider = _actionZone.GetComponent<ColliderZone>();
		}

		public override void UpdateCollisionDepth(InteractableTool interactableTool,
		  InteractableCollisionDepth oldCollisionDepth,
		  InteractableCollisionDepth collisionDepth, InteractableTool collidingTool)
		{
			bool isFarFieldTool = interactableTool.IsFarFieldTool;

			// if this is a near field tool and another tool already controls it, bail.
			if (!isFarFieldTool && _toolToState.Keys.Count > 0 && !_toolToState.ContainsKey(interactableTool))
			{
				return;
			}

			var oldState = _currentButtonState;

			// ignore contact test if you are using the far field tool
			var currButtonDirection = transform.TransformDirection(_localButtonDirection);
			bool validContact = IsValidContact(collidingTool, currButtonDirection) || collidingTool.IsFarFieldTool;
			// in case finger enters contact zone first, we are in proximity as well
			bool toolInActionZone = collisionDepth == InteractableCollisionDepth.Action;

			// plane describing positive side of button
			var buttonZonePlane = new Plane(-currButtonDirection, _buttonPlaneCenter.position);
			// skip plane test if the boolean flag tells us not to test it
			bool onPositiveSideOfButton = !_makeSureToolIsOnPositiveSide ||
			  buttonZonePlane.GetSide(collidingTool.InteractionPosition);

			bool switchingStates = oldCollisionDepth != collisionDepth;

			var newState = oldState;
			if (collidingTool.IsFarFieldTool)
			{
				newState = toolInActionZone ? InteractableState.ActionState : InteractableState.Default;
			}
			else
			{
				switch (oldState)
				{
					case InteractableState.ActionState:
						if (!toolInActionZone)
						{
							newState = InteractableState.Default;
							if(OnExitActionZone != null) OnExitActionZone.Invoke();
						}

						break;
					case InteractableState.Default:
						// test contact, action first then proximity (more important states
						// take precedence)
						if (validContact &&
							  collisionDepth == InteractableCollisionDepth.Action)
						{
							newState = InteractableState.ActionState;
							if(OnEnterActionZone != null) OnEnterActionZone.Invoke();
						}

						break;
				}
			}

			if (newState != InteractableState.Default)
			{
				_toolToState[interactableTool] = newState;
			}
			else
			{
				_toolToState.Remove(interactableTool);
			}

			// far field tools depend on max state set
			if (isFarFieldTool)
			{
				foreach (var toolState in _toolToState.Values)
				{
					if (newState < toolState)
					{
						newState = toolState;
					}
				}
			}

			if (oldState != newState)
			{
				_currentButtonState = newState;
				var interactionType = !switchingStates ? InteractionType.Stay :
				  collisionDepth == InteractableCollisionDepth.None ? InteractionType.Exit :
				  InteractionType.Enter;
				if (InteractableStateChanged != null)
				{
					InteractableStateChanged.Invoke(new InteractableStateArgs(this, interactableTool,
					  _currentButtonState, oldState, new ColliderZoneArgs(ContactCollider, Time.frameCount,
					  collidingTool, interactionType)));
				}
			}
		}

		private bool IsValidContact(InteractableTool collidingTool, Vector3 buttonDirection)
		{
			if (_contactTests == null || collidingTool.IsFarFieldTool)
			{
				return true;
			}

			foreach (var contactTest in _contactTests)
			{
				switch (contactTest)
				{
					case ContactTest.BackwardsPress:
						if (!PassEntryTest(collidingTool, buttonDirection))
						{
							return false;
						}

						break;
					default:
						if (!PassPerpTest(collidingTool, buttonDirection))
						{
							return false;
						}

						break;
				}
			}

			return true;
		}

		/// <summary>
		/// Is tool entering button correctly? Check velocity and make sure that
		/// tool is not below action zone.
		/// </summary>
		private bool PassEntryTest(InteractableTool collidingTool, Vector3 buttonDirection)
		{
			var jointVelocityVector = collidingTool.Velocity.normalized;
			var dotProduct = Vector3.Dot(jointVelocityVector, buttonDirection);
			if (dotProduct < ENTRY_DOT_THRESHOLD)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Is our tool pointing in opposite direction compared to button?
		/// </summary>
		private bool PassPerpTest(InteractableTool collidingTool, Vector3 buttonDirection)
		{
			// the "right" vector points along tool by default
			// if it's right hand, then flip that direction
			var toolDirection = collidingTool.ToolTransform.right;
			if (collidingTool.IsRightHandedTool)
			{
				toolDirection = -toolDirection;
			}

			var dotProduct = Vector3.Dot(toolDirection, buttonDirection);
			if (dotProduct < PERP_DOT_THRESHOLD)
			{
				return false;
			}

			return true;
		}
	}
}
