namespace VRTK
{

	using UnityEngine;
    using VRTK.Examples.Archery;
    using VRTK.GrabAttachMechanics;
	public class TouchWeaponManager : MonoBehaviour {

        public GameObject Arrow;
		private BowAim bow;

        public GameObject TopWeapon;
        public GameObject RightWeapon;
        public GameObject BottomWeapon;
        public GameObject LeftWeapon;
        public GameObject MiddleWeapon;

        private GameObject TopWeaponClone;
        private GameObject RightWeaponClone;
        private GameObject BottomWeaponClone;
        private GameObject LeftWeaponClone;
        private GameObject MiddleWeaponClone;
        private GameObject OtherController;
        private VRTK_InteractableObject interact;
        private GameObject newArrow = null;
        private bool pressed = false;
		private bool createdArrow;

        private GameObject currentGrabbedObject;

        private bool isColliding;
		// Use this for initialization
        void Start(){
            TopWeaponClone = Instantiate(TopWeapon);
            RightWeaponClone = Instantiate(RightWeapon);
        }

		private void OnTriggerStay(Collider collider)
        {   
            VRTK_InteractGrab grabbingController = (collider.gameObject.GetComponent<VRTK_InteractGrab>() ? collider.gameObject.GetComponent<VRTK_InteractGrab>() : collider.gameObject.GetComponentInParent<VRTK_InteractGrab>());
            VRTK_ControllerEvents grabbingControllerEvent = (collider.gameObject.GetComponent<VRTK_ControllerEvents>() ? collider.gameObject.GetComponent<VRTK_ControllerEvents>() : collider.gameObject.GetComponentInParent<VRTK_ControllerEvents>());
            // if (collider.gameObject.name == "Head"){
                if(OtherController != null && grabbingController != null && OtherController.gameObject.name == grabbingController.gameObject.name){
                        // if (CanGrabArrow(OtherController.gameObject.GetComponent<VRTK_InteractGrab>())){
                            if (OtherController.GetComponent<VRTK_InteractGrab>().GetGrabbedObject() == null ){
                                Debug.Log("grabbedobject is null");
                                newArrow = Instantiate(Arrow);
                                newArrow.transform.position = OtherController.gameObject.transform.position;
                                OtherController.GetComponent<VRTK_InteractTouch>().ForceTouch(newArrow);
                                OtherController.GetComponent<VRTK_InteractGrab>().AttemptGrab();
                            }
                                
                        // }
                }
                else if (CanGrab(grabbingController) )
                {
                        Vector2 touchAxis = grabbingControllerEvent.GetTouchpadAxis();
                        if (touchAxis.x >= -0.5f && touchAxis.x < 0.5f && touchAxis.y > 0.5f && touchAxis.y <= 1f ){
                            if(grabbingController.GetGrabbedObject() == null || TopWeaponClone != grabbingController.GetGrabbedObject()){
                                if(weaponIsOccupied(TopWeaponClone)){
                                    ForceReleaseWeapon(TopWeaponClone);
                                }
                                Grab(grabbingController, TopWeaponClone);
                                Debug.Log("Top");                        
                            }
                        }
                        else if(touchAxis.x >= 0.5f && touchAxis.x <= 1f && touchAxis.y <= 0.5f && touchAxis.y > -0.5f ){
                            if(grabbingController.GetGrabbedObject() == null || RightWeaponClone != grabbingController.GetGrabbedObject()){
                                if(weaponIsOccupied(RightWeaponClone)){
                                    ForceReleaseWeapon(RightWeaponClone);
                                }
                                Debug.Log("Right");
                                Grab(grabbingController, RightWeaponClone);
                            }
                        }
                        else if(touchAxis.x >= -0.5f && touchAxis.x < 0.5f && touchAxis.y < -0.5f && touchAxis.y >= -1f ){
                            if(grabbingController.GetGrabbedObject() == null || BottomWeaponClone != grabbingController.GetGrabbedObject()){
                                Debug.Log("Bottom");
                                Grab(grabbingController, BottomWeaponClone);
                            }
                        }
                        else if(touchAxis.x <= -0.5f && touchAxis.x > -1f && touchAxis.y < 0.5f && touchAxis.y >= -0.5f ){
                            if(grabbingController.GetGrabbedObject() == null || LeftWeaponClone != grabbingController.GetGrabbedObject()){
                                Debug.Log("Left");
                                Grab(grabbingController, LeftWeaponClone);
                            }
                        }
                }
            // }
        }

        private GameObject GetOtherController(GameObject pressingController){
            if(VRTK_SDK_Bridge.IsControllerLeftHand(pressingController)){
                return OtherController = VRTK_DeviceFinder.GetControllerRightHand();
            }else{
                return OtherController = VRTK_DeviceFinder.GetControllerLeftHand();
            }
        }
        private bool CanGrab(VRTK_InteractGrab grabbingController)
        {
            return (grabbingController && grabbingController.gameObject.GetComponent<VRTK_ControllerEvents>().touchpadPressed);
        }

        private bool CanGrabArrow(VRTK_InteractGrab grabbingController)
        {
            return (grabbingController && grabbingController.gameObject.GetComponent<VRTK_ControllerEvents>().grabPressed);
        }


        private bool weaponIsOccupied(GameObject weapon){
            return (weapon.GetComponent<VRTK_InteractableObject>().IsGrabbed());
        }

        private void ForceReleaseWeapon(GameObject weapon){
            if (weapon.GetComponent<VRTK_InteractableObject>() != null){
                weapon.GetComponent<VRTK_InteractableObject>().GetGrabbingObject().GetComponent<VRTK_InteractGrab>().ForceRelease(false);
            }
        }
        private void Grab(VRTK_InteractGrab grabbingController, GameObject weaponToGrab){
            weaponToGrab.transform.position = grabbingController.gameObject.transform.position;
            grabbingController.GetComponent<VRTK_InteractTouch>().ForceStopTouching();
            grabbingController.ForceRelease(false);
            grabbingController.GetComponent<VRTK_InteractTouch>().ForceTouch( weaponToGrab);
            grabbingController.AttemptGrab();
            currentGrabbedObject = weaponToGrab;
            if (weaponToGrab.transform.tag == "TwoHand"){
                Debug.Log("BasicBow is grabbed");
                GetOtherController(grabbingController.gameObject).GetComponent<VRTK_InteractGrab>().ForceRelease(false);
            }else{
                OtherController = null;
            }
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