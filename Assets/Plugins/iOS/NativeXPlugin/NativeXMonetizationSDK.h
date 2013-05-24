//
//NativeX.h
//NativeX
//
//  This file is subject to the SDK Source Code License Agreement defined in file
//  "SDK_SourceCode_LicenseAgreement", which is part of this source code package.
//
//  Created by Patrick Brennan on 10/6/11.
//  Copyright 2011 NativeX. All rights reserved.

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "NativeXBannerAdView.h"
#import "NativeXInterstitialAdViewController.h"
#import "NativeXInAppPurchaseTrackRequest.h"

@protocol NativeXMonetizationDelegate;

/** Main class for NativeXPublisherSDK

*/
@interface NativeXMonetizationSDK : NSObject

@property (nonatomic, copy) NSString *gameTitle;
@property (nonatomic, assign) BOOL showViolations;
@property (nonatomic, assign) BOOL showMessages;
@property (nonatomic, strong) id<NativeXMonetizationDelegate> delegate;
@property (nonatomic, strong) UIPopoverController *offerWallPopover;
@property (nonatomic, assign) BOOL shouldShowRedeemAlert;
@property (nonatomic, assign) BOOL shouldBeWebOfferWall;
@property (nonatomic, strong) UIAlertView *featuredAlertView;
@property (nonatomic, readonly) BOOL isShowFeaturedOffer;
@property (nonatomic, assign) int numberOfOffersInOfferWall;
@property (nonatomic, assign) BOOL shouldUseInAppDownload;

#pragma mark - Publisher API

// This method provides access to the NativeXPublisherSDK object.
// Returns: a singleton NativeXPublisherSDK instance.
+ (id)sharedInstance;

// This method provides access to NativeX SDK version
// Returns: NativeXPublisherSDK Version 
- (NSString *)getSDKVersion;

// Create a session with NativeX offer network
// param: appID -- is the unique Identifier you receive from NativeX
// param: publisherUserId -- Id used for publisher currency postback if used
// call this in AppDidFinishLaunchingWithOptions
- (void)initiateWithAppId:(NSString *)appId
       andPublisherUserId:(NSString *)publisherUserId;

// iPhone offer wall presentation
- (void)openOfferWallFromPresentingViewController:(UIViewController *)presentingViewController;

// iPad offer wall presentation
- (void)openOfferWallFromPopoverView:(UIView *)popoverView;

// Gets and shows a featured offer alert
- (void)showFeaturedOffer;

// Gets and caches a featured offer alert
- (void)getAndCacheFeaturedOffer;

// Shows cached featured offer alert
- (void)showCachedFeaturedOffer;

// Call redeem currency 
- (void)redeemCurrency;

// Banner Ad View
- (NativeXBannerAdView *)bannerAdViewWithThemeID:(NSNumber *)themeID
                                        delegate:(id<NativeXBannerAdViewDelegate>)delegate
                                           frame:(CGRect)frame;

// Interstitial Ad View
- (NativeXInterstitialAdViewController *)interstitialAdViewControllerWithThemeID:(NSNumber *)themeID
                                                                        delegate:(id<NativeXInterstitialAdViewControllerDelegate>)delegate;

- (NativeXInAppPurchaseTrackRequest *)trackInAppPurchaseWithTrackRecord:(NativeXInAppPurchaseTrackRecord *)trackRecord
                                                               delegate:(id<NativeXInAppPurchaseTrackDelegate>)delegate; //if the delegate is about to be deallocated clear return value's delegate property

// Non-reward web offer wall iPhone and iPad presentation
- (void)openNonRewardWebOfferWallFromPresentingViewController:(UIViewController *)presentingViewController;

// returns the sessionId
- (NSString *) getSessionId;


#pragma mark - NativeX Advertiser API
// NativeX Advertiser API

// call this to connect to NativeX and inform that the app "appID" was run
// param: appID -- is the unique Identifier you receive from NativeX
// call this in AppDidFinishLaunchingWithOptions
- (void) connectWithAppID:(NSString*)appID;

// call this to connect to NativeX and inform that the app "actionID" was performed
// param: actionID -- is the unique Identifier for the action, that you receive from NativeX
- (void)actionTakenWithActionID:(NSString*)actionID;

// call this when you no longer need the shared connector object
// usually in AppWillTerminate
- (void)close;

@end

#pragma mark - Publisher Delegates

@protocol NativeXMonetizationDelegate <NSObject>

@required

// Called when the Offer Wall is successfully initialized.
- (void)nativeXMonetizationSdkDidInitiateWithIsOfferwallAvailable:(BOOL)isAvailable;

// Called when there is an error trying to initialize the Offer Wall.
- (void)nativeXMonetizationSdkDidFailToInitiate: (NSError *) error;

// Called when the currency redemption is successfull.
- (void)didRedeemWithBalances:(NSArray *)balances
                 andReceiptId:(NSString *)receiptId;

// Called when the currency redemption is unsuccessfull.
- (void)didRedeemWithError:(NSError *)error;

// Called when publisher is about to display modally fullscreen instruction view for a chosen featured offer
- (UIViewController *)parentViewControllerForModallyPresentingInterstitialInstructionView:(UIViewController *)interstitialInstructionVC;

@optional
- (void)offerWallWillDisplay;
- (void)offerWallDidDisplay;
- (void)offerWallWillDismiss;
- (void)offerWallDidDismiss;

// Called when offerwall or ad offers are about to redirect
- (void)offerWallWillRedirectUserToAppStore;

- (void)featuredOfferNotAvailable;
- (void)featuredOfferIsAvailable;
- (void)featuredOfferDidDismiss;

@end
