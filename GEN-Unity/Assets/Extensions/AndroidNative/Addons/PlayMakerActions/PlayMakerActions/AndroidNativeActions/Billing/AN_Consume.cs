﻿using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions {

	[ActionCategory("Android Native - Billing")]
	[Tooltip("Aaction will start purchase flow on device. In Editor mode Success event will fired immediately")]
	public class AN_Consume : FsmStateAction {


		[Tooltip("Event fired when Store Kit initlization is complete")]
		public FsmEvent successEvent;

		[Tooltip("Event fired when Store Kit initlization is failed")]
		public FsmEvent failEvent;


		[Tooltip("Purhase product Id")]
		public string ProductID  = "";

	

		public override void OnEnter() {

			bool IsInEdditorMode = false;
			
			#if UNITY_EDITOR_OSX || UNITY_EDITOR
			IsInEdditorMode = true;
			#endif
			
			if(IsInEdditorMode) {
				Fsm.Event(successEvent);
				Finish();
				return;
			}

			InitAndroidInventoryTask iTask = InitAndroidInventoryTask.Create();
			iTask.addEventListener(BaseEvent.COMPLETE, OnInvComplete);
			iTask.addEventListener(BaseEvent.FAILED, OnInvFailed);
			iTask.Run();


			
		}

		private void OnInvComplete(CEvent e) {
			e.dispatcher.removeEventListener(BaseEvent.COMPLETE, OnInvComplete);
			e.dispatcher.removeEventListener(BaseEvent.FAILED, OnInvFailed);

			AndroidInAppPurchaseManager.instance.addEventListener (AndroidInAppPurchaseManager.ON_PRODUCT_CONSUMED,  OnProductConsumed);
			AndroidInAppPurchaseManager.instance.consume(ProductID);
		}

		private void OnInvFailed(CEvent e) {
			e.dispatcher.removeEventListener(BaseEvent.COMPLETE, OnInvComplete);
			e.dispatcher.removeEventListener(BaseEvent.FAILED, OnInvFailed);
			OnFailed();
		}

		private void OnProductConsumed(CEvent e) {
			BillingResult result = e.data as BillingResult;
			
			if(result.isSuccess) {
				OnPurchase();
			} else {
				OnFailed();
			}
			
			Debug.Log ("Cousume Responce: " + result.response.ToString() + " " + result.message);
		}



		private void OnPurchase() {
			AndroidInAppPurchaseManager.instance.removeEventListener (AndroidInAppPurchaseManager.ON_PRODUCT_CONSUMED,  OnProductConsumed);;

			Fsm.Event(successEvent);
			Finish();

		}

		private void OnFailed() {
			AndroidInAppPurchaseManager.instance.removeEventListener (AndroidInAppPurchaseManager.ON_PRODUCT_CONSUMED,  OnProductConsumed);

			Fsm.Event(failEvent);
			Finish();

		}
		
	}
}