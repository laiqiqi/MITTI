namespace VRTK
{

	using UnityEngine;
    using VRTK.Examples.Archery;
	public class TouchWeaponManager : MonoBehaviour {
		public GameObject Bow;

        public GameObject Arrow;
		private BowAim bow;

        private GameObject ArrowController;
        private VRTK_InteractableObject interact;
        private GameObject newBow = null;
        private GameObject newArrow = null;
        private bool newBowAttach = false;
		private bool createdArrow;
		// Use this for initialization
        void Start(){
            newBow = Instantiate(Bow);
            // newBow.SetActive(false);
        }
		private void OnTriggerStay(Collider collider)
        {   
            VRTK_InteractGrab grabbingController = (collider.gameObject.GetComponent<VRTK_InteractGrab>() ? collider.gameObject.GetComponent<VRTK_InteractGrab>() : collider.gameObject.GetComponentInParent<VRTK_InteractGrab>());
            // if (CanGrab(grabbingController) && !newBow.activeSelf)
            
            // Debug.Log("hitbox");
            // Debug.Log("can" + CanGrab(grabbingController));
            // Debug.Log("newbxat" +!newBowAttach);
            if (CanGrab(grabbingController) && !newBowAttach)
            {
                // newBow = Instantiate(Bow);
                // Debug.Log("cangrab");
                newBowAttach = true;
                // newBow.SetActive(true);
                newBow.transform.position = grabbingController.gameObject.transform.position;
                grabbingController.GetComponent<VRTK_InteractTouch>().ForceStopTouching();
                grabbingController.GetComponent<VRTK_InteractTouch>().ForceTouch(newBow);
                // Debug.Log(grabbingController.GetComponent<VRTK_InteractTouch>().GetTouchedObject());
                grabbingController.AttemptGrab();
                // Debug.Log(newBow.gameObject.name);
                // Debug.Log(grabbingController.gameObject.name);
               
                GetArrowController(grabbingController.gameObject);
            }
            else if (ArrowController != null && CanGrab(ArrowController.gameObject.GetComponent<VRTK_InteractGrab>())){
                newArrow = Instantiate(Arrow);
                newArrow.transform.position = ArrowController.gameObject.transform.position;
                ArrowController.GetComponent<VRTK_InteractTouch>().ForceTouch(newArrow);
                // Debug.Log(ArrowController.GetComponent<VRTK_InteractTouch>().GetTouchedObject());
                ArrowController.GetComponent<VRTK_InteractGrab>().AttemptGrab();
            }
        }

        private void GetArrowController(GameObject pressingController){
            if(VRTK_SDK_Bridge.IsControllerLeftHand(pressingController)){
                ArrowController = VRTK_DeviceFinder.GetControllerRightHand();
            }else{
                ArrowController = VRTK_DeviceFinder.GetControllerLeftHand();
            }
        }
        private bool CanGrab(VRTK_InteractGrab grabbingController)
        {
            // Debug.Log("grabbingController" + grabbingController);
            // Debug.Log("grabbingController.GetGrabbedObject() == null"+ grabbingController.GetGrabbedObject() == null);
            // Debug.Log("grabbingController.gameObject.GetComponent<VRTK_ControllerEvents>().grabPressed"+grabbingController.gameObject.GetComponent<VRTK_ControllerEvents>().grabPressed);
            return (grabbingController && grabbingController.GetGrabbedObject() == null && grabbingController.gameObject.GetComponent<VRTK_ControllerEvents>().grabPressed);
        }

        private bool NoArrowNotched(GameObject controller)
        {
            if (VRTK_SDK_Bridge.IsControllerLeftHand(controller))
            {
                bow = VRTK_DeviceFinder.GetControllerRightHand().GetComponentInChildren<BowAim>();
            }
            else if (VRTK_SDK_Bridge.IsControllerRightHand(controller))
            {
                bow = VRTK_DeviceFinder.GetControllerLeftHand().GetComponentInChildren<BowAim>();
            }
            return (bow == null || !bow.HasArrow());
        }
	}
}